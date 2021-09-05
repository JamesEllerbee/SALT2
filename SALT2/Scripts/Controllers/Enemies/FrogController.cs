using Godot;
using SALT2.Scripts.Enumerations;
using System;

namespace SALT2.Scripts.Controllers.Enemies
{
    /// <summary>
    /// Script that controls the Frog enemy types.
    /// </summary>
    public abstract class FrogController : KinematicBody
    {
        /// <summary>
        /// The direction modifier determines which direction the entity should begin moving in.
        /// </summary>
        [Export]
        private int directionModifier = 1;

        [Export]
        private long changeDirectionMs;

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


        /// <inheritdoc/>
        public override void _Ready()
        {
            base._Ready();

            GD.Print($"Speed={MoveSpeed}, Period={MovingPeriod}");

            // set the inital change direction period.
            changeDirectionMs = GetNextChangeDirectionPeriod();
        }

        /// <inheritdoc/>
        public override void _PhysicsProcess(float delta)
        {
            base._PhysicsProcess(delta);
        }

        /// <summary>
        /// Process the walk cycle.
        /// </summary>
        protected void DoWalkCycle()
        {
            // during the process method, apply movement along the "walk cycle"
            var currentTime = GetCurrentTime();
            if (IsTimeToChange(currentTime))
            {
                // update direction modifier
                CurrentFacingDirection *= -1;

                // update next change directino interval
                TimeToChange_Ms = GetNextChangeDirectionPeriod();
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
    }
}
