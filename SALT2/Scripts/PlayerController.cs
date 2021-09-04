using Godot;
using System;

public class PlayerController : KinematicBody
{
    // Variables
    [Export] private float GRAVITY = 9.8f;
    [Export] private float MAX_SPEED = 10;
    [Export] private float JUMP_HEIGHT = 100;
    [Export] private float ACCELERATION = 5;
    [Export] private float FRICTION = 0.2f;
    [Export] private float AIR_FRICTION = 0.05f;

    //private float y_velocity = 0;
    private Vector3 motion;
    private bool facing_right = false;

    private AnimationPlayer anim_player;
    private Spatial graphics;

    /// <summary>
    /// Called when the node enters the scene tree for the first time.
    /// </summary>
    public override void _Ready()
    {
        graphics = (Spatial)GetNode("Graphics");
        anim_player = (AnimationPlayer)graphics.GetNode("AnimationPlayer");
    }

    /// <summary>
    /// Called during physics process step of primary game loop.
    /// </summary>
    /// <param name="delta">
    /// Time elapsed in seconds since the previous call to _process()
    /// </param>
    public override void _PhysicsProcess(float delta)
    {
        base._PhysicsProcess(delta);

        motion.y -= GRAVITY * delta;
        bool friction = false;
        int move_dir = 0;

        // Check for player directional inputs to add or subtract acceleration.
        if(Input.IsActionPressed("move_right"))
        {
            motion.x = Math.Min((motion.x + ACCELERATION), MAX_SPEED);
            move_dir += 1;
            // TODO: Add walk animation.
        }
        else if (Input.IsActionPressed("move_left"))
        {
            motion.x = Math.Max((motion.x - ACCELERATION), -MAX_SPEED);
            move_dir -= 1;
            // TODO: Add walk animation.
        }
        else
        {
            friction = true;
            // TODO: Add idle animation.
        }

        // Jump logic. Checks if player is on floor before deciding if it is ok to jump.
        // If player is in the air, play jump animation. Additionally, less friction is
        // applied to the player while in the air.
        if (IsOnFloor())
        {
            if (Input.IsActionPressed("move_jump"))
            {
                motion.y = JUMP_HEIGHT;
            }
            if (friction == true)
            {
                motion.x = Mathf.Lerp(motion.x, 0, FRICTION);
            }
        }
        else
        {
            // Play jump animation if going up.
            if (motion.y > 0)
            {
                play_anim("test_jump_anim");
            }
            // Play falling animation if going down.
            else
            {
                play_anim("test_fall_anim");
            }
            
            // Air friction is applied while in the air.
            if (friction == true)
            {
                motion.x = Mathf.Lerp(motion.x, 0, AIR_FRICTION);
            }
        }

        // Decides when to call flip method.
        if(move_dir < 0 && facing_right)
        {
            flip();
        }
        if (move_dir > 0 && !facing_right)
        {
            flip();
        }

        motion = MoveAndSlide(motion, Vector3.Up);
    }

    // Flips player model depending on direction faced.
    private void flip()
    {
        //Console.WriteLine("FLIP!");
        graphics.RotateY(-1f);
        facing_right = !facing_right;
    }

    // Plays animations.
    private void play_anim(String animation)
    {
        if(anim_player.CurrentAnimation == animation)
        {
            return;
        }
        anim_player.Play(animation);
    }
}
