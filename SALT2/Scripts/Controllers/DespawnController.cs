using Godot;
using System;

public class DespawnController : Area
{
    /// <summary>
    /// Hit detection code.
    /// </summary>
    /// <param name="body">Value for body that was hit by despawner.</param>
    public void _on_Despawner_body_entered(PhysicsBody body)
    {
        if (body.IsInGroup("Enemy"))
        {
            // Despawn enemy.
            body.QueueFree();
        }
    }
}
