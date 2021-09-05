﻿using System;
using Godot;

namespace SALT2.Scripts.Controllers.Enemies
{
    /// <summary>
    /// Controls an instance of the Basic Ground Frog enemy.
    /// </summary>
    public class BasicFrogController : FrogController
    {
        /// <inheritdoc/>
        public override void _Ready()
        {
            base._Ready();
        }

        /// <inheritdoc/>
        public override void _Process(float delta)
        {
            base._Process(delta);

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
                GD.Print("Detected player");

                // todo: logic when player detected
            }
        }
    }
}