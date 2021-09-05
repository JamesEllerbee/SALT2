using System;
using Godot;

namespace SALT2.Scripts.Controllers.Enemies
{
    /// <summary>
    /// Controls an instance of the Basic Ground Frog enemy.
    /// </summary>
    public class BasicFrogController : FrogController
    {
        private Position3D gun;
        private Godot.Timer cdTimer;
        private PackedScene bullet;
        private AnimationPlayer anim;
        private bool canShoot = true;
        private bool shooting = false;

        /// <inheritdoc/>
        public override void _Ready()
        {
            base._Ready();

            // anim = (AnimationPlayer)GetNode("AnimationPlayer");
            gun = (Position3D)GetNode("Graphics/Gun");
            cdTimer = (Godot.Timer)gun.GetNode("Cooldown");
            bullet = (PackedScene)ResourceLoader.Load("res://Scenes/ENEMY_BULLET.tscn");
        }

        /// <inheritdoc/>
        public override void _Process(float delta)
        {
            base._Process(delta);

            if (shooting && canShoot && !IsDead)
            {
                Shoot();
                cdTimer.Start();
                canShoot = false;
            }
            if (IsDead)
            {
                // todo: begin playing the death animation
            }
        }

        /// <inheritdoc/>
        public override void _PhysicsProcess(float delta)
        {
            base._PhysicsProcess(delta);

            // todo: can alter what this entity should be doing when player has been detected
            if (!shooting)
            {
                DoWalkCycle(delta);
            }
        }

        /// <summary>
        /// Player detection code.
        /// </summary>
        /// <param name="body">The body that entered this entity. </param>
        public void _on_DetectionRadius_body_entered(PhysicsBody body)
        {
            if (body.IsInGroup("Player"))
            {
                GD.Print("Detected player");
                shooting = true;
            }
        }

        /// <summary>
        /// Used to go back to walking when player exits detection radius.
        /// </summary>
        /// <param name="body">The body that exited this entity. </param>
        public void _on_DetectionRadius_body_exited(PhysicsBody body)
        {
            if (body.IsInGroup("Player"))
            {
                GD.Print("Stopped detecting player");

                // Resets player to correct rotation for walk cycle.
                if (CurrentFacingDirection == 1 && RotationDegrees.y > 0)
                {
                    Flip();
                }
                else if (CurrentFacingDirection == -1 && RotationDegrees.y < 0)
                {
                    Flip();
                }

                shooting = false;
            }
        }

        /// <summary>
        /// Player detection code from behind.
        /// </summary>
        /// <param name="body">The body that entered this entity. </param>
        public void _on_DetectionRadius2_body_entered(PhysicsBody body)
        {
            if (body.IsInGroup("Player"))
            {
                // Flip enemy to face player if player is behind enemy.
                Flip();
                GD.Print("Detected player");
                shooting = true;
            }
        }

        /// <summary>
        /// Used to go back to walking when player exits detection radius from behind.
        /// </summary>
        /// <param name="body">The body that exited this entity. </param>
        public void _on_DetectionRadius2_body_exited(PhysicsBody body)
        {
            if (body.IsInGroup("Player"))
            {
                GD.Print("Stopped detecting player");

                // Resets player to correct rotation for walk cycle.
                if (CurrentFacingDirection == 1 && RotationDegrees.y > 0)
                {
                    Flip();
                }
                else if (CurrentFacingDirection == -1 && RotationDegrees.y < 0)
                {
                    Flip();
                }

                shooting = false;
            }
        }

        /// <summary>
        /// Called when cooldown timer hits 0. Resets enemy's ability to shoot.
        /// </summary>
        public void _on_Cooldown_timeout()
        {
            canShoot = true;
        }

        // Logic for firing enemy's weapons.
        private void Shoot()
        {
            BulletController b = bullet.Instance() as BulletController;
            gun.AddChild(b);
            b.LookAt(GlobalTransform.origin, Vector3.Up);
            b.Shoot = true;
        }
    }
}