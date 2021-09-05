using System;
using System.Threading;
using System.Threading.Tasks;
using Godot;

/// <summary>
/// Script used to control the player.
/// </summary>
public class PlayerController : KinematicBody
{
    #region Private Fields

    [Export]
    private float gravity = 17;
    [Export]
    private float maxSpeed = 5;
    [Export]
    private float jumpHeight = 10;
    [Export]
    private float acceleration = 4;
    [Export]
    private float friction = 0.7f;
    [Export]
    private float airFriction = 0.01f;
    [Export]
    private int hitPoints = 3;
    [Export]
    private int invincibilityTimeMs = 1500;

    private Vector3 motion;
    private bool canShoot = true;
    private bool facingRight = true;
    private bool wasDamaged = false;
    private object syncObject = new object();

    private AnimationPlayer animPlayer;
    private Spatial graphics;
    private Position3D gun;
    private Godot.Timer cdTimer;
    private PackedScene bullet;

    #endregion

    #region Public Properties

    /// <summary>
    /// Gets a value indicating whether player is facing right.
    /// </summary>
    public bool GetFacingRight { get => facingRight; }

    /// <summary>
    /// Gets the player's current number of hit points.
    /// </summary>
    public int CurrentHealth { get => hitPoints; }

    #endregion


    /// <inheritdoc/>
    public override void _Ready()
    {
        graphics = (Spatial)GetNode("Graphics");
        animPlayer = (AnimationPlayer)graphics.GetNode("AnimationPlayer");
        gun = (Position3D)graphics.GetNode("Gun");
        cdTimer = (Godot.Timer)gun.GetNode("Cooldown");
        bullet = (PackedScene)ResourceLoader.Load("res://Scenes/BULLET.tscn");
        MoveLockZ = true;
    }

    /// <inheritdoc/>
    public override void _Process(float delta)
    {
        base._Process(delta);

        // When fire is pressed, spawn new bullet and shoot.
        if (Input.IsActionPressed("action_shoot") && canShoot)
        {
            Shoot();
            cdTimer.Start();
            canShoot = false;
        }

        // todo: do damaged stuff.
        if (hitPoints < 0)
        {
            // trigger death and respawn.
        }
    }


    /// <inheritdoc/>
    public override void _PhysicsProcess(float delta)
    {
        base._PhysicsProcess(delta);

        motion.y -= gravity * delta;
        bool isFriction = false;
        int moveDir = 0;

        // Check for player directional inputs to add or subtract acceleration.
        if (Input.IsActionPressed("move_right"))
        {
            motion.x = Math.Min(motion.x + acceleration, maxSpeed);
            moveDir += 1;

            // TODO: Add walk animation.
        }
        else if (Input.IsActionPressed("move_left"))
        {
            motion.x = Math.Max(motion.x - acceleration, -maxSpeed);
            moveDir -= 1;

            // TODO: Add walk animation.
        }
        else
        {
            isFriction = true;

            // TODO: Add idle animation.
        }

        // Jump logic. Checks if player is on floor before deciding if it is ok to jump.
        // If player is in the air, play jump animation. Additionally, less friction is
        // applied to the player while in the air.
        if (IsOnFloor())
        {
            if (Input.IsActionPressed("move_jump"))
            {
                motion.y = jumpHeight;
            }

            if (isFriction == true)
            {
                motion.x = Mathf.Lerp(motion.x, 0, friction);
            }
        }
        else
        {
            // Play jump animation if going up.
            if (motion.y > 0)
            {
                PlayAnimation("test_jump_anim");
            }

            // Play falling animation if going down.
            else
            {
                PlayAnimation("test_fall_anim");
            }

            // Air friction is applied while in the air.
            if (isFriction)
            {
                motion.x = Mathf.Lerp(motion.x, 0, airFriction);
            }
        }

        // Decides when to call flip method.
        if (moveDir < 0 && facingRight)
        {
            Flip();
        }

        if (moveDir > 0 && !facingRight)
        {
            Flip();
        }

        motion.z = 0;
        motion = MoveAndSlide(motion, Vector3.Up);
    }

    /// <summary>
    /// Subtracts the damage value from the player's current health.
    /// </summary>
    /// <param name="damageValue"> The value that will be used to lower the player's current hit point value. </param>
    public void Damage(int damageValue)
    {
        // don't apply damage if the player was recently damaged
        bool hasLock = false;
        Monitor.TryEnter(syncObject, TimeSpan.FromMilliseconds(1), ref hasLock);
        if (hasLock && !wasDamaged)
        {
            hitPoints -= damageValue;
            wasDamaged = true;
            GD.Print($"Damaged! Remaining HP {hitPoints}");

            // start a timer to re-enable damage
            // todo: invulnerability animation
            var task = Task.Factory.StartNew(() =>
            {
                // after the invicibility timeout, set was damaged to false allowing the player to be damaged again
                System.Threading.Thread.Sleep(invincibilityTimeMs);
                wasDamaged = false;

                // todo: stop invincibility animation
                GD.Print("Player can now be damaged.");
            });
        }
    }

    /// <summary>
    /// Called when cooldown timer hits 0. Resets player's ability to shoot.
    /// </summary>
    public void _on_Cooldown_timeout()
    {
        canShoot = true;
    }

    // Flips player model depending on direction faced.
    private void Flip()
    {
        graphics.RotateY(3.14159f);
        facingRight = !facingRight;
    }

    // Plays animations.
    private void PlayAnimation(String animation)
    {
        if (animPlayer.CurrentAnimation == animation)
        {
            return;
        }

        animPlayer.Play(animation);
    }

    // Logic for firing player's weapons.
    private void Shoot()
    {
        BulletController b = bullet.Instance() as BulletController;
        gun.AddChild(b);
        b.LookAt(GlobalTransform.origin, Vector3.Up);
        b.Shoot = true;
    }
}