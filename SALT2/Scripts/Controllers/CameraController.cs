using System;
using Godot;

/// <summary>
/// Script used to control the camera.
/// </summary>
public class CameraController : Camera
{
    [Export]
    private float snapOffset = 3;

    private KinematicBody player;

    /// <inheritdoc/>
    public override void _Ready()
    {
        player = (KinematicBody)GetNode("/root/Main/Player");

        if (player == null)
        {
            GD.PrintErr("Could not resolve player node.");
        }
    }

    /// <inheritdoc/>
    public override void _Process(float delta)
    {
        var playerXPos = player != null ?
            player.Translation.x :
            100f;

        playerXPos += snapOffset;

        if (playerXPos > this.Translation.x)
        {
            this.Translation = new Vector3(playerXPos, this.Translation.y, this.Translation.z);
        }
    }
}
