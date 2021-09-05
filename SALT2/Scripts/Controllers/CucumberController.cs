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

    /// <summary>
    /// Gets a value indicating whether if this entity is active or not.
    /// </summary>
    public bool IsActive { get => isActive; }

    /// <inheritdoc/>
    public override void _Ready()
    {
        player = (KinematicBody)GetNode("/root/Main/Player");

        if (player == null)
        {
            GD.PrintErr("Could not resolve player");
        }

        GD.Print(this.Translation);
    }

    /// <inheritdoc/>
    public override void _Process(float delta)
    {
        // determine if player touching
        if (IsPlayerTouching() && isActive)
        {
            GD.Print("Player touching cucumber");
            Disable();

            // TODO: animation, clean up this entity
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

        var inX = this.Translation.x - this.Scale.x < playerX && playerX < this.Translation.x + this.Scale.x;
        var inY = this.Translation.y - this.Scale.y < playerY && playerY < this.Translation.y + this.Scale.y;

        return inX && inY;
    }

    private void Disable()
    {
        this.Visible = false;
        isActive = false;
    }
}
