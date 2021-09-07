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

	/// <summary>
	/// Hit detection.
	/// </summary>
	/// <param name="body">The body that entered this entity.</param>
	public void OnCumcumberBodyEntered(PhysicsBody body)
	{
		if (body.IsInGroup("Player"))
		{
			var playerController = body as PlayerController;
			playerController.UpdateHitPoints(restoreAmount * -1);

			CollisionMask = 0b0;
			graphics.Visible = false;
			
			GetNode<AudioStreamPlayer>("CucumberSoundFX").Play();
		}
	}
}
