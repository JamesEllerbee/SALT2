using System;
using Godot;

namespace SALT2.Scripts.Controllers.Enemies
{
    /// <summary>
    /// Script that controls the Rusher Frog enemy types.
    /// </summary>
    public class RusherFrogController : FrogController
    {
        private AnimationPlayer anim;
        private AnimationPlayer damageAnim;
        private bool isPlayingDeathAnimation = false;

        /// <inheritdoc/>
        public override void _Ready()
        {
            base._Ready();

            // Animation Setup
            anim = (AnimationPlayer)GetNode("Graphics/polywogUPDATED/AnimationPlayer");
            damageAnim = (AnimationPlayer)GetNode("DamageAnimation");
            anim.GetAnimation("WalkCycle").Loop = true;
            anim.Play("WalkCycle");
        }

        /// <inheritdoc/>
        public override void _Process(float delta)
        {
            base._Process(delta);

            if (IsDead && !isPlayingDeathAnimation)
            {
                GD.Print("Playing death animation");
                // todo: begin playing the death animation
                anim.Play("Death");
                isPlayingDeathAnimation = true;
            }
        }

        /// <inheritdoc/>
        public override void _PhysicsProcess(float delta)
        {
            base._PhysicsProcess(delta);
            if (!IsDead)
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
                GD.Print($"Detected player");
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
            }
        }
    }
}