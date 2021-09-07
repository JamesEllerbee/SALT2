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
        private PackedScene bullet;
        private AnimationPlayer anim;
        private bool canShoot = false;
        private bool shooting = false;
        private bool shootAnim = false;

        /// <inheritdoc/>
        public override void _Ready()
        {
            base._Ready();

            anim = (AnimationPlayer)GetNode("Graphics/frogGunner/AnimationPlayer");
            anim.GetAnimation("Walk").Loop = true;
            anim.Play("Walk");
            gun = (Position3D)GetNode("Graphics/Gun");
            bullet = (PackedScene)ResourceLoader.Load("res://Scenes/ENEMY_BULLET.tscn");
        }

        /// <inheritdoc/>
        public override void _Process(float delta)
        {
            base._Process(delta);

            if (shooting && canShoot && !IsDead)
            {
                shootAnim = true;
                anim.Play("shoot");
                canShoot = false;
            }

            if (IsDead)
            {
                anim.Play("pain");
            }
        }

        /// <inheritdoc/>
        public override void _PhysicsProcess(float delta)
        {
            base._PhysicsProcess(delta);

            if (!shooting && !IsDead)
            {
                if (!shootAnim)
                {
                    anim.Play("Walk");
                    DoWalkCycle(delta);
                }
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
                shootAnim = true;
                anim.Play("shoot");
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

                // Resets enemy to correct rotation for walk cycle.
                if (CurrentFacingDirection == 1 && RotationDegrees.y > 0)
                {
                    Flip();
                }
                else if (CurrentFacingDirection == -1 && RotationDegrees.y < 0)
                {
                    Flip();
                }

                canShoot = false;
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
                shootAnim = true;
                anim.Play("shoot");
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

                // Resets enemy to correct rotation for walk cycle.
                if (CurrentFacingDirection == 1 && RotationDegrees.y > 0)
                {
                    Flip();
                }
                else if (CurrentFacingDirection == -1 && RotationDegrees.y < 0)
                {
                    Flip();
                }

                canShoot = false;
                shooting = false;
            }
        }

        /// <summary>
        /// Used to detect end of shooting animations.
        /// </summary>
        /// <param name="animation">The animation that has finished.</param>
        public void _on_AnimationPlayer_animation_finished(String animation)
        {
            if (animation == "shoot")
            {
                GetNode<AudioStreamPlayer>("ShootSoundFX").Play();
                Shoot();
                canShoot = true;
                shootAnim = false;
            }
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