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
    private float maxCrouchSpeed = 2;
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
    private int deathSequenceTimeMs = 3000;
    private float currentSpeed;

    private Vector3 motion;
    private bool canShoot = true;
    private bool facingRight = true;
    private bool isCrouching = false;
    private bool isShooting = false;
    private bool wasRecentlyDamaged = false;
    private bool inDeathSequence = false;
    private int maxHp;
    private object damagedSyncObject = new object();
    private object deathSequenceSyncObject = new object();

    private Spatial ticket1;
    private Spatial ticket2;
    private Spatial ticket3;

    private AnimationPlayer animPlayer;
    private Spatial graphics;
    private Position3D gun;
    private Godot.Timer cdTimer;
    private CollisionShape standingShape;
    private CollisionShape crouchingShape;
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
        animPlayer = (AnimationPlayer)graphics.GetNode("SaltMare/AnimationPlayer");
        animPlayer.GetAnimation("idle").Loop = true;
        animPlayer.GetAnimation("crouchStay").Loop = true;
        animPlayer.GetAnimation("pain").Loop = true;
        animPlayer.Play("idle");
        gun = (Position3D)graphics.GetNode("Gun");
        cdTimer = (Godot.Timer)gun.GetNode("Cooldown");
        standingShape = (CollisionShape)GetNode("CollisionShape");
        crouchingShape = (CollisionShape)GetNode("CrouchingShape");
        bullet = (PackedScene)ResourceLoader.Load("res://Scenes/BULLET.tscn");
        MoveLockZ = true;

        maxHp = hitPoints;
        currentSpeed = maxSpeed;

        ticket1 = (Spatial)GetNode("/root/Main/Camera/Ticket1");
        ticket2 = (Spatial)GetNode("/root/Main/Camera/Ticket2");
        ticket3 = (Spatial)GetNode("/root/Main/Camera/Ticket3");
    }

    /// <inheritdoc/>
    public override void _Process(float delta)
    {
        base._Process(delta);

        // When fire is pressed, spawn new bullet and shoot.
        if (Input.IsActionPressed("action_shoot") && canShoot && !inDeathSequence)
        {
            Shoot();
            cdTimer.Start();
            canShoot = false;
        }

        // When down is pressed, make player crouch.
        if (Input.IsActionPressed("move_down"))
        {
            currentSpeed = maxCrouchSpeed;
            crouchingShape.Disabled = false;
            standingShape.Disabled = true;
            isCrouching = true;
        }
        else
        {
            currentSpeed = maxSpeed;
            standingShape.Disabled = false;
            crouchingShape.Disabled = true;
            isCrouching = false;
        }

        // if the player's remaining hp is less than zero and the player is not already in a death sequence.
        if (hitPoints <= 0 && !inDeathSequence)
        {
            bool haveLock = false;
            Monitor.TryEnter(deathSequenceSyncObject, TimeSpan.FromMilliseconds(1), ref haveLock);

            if (haveLock)
            {
                inDeathSequence = true;

                // trigger death and respawn.
                var deathTimers = Task.Factory.StartNew(() =>
                {
                    animPlayer.Play("pain");
                    motion = Vector3.Zero;

                    // pause for three sceonds before updating the ui and resetting the player's hitpoints.
                    System.Threading.Thread.Sleep(deathSequenceTimeMs);

                    UpdateUIHp(true);
                    hitPoints = maxHp;
                    inDeathSequence = false;

                    // todo: add invinsibility timer
                });
            }
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
        if (Input.IsActionPressed("move_right") && !inDeathSequence)
        {
            motion.x = Math.Min(motion.x + acceleration, currentSpeed);
            moveDir += 1;
            if (!isShooting)
            {
                if (isCrouching)
                {
                    animPlayer.Play("crouchWalk");
                }
                else
                {
                    animPlayer.Play("run");
                }
            }
        }
        else if (Input.IsActionPressed("move_left") && !inDeathSequence)
        {
            motion.x = Math.Max(motion.x - acceleration, -currentSpeed);
            moveDir -= 1;
            if (!isShooting)
            {
                if (isCrouching)
                {
                    animPlayer.Play("crouchWalk");
                }
                else
                {
                    animPlayer.Play("run");
                }
            }
        }
        else
        {
            isFriction = true;
            if (!isShooting && !inDeathSequence)
            {
                if (isCrouching)
                {
                    animPlayer.Play("crouchStay");
                }
                else
                {
                    animPlayer.Play("idle");
                }
            }
        }

        // Jump logic. Checks if player is on floor before deciding if it is ok to jump.
        // If player is in the air, play jump animation. Additionally, less friction is
        // applied to the player while in the air.
        if (IsOnFloor() && !inDeathSequence)
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
        else if (!inDeathSequence)
        {
            // Play jump animation if going up.
            if (motion.y > 0)
            {
                // jump
            }

            // Play falling animation if going down.
            else
            {
                // fall
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
        bool haveLock = false;
        Monitor.TryEnter(damagedSyncObject, TimeSpan.FromMilliseconds(1), ref haveLock);
        if (haveLock && !wasRecentlyDamaged)
        {
            hitPoints -= damageValue;
            wasRecentlyDamaged = true;
            GD.Print($"Damaged! Remaining HP {hitPoints}");

            UpdateUIHp();

            // start a timer to re-enable damage
            // todo: invulnerability animation
            var damageTimer = Task.Factory.StartNew(() =>
            {

                // after the invicibility timeout, set was damaged to false allowing the player to be damaged again
                System.Threading.Thread.Sleep(invincibilityTimeMs);
                wasRecentlyDamaged = false;

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

    /// <summary>
    /// Used to detect end of shooting animations.
    /// </summary>
    /// <param name="animation">The animation that has finished.</param>
    public void _on_AnimationPlayer_animation_finished(String animation)
    {
        if (animation == "shoot" || animation == "crouchShoot")
        {
            isShooting = false;
        }
    }

    // Flips player model depending on direction faced.
    private void Flip()
    {
        graphics.RotateY(3.14159f);
        facingRight = !facingRight;
    }

    // Logic for firing player's weapons.
    private void Shoot()
    {
        BulletController b = bullet.Instance() as BulletController;
        gun.AddChild(b);
        b.LookAt(GlobalTransform.origin, Vector3.Up);
        b.Shoot = true;
        isShooting = true;
        if (isCrouching)
        {
            animPlayer.Play("crouchShoot");
        }
        else
        {
            animPlayer.Play("shoot");
        }
    }

    private void UpdateUIHp(bool reset = false)
    {
        if (!reset)
        {
            if (ticket3.Visible)
            {
                ticket3.Visible = false;
            }
            else if (ticket2.Visible)
            {
                ticket2.Visible = false;
            }
            else if (ticket1.Visible)
            {
                ticket1.Visible = false;
            }
        }
        else
        {
            ticket1.Visible = true;
            ticket2.Visible = true;
            ticket3.Visible = true;
        }
    }
}