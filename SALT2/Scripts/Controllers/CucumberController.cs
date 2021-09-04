using System;
using Godot;

/// <summary>
/// Script used to control a cucumber collectable.
/// </summary>
public class CucumberController : StaticBody
{
    private KinematicBody player;

    [Export]
    private bool isActive = true;

    /// <inheritdoc/>
    public override void _Ready()
    {
        player = (KinematicBody)GetNode("/root/Main/Player");

        if (player == null)
        {
            GD.PrintErr("Could not resolve player");
        }
    }

    /// <inheritdoc/>
    public override void _PhysicsProcess(float delta)
    {
        // determine if player touching
        if (IsPlayerTouching() && isActive)
        {
            GD.Print("Player touching cucumber");
            Disable();

            // TODO: dispearring animation, clean up this entity
        }
    }

    private bool IsPlayerTouching()
    {
        // the player is touching this entity if the player'x and y are within this entitys translation and scale
        var playerX = player != null ?
            player.Translation.x :
            0;

        var playerY = player != null ?
            player.Translation.y :
            0;

        var inX = this.Translation.x - this.Scale.x < player.Translation.x && player.Translation.x < this.Translation.x + this.Scale.x;
        var inY = this.Translation.y - this.Scale.y < player.Translation.y && player.Translation.y < this.Translation.y + this.Scale.y;

        return inX && inY;
    }

    private void Disable()
    {
        this.Visible = false;
        isActive = false;
    }
}
