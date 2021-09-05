using System;
using Godot;

namespace SALT2.Scripts.Controllers.Enemies
{
    /// <summary>
    /// Script that controls the Rusher Frog enemy types.
    /// </summary>
    public class RusherFrogController : FrogController
    {
        /// <inheritdoc/>
        public override void _Ready()
        {
            base._Ready();
        }

        /// <inheritdoc/>
        public override void _Process(float delta)
        {
            base._PhysicsProcess(delta);
        }

        /// <inheritdoc/>
        public override void _PhysicsProcess(float delta)
        {
            base._PhysicsProcess(delta);
            DoWalkCycle(delta);
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
    }
}