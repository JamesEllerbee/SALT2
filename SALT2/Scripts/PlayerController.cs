using Godot;
using System;

public class PlayerController : KinematicBody
{
    // Variables
    public const float MOVE_SPEED = 5;
    public const float JUMP_FORCE = 10;
    public const float GRAVITY = 9.8f;
    public const float MAX_FALL_SPEED = 100;

    float y_velocity = 0;
    bool facing_right = false;

    AnimationPlayer anim_player;
    Spatial graphics;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        graphics = (Spatial)GetNode("Graphics");
        anim_player = (AnimationPlayer)graphics.GetNode("AnimationPlayer");
    }

    // Called during physics process step of primary game loop.
    public override void _PhysicsProcess(float delta)
    {
        base._PhysicsProcess(delta);
        int move_dir = 0;

        if(Input.IsActionPressed("move_right"))
        {
            move_dir += 1;
        }
        if (Input.IsActionPressed("move_left"))
        {
            move_dir -= 1;
        }

        MoveAndSlide(new Vector3(move_dir * MOVE_SPEED, y_velocity, 0), Vector3.Up);

        bool just_jumped = false;
        bool grounded = IsOnFloor();
        y_velocity -= GRAVITY * delta;

        if(y_velocity < -MAX_FALL_SPEED)
        {
            y_velocity = -MAX_FALL_SPEED;
        }

        if(grounded)
        {
            y_velocity = -0.1f;
            if(Input.IsActionPressed("move_jump"))
            {
                y_velocity = JUMP_FORCE;
                just_jumped = true;
            }
        }

        if(move_dir < 0 && facing_right)
        {
            flip();
        }
        if(move_dir > 0 && !facing_right)
        {
            flip();
        }

        if(just_jumped)
        {
            play_anim("test_jump_anim");
        }
    }

    // Flips player model depending on direction faced.
    public void flip()
    {
        Console.WriteLine("FLIP!");
        graphics.RotateY(-1f);
        facing_right = !facing_right;
    }

    // Plays animations.
    public void play_anim(String animation)
    {
        if(anim_player.CurrentAnimation == animation)
        {
            return;
        }
        anim_player.Play(animation);
    }
}
