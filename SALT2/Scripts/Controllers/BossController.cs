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
    private bool isDead = false;
    private bool canShootGun = false;
    private bool canShootLazer = false;
    private bool isLazerFiring = false;
    private bool lazerProcess = false;
    private int hitPoints;

    private Position3D gun;
    private Position3D lazer;
    private Timer gunCooldown;
    private Timer lazerCooldown;
    private AudioStreamPlayer lazerDuration;
    private AudioStreamPlayer lazerCharge;
    private AudioStreamPlayer entranceSound;
    private AudioStreamPlayer deathSound;
    private AudioStreamPlayer shootSound;
    private Timer bossWindup;
    private PhysicsBody player;
    private RayCast lazerCast;
    private CSGCombiner lazerModel;
    private CSGCombiner lazerChargeModel;
    private AnimationPlayer damageAnim;
    private AnimationPlayer bossAnim;
    private AnimationPlayer lazerAnim;
    private AnimationPlayer gunAnim;
    private AnimationPlayer deathAnim;
    private PackedScene bullet;

    /// <inheritdoc/>
    public override void _Ready()
    {
        hitPoints = maxHitpoints;
        gun = (Position3D)GetNode("Gun");
        lazer = (Position3D)GetNode("Lazer");
        gunCooldown = (Timer)gun.GetNode("Cooldown");
        lazerCooldown = (Timer)lazer.GetNode("LazerCooldown");
        lazerDuration = (AudioStreamPlayer)lazer.GetNode("LazerDuration");
        lazerCharge = (AudioStreamPlayer)lazer.GetNode("LazerCharge");
        entranceSound = (AudioStreamPlayer)GetNode("EntranceSound");
        deathSound = (AudioStreamPlayer)GetNode("DeathSound");
        shootSound = (AudioStreamPlayer)GetNode("ShootSoundFX");
        bossWindup = (Timer)GetNode("BossWindup");
        lazerCast = (RayCast)lazer.GetNode("RayCast");
        lazerModel = (CSGCombiner)lazer.GetNode("CSGCombiner");
        lazerChargeModel = (CSGCombiner)lazer.GetNode("CSGCombiner2");
        damageAnim = (AnimationPlayer)GetNode("DamageAnimation");
        bossAnim = (AnimationPlayer)GetNode("AbirdBoss/AnimationPlayer");
        lazerAnim = (AnimationPlayer)GetNode("bossLaser/AnimationPlayer");
        deathAnim = (AnimationPlayer)GetNode("DeathAnimation");
        lazerAnim.GetAnimation("chargeLaser").Loop = true;
        lazerAnim.GetAnimation("shootlaser").Loop = true;
        gunAnim = (AnimationPlayer)GetNode("bossGun/AnimationPlayer");
        bossAnim.GetAnimation("idle").Loop = true;
        bossAnim.Play("idle");
        bullet = (PackedScene)ResourceLoader.Load("res://Scenes/ENEMY_BULLET.tscn");
        RotateY(3.14159f);
    }

    /// <inheritdoc/>
    public override void _Process(float delta)
    {
        if (isFighting)
        {
            if (canShootGun && !lazerProcess)
            {
                gunAnim.Play("shootlaser");
                Shoot();
                gunCooldown.Start();
                canShootGun = false;
            }

            if (canShootLazer)
            {
                canShootLazer = false;
                lazerChargeModel.Visible = true;
                lazerProcess = true;
                lazerAnim.Play("chargeLaser");
                lazerCharge.Play();
            }

            if (isLazerFiring)
            {
                if (lazerCast.IsColliding())
                {
                    //GD.Print(lazerCast.GetCollider().GetType().ToString());
                    if (lazerCast.GetCollider().GetType().ToString() == "PlayerController")
                    {
                        PlayerController collider = lazerCast.GetCollider() as PlayerController;
                        collider.UpdateHitPoints(1);
                    }
                }
            }
        }

        if (isDead)
        {
            lazerDuration.Stop();
            lazerCharge.Stop();
            damageAnim.Stop();
            gunAnim.Stop();
            lazerAnim.Stop();
            isFighting = false;
            canShootGun = false;
            canShootLazer = false;
            isLazerFiring = false;
            lazerProcess = false;
            bossAnim.PlaybackSpeed = 3;
            deathAnim.Play("deathAnimation");
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
            entranceSound.Play();
            bossWindup.Start();
            CollisionShape collider = (CollisionShape)GetNode("DetectionRadius/CollisionShape");
            collider.Disabled = true;
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

    /// <summary>
    /// Fire lazer when chargeup is finished.
    /// </summary>
    public void _on_LazerCharge_finished()
    {
        lazerChargeModel.Visible = false;
        ShootLazer();
    }

    /// <summary>
    /// Disable lazer after duration is up.
    /// </summary>
    public void _on_LazerDuration_finished()
    {
        isLazerFiring = false;
        lazerCast.Enabled = false;
        lazerModel.Visible = false;
        lazerProcess = false;
        lazerAnim.Stop();
        lazerCooldown.Start();
    }

    /// <summary>
    /// Deletes boss when death animation finishes.
    /// </summary>
    public void _on_DeathAnimation_animation_finished(String animation)
    {
        QueueFree();
    }

    /// <summary>
    /// Gives player a chance to see boss before fight.
    /// </summary>
    public void _on_BossWindup_timeout()
    {
        isFighting = true;
        gunCooldown.Start();
        lazerCooldown.Start();
    }

    /// <summary>
    /// Subtracts the <paramref name="amount"/> value from the boss's current health.
    /// </summary>
    /// <param name="amount"> The value that will be used to lower the boss's current hit point value. </param>
    public void UpdateHitPoints(int amount)
    {
        if (isFighting)
        {
            hitPoints -= amount;
            damageAnim.Play("damageTaken");

            if (hitPoints > maxHitpoints)
            {
                hitPoints = maxHitpoints;
            }
            else if (hitPoints < 0)
            {
                hitPoints = 0;
            }

            GD.Print($"Boss HP changed! Remaining HP {hitPoints}");

            if (hitPoints == 0)
            {
                deathSound.Play();
                isDead = true;
            }
            else if (hitPoints == (maxHitpoints / 2))
            {
                bossAnim.PlaybackSpeed = 2;
                entranceSound.Play();
            }
        }
    }

    // Logic for firing boss's standard gun.
    private void Shoot()
    {
        BulletController b = bullet.Instance() as BulletController;
        gun.AddChild(b);
        b.LookAt(player.GlobalTransform.origin, Vector3.Up);
        b.RotateY(3.14159f);
        b.RotateX(3.14159f);
        shootSound.Play();
        b.Shoot = true;
    }

    // Fires boss's lazer weapon.
    private void ShootLazer()
    {
        lazerCast.Enabled = true;
        lazerModel.Visible = true;
        isLazerFiring = true;
        lazerAnim.Play("shootlaser");
        lazerDuration.Play();
    }
}
