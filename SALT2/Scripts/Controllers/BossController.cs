using System;
using Godot;

/// <summary>
/// Controls boss sequence.
/// </summary>
public class BossController : StaticBody
{
    // Variables
    [Export]
    private int maxHitpoints = 100;
    [Export]
    private float aimSpeed = 2;

    private bool isFighting = false;
    private bool canShootGun = false;
    private bool canShootLazer = false;
    private int hitpoints;

    private Position3D gun;
    private Position3D lazer;
    private Timer gunCooldown;
    private Timer lazerCooldown;
    private PhysicsBody player;
    private PackedScene bullet;

    /// <inheritdoc/>
    public override void _Ready()
    {
        hitpoints = maxHitpoints;
        gun = (Position3D)GetNode("Gun");
        lazer = (Position3D)GetNode("Lazer");
        gunCooldown = (Timer)gun.GetNode("Cooldown");
        lazerCooldown = (Timer)lazer.GetNode("LazerCooldown");
        bullet = (PackedScene)ResourceLoader.Load("res://Scenes/ENEMY_BULLET.tscn");
        RotateY(3.14159f);
    }

    /// <inheritdoc/>
    public override void _Process(float delta)
    {
        if (isFighting)
        {
            if (canShootGun)
            {
                Shoot();
                gunCooldown.Start();
                canShootGun = false;
            }

            if (canShootLazer)
            {
                ShootLazer();
                lazerCooldown.Start();
                canShootLazer = false;
            }
        }
    }

    /// <summary>
    /// Starts fight when player enters detection radius.
    /// </summary>
    /// <param name="body">Body that entered detection radius.</param>
    public void _on_DetectionRadius_body_entered(PhysicsBody body)
    {
        if (body.IsInGroup("Player"))
        {
            player = body;
            isFighting = true;
            gunCooldown.Start();
            lazerCooldown.Start();
        }
    }

    /// <summary>
    /// Cooldown for gun.
    /// </summary>
    public void _on_Cooldown_timeout()
    {
        canShootGun = true;
    }

    /// <summary>
    /// Cooldown for lazer.
    /// </summary>
    public void _on_LazerCooldown_timeout()
    {
        canShootLazer = true;
    }

    // Logic for firing boss's standard gun.
    private void Shoot()
    {
        BulletController b = bullet.Instance() as BulletController;
        gun.AddChild(b);
        b.LookAt(player.GlobalTransform.origin, Vector3.Up);
        b.RotateY(3.14159f);
        b.RotateX(3.14159f);
        b.Shoot = true;
    }

    // Fires boss's lazer weapon.
    private void ShootLazer()
    {
        // TODO: Add lazer logic.
    }
}
