using System;
using Godot;

/// <summary>
/// Script that controlls the music player.
/// </summary>
public class MusicController : AudioStreamPlayer
{
    /// <inheritdoc/>
    public override void _Ready()
    {
        // Makes music loop
        if (!Playing)
        {
            Play();
        }
    }
}
