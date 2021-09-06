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
    private bool bossSequence = false;
    private PackedScene alert;
    private PackedScene missionStart;

    /// <inheritdoc/>
    public override void _Ready()
    {
        player = (KinematicBody)GetNode("/root/Main/Player");
        alert = (PackedScene)ResourceLoader.Load("res://Prefabs/ALERT.tscn");
        missionStart = (PackedScene)ResourceLoader.Load("res://Prefabs/MISSION_START.tscn");
        var s = missionStart.Instance();
        AddChild(s);

        if (player == null)
        {
            GD.PrintErr("Could not resolve player node.");
        }
    }

    /// <inheritdoc/>
    public override void _Process(float delta)
    {
        if (!bossSequence)
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

    /// <summary>
    /// Used to stop moving camera during boss sequence.
    /// </summary>
    /// <param name="body">Value for body that was hit by boss detector.</param>
    public void _on_BossDetector_body_entered(PhysicsBody body)
    {
        if (body.IsInGroup("BossDetector"))
        {
            bossSequence = true;
            GD.Print("Begin boss sequence.");
            var a = alert.Instance();
            AddChild(a);
        }
    }
}
