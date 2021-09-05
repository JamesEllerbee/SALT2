using System;
using Godot;

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

    /// <summary>
    /// Gets or sets a value indicating whether bullet is shot.
    /// </summary>
    public bool Shoot { get => shoot; set => shoot = value; }

    /// <inheritdoc/>
    public override void _Ready()
    {
        SetAsToplevel(true);
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
            QueueFree();
        }
        else if (body.IsInGroup("Player"))
        {
            GD.Print("Player Hit!");

            PlayerController player = body as PlayerController;
            player.Damage(damage);
            QueueFree();
        }
        else
        {
            QueueFree();
        }
    }
}
