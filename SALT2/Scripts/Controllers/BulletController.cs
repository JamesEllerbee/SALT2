using System;
using Godot;
using SALT2.Scripts.Controllers.Enemies;

/// <summary>
/// Script used to control all types of bullets.
/// </summary>
public class BulletController : RigidBody
{
    // Variables
    [Export]
    private int damage = 1;
    [Export]
    private int speed = 15;

    private bool shoot = false;

    private ScoreController scoreController;

    /// <summary>
    /// Gets or sets a value indicating whether bullet is shot.
    /// </summary>
    public bool Shoot { get => shoot; set => shoot = value; }

    /// <inheritdoc/>
    public override void _Ready()
    {
        SetAsToplevel(true);
        scoreController = GetNode<ScoreController>("/root/Main/Score");
    }

    /// <inheritdoc/>
    public override void _PhysicsProcess(float delta)
    {
        base._PhysicsProcess(delta);

        if (shoot)
        {
            LinearVelocity = new Vector3(Transform.basis.z.Normalized() * speed);
        }
    }

    /// <summary>
    /// Hit detection code.
    /// </summary>
    /// <param name="body">Value for body that was hit by bullet.</param>
    public void _on_Area_body_entered(PhysicsBody body)
    {
        if (body.IsInGroup("Enemy"))
        {
            GD.Print("Enemy Hit!");

            // TODO: Add logic to do damage.

            // Get enemy controller
            var frogController = body as FrogController;
            if (frogController != null)
            {
                frogController.Damage(damage);
            }
            else
            {
                GD.PrintErr("Could not resolve frog controller");
            }

            QueueFree();
            scoreController.Add(10);
        }
        else if (body.IsInGroup("Player"))
        {
            GD.Print("Player Hit!");

            PlayerController player = body as PlayerController;
            player.UpdateHitPoints(damage);
            QueueFree();
        }
        else if (body.IsInGroup("Boss"))
        {
            GD.Print("Boss Hit!");

            // TODO: Add logic to damage boss
            QueueFree();
        }
        else
        {
            QueueFree();
        }
    }
}
