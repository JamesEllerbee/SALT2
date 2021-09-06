using System;
using Godot;

namespace SALT2.Scenes
{
    /// <summary>
    /// Script to control game components.
    /// </summary>
    public class GameController : Spatial
    {
        private MusicController musicController;

        /// <inheritdoc/>
        public override void _Ready()
        {
            musicController = GetNode<MusicController>("/root/Main/SeedbayMusic");
        }

        /// <inheritdoc/>
        public override void _Process(float delta)
        {
            // check to see if the music controller is playing the level track, and make it play if it is not.
            if (!musicController.Playing)
            {
                musicController.Play();
            }
        }
    }
}