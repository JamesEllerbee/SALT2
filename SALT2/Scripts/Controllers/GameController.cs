using System;
using Godot;

/// <summary>
/// Script to controll game components.
/// </summary>
public class GameController : Spatial
{
    private MusicController musicController;

    /// <inheritdoc/>
    public override void _Ready()
    {
        musicController = GetNode<MusicController>("/root/Main/SeedBayMusic");
    }

    /// <inheritdoc/>
    public override void _Process(float delta)
    {
        // if the music controller is not currently playing music, make it play.
        if (!musicController.Playing)
        {
            musicController.Play();
        }
    }
}
