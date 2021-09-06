using System;
using Godot;

/// <summary>
/// Script used to control a cucumber collectable.
/// </summary>
public class CucumberController : Area
{
    [Export]
    private int restoreAmount = 1;

    private KinematicBody player;
    private Spatial graphics;

    [Export]
    private bool isActive = true;

    private bool wasUsed = false;

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
            GD.PrintErr("[Cucumber Controller] Could not resolve player");
        }

        graphics = (Spatial)GetNode("Graphics");

        if (graphics == null)
        {
            GD.PrintErr("[Cucumber Controller] Could not resolve Graphics node.");
        }

        GD.Print(this.Translation);
    }

    /// <inheritdoc/>
    public override void _Process(float delta)
    {
        base._Process(delta);

        if (wasUsed)
        {
            Free();
        }
    }

    /// <summary>
    /// Hit detection.
    /// </summary>
    /// <param name="body">The body that entered this entity.</param>
    public void OnCumcumberBodyEntered(PhysicsBody body)
    {
        if (body.IsInGroup("Player") && !wasUsed)
        {
            var playerController = body as PlayerController;
            playerController.UpdateHitPoints(restoreAmount * -1);

            wasUsed = true;
            CollisionMask = 0b0;
            graphics.Visible = false;
        }
    }
}
