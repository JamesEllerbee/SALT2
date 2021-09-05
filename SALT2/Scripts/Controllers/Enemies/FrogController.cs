using Godot;
using SALT2.Scripts.Enumerations;
using System;
using System.Threading.Tasks;

namespace SALT2.Scripts.Controllers.Enemies
{
    /// <summary>
    /// Script that controls the Frog enemy types.
    /// </summary>
    public abstract class FrogController : KinematicBody
    {
        #region Private Fields

        /// <summary>
        /// The direction modifier determines which direction the entity should begin moving in.
        /// </summary>
        [Export]
        private int directionModifier = 1;

        private long changeDirectionMs;

        private int deathSequenceTimeMs = 1000;

        private bool inDeathSequence = false;

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets or sets this entity's enemy type.
        /// </summary>
        [Export]
        public FrogType EnemyType { get; protected set; }

        /// <summary>
        /// Gets or sets the movement speed of this entity.
        /// </summary>
        [Export]
        public float MoveSpeed { get; protected set; }

        /// <summary>
        /// Gets or sets the gravity of this entity.
        /// </summary>
        [Export]
        public float Gravity { get; protected set; }

        /// <summary>
        /// Gets or sets the moving period is how long this entity will move in a given direction.
        /// </summary>
        [Export]
        public long MovingPeriod { get; protected set; }

        /// <summary>
        /// Gets or sets the amount of damage this entity will cause another entity.
        /// </summary>
        [Export]
        public int AttackDamage { get; protected set; }

        /// <summary>
        /// Gets or sets the amount of damage this eneity can take before getting destoried.
        /// </summary>
        [Export]
        public int HitPoints { get; protected set; }

        /// <summary>
        /// Gets or sets a value indicating whether if this entity is dead or not.
        /// </summary>
        public bool IsDead { get; protected set; }

        /// <summary>
        /// Gets or sets the value indicating the current direction this entity is facing.
        /// </summary>
        public int CurrentFacingDirection { get => directionModifier; protected set => directionModifier = value; }

        /// <summary>
        /// Gets or sets the next change direction interval.
        /// </summary>
        public long TimeToChange_Ms { get => changeDirectionMs; protected set => changeDirectionMs = value; }

        /// <summary>
        /// Gets or sets vertical velocity.
        /// </summary>
        public float VerticalVelocity { get; protected set; }

        /// <summary>
        /// Gets or sets the graphics node for this entity.
        /// </summary>
        protected Spatial Graphics { get; set; }

        #endregion

        /// <inheritdoc/>
        public override void _Ready()
        {
            base._Ready();

            GD.Print($"Speed={MoveSpeed}, Period={MovingPeriod}");
            Graphics = (Spatial)GetNode("Graphics");

            // set the inital change direction period.
            changeDirectionMs = GetNextChangeDirectionPeriod();
        }

        /// <inheritdoc/>
        public override void _Process(float delta)
        {
            base._Process(delta);

            // if this entity hit points are <= 0  and is not already dead, then handle death
            if (HitPoints <= 0 && !IsDead)
            {
                GD.Print("I'm dead dude!");
                IsDead = true;

                // update layer so that player no longer interacts with this entity.
                CollisionLayer = 0b0;

                // update mask so that bullets no longer interact with this entity.
                CollisionMask = 0b0;

                // begin death equence timer.
                var deathTask = Task.Factory.StartNew(() =>
                {
                    Graphics.RotateZ(1.5708f);

                    // wait half a second
                    System.Threading.Thread.Sleep(deathSequenceTimeMs);

                    // free this entity
                    Free();
                });
            }
        }


        /// <inheritdoc/>
        public override void _PhysicsProcess(float delta)
        {
            base._PhysicsProcess(delta);

            var collisionInfo = MoveAndCollide(Vector3.Zero, testOnly: true);

            // if collided with player and this entity is not dead,
            if (collisionInfo != null && collisionInfo.Collider != null && collisionInfo.Collider is PlayerController && !IsDead)
            {
                // then resolve the player controller and damage the players
                var playerController = (PlayerController)collisionInfo.Collider;
                playerController.Damage(AttackDamage);
            }
        }

        /// <summary>
        /// Subtracts the damage value from the entity's current health.
        /// </summary>
        /// <param name="damageValue"> The value that will be used to lower the entity's current hit point value. </param>
        public void Damage(int damageValue)
        {
            // future: if an entity has more than 1 hp, and the enemy should have I frames, then update this to create a invulnerability timer and synchronize the code.
            HitPoints -= damageValue;
            GD.Print($"Enemy damaged! {HitPoints}");
        }

        /// <summary>
        /// Process the walk cycle.
        /// </summary>
        /// <param name="delta"> Time since last physics processing. </param>
        protected void DoWalkCycle(float delta)
        {
            // during the process method, apply movement along the "walk cycle"
            var currentTime = GetCurrentTime();
            if (IsTimeToChange(currentTime))
            {
                // update direction modifier
                CurrentFacingDirection *= -1;

                // update next change directino interval
                TimeToChange_Ms = GetNextChangeDirectionPeriod();

                // Flip model
                Flip();
            }

            // apply movement
            VerticalVelocity -= Gravity * delta;
            MoveAndSlide(new Vector3(CurrentFacingDirection * MoveSpeed, VerticalVelocity, 0), Vector3.Up);

            if (IsOnFloor())
            {
                VerticalVelocity = -0.1F;
            }
        }

        private long GetNextChangeDirectionPeriod()
        {
            return GetCurrentTime() + MovingPeriod;
        }

        private long GetCurrentTime()
        {
            return DateTimeOffset.Now.ToUnixTimeMilliseconds();
        }

        private bool IsTimeToChange(long currentTime)
        {
            return changeDirectionMs - currentTime <= 0L;
        }

        /// <summary>
        /// Flips enemy model depending on direction faced.
        /// </summary>
        protected void Flip()
        {
            RotateY(3.14159f);
        }
    }
}
