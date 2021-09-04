using System;
using Godot;

/// <summary>
/// The PlayerController script.
/// </summary>
public class PlayerController : KinematicBody
{
    // Variables
    [Export]
    private float moveSpeed = 5;

    [Export] private float jumpForce = 10;

    [Export]
    private float gravity = 9.8f;

    [Export]
    private float maxFallSpeed = 100;

    private float yVelocity = 0;
    private bool facingRight = false;

    private AnimationPlayer animPlayer;
    private Spatial graphics;

    /// <summary>
    /// Called when the node enters the scene tree for the first time.
    /// </summary>
    public override void _Ready()
    {
        graphics = (Spatial)GetNode("Graphics");
        animPlayer = (AnimationPlayer)graphics.GetNode("AnimationPlayer");
    }

    /// <summary>
    /// Called during physics process step of primary game loop.
    /// </summary>
    /// <param name="delta">
    /// Time elapsed in seconds since the previous call to _process().
    /// </param>
    public override void _PhysicsProcess(float delta)
    {
        base._PhysicsProcess(delta);
        int move_dir = 0;

        if (Input.IsActionPressed("move_right"))
        {
            move_dir += 1;
        }

        if (Input.IsActionPressed("move_left"))
        {
            move_dir -= 1;
        }

        MoveAndSlide(new Vector3(move_dir * moveSpeed, yVelocity, 0), Vector3.Up);

        bool just_jumped = false;
        bool grounded = IsOnFloor();
        yVelocity -= gravity * delta;

        if (yVelocity < -maxFallSpeed)
        {
            yVelocity = -maxFallSpeed;
        }

        if (grounded)
        {
            yVelocity = -0.1f;
            if (Input.IsActionPressed("move_jump"))
            {
                yVelocity = jumpForce;
                just_jumped = true;
            }
        }

        if (move_dir < 0 && facingRight)
        {
            Flip();
        }

        if (move_dir > 0 && !facingRight)
        {
            Flip();
        }

        if (just_jumped)
        {
            Play_anim("test_jump_anim");
        }
    }

    // Flips player model depending on direction faced.
    private void Flip()
    {
        Console.WriteLine("FLIP!");
        graphics.RotateY(-1f);
        facingRight = !facingRight;
    }

    // Plays animations.
    private void Play_anim(String animation)
    {
        if (animPlayer.CurrentAnimation == animation)
        {
            return;
        }

        animPlayer.Play(animation);
    }
}
