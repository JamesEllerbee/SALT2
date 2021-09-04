using System;
using Godot;

/// <summary>
/// Script used to control the player.
/// </summary>
public class PlayerController : KinematicBody
{
    // Variables
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

    private Vector3 motion;
    private bool facingRight = true;

    private AnimationPlayer animPlayer;
    private Spatial graphics;

    /// <summary>
    /// Gets a value indicating whether player is facing right.
    /// </summary>
    public bool GetFacingRight { get => facingRight; }

    /// <summary>
    /// Called when the node enters the scene tree for the first time.
    /// </summary>
    public override void _Ready()
    {
        graphics = (Spatial)GetNode("Graphics");
        animPlayer = (AnimationPlayer)graphics.GetNode("AnimationPlayer");
        MoveLockZ = true;
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
            if (isFriction == true)
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
}