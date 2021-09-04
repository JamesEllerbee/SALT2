using System;
using Godot;

/// <summary>
/// Controls an instance of the Basic Ground Frog enemy.
/// </summary>
public class BasicGroundFrogController : KinematicBody
{
    // TODO: Use abstract frog class to store fields common across all frog enemy types.
    #region  Private Fields

    /// <summary>
    /// The moving period is how long this entity will move in a given direction.
    /// </summary>
    [Export]
    private long movingPeriod = 3000L;

    [Export]
    private float moveSpeed = 5;

    [Export]
    private float gravity = 9.8f;
    private float verticalVelocity = 0;

    private long changeDirectionMs;

    /// <summary>
    /// The direction modifier determines which direction the entity should begin moving in.
    /// </summary>
    [Export]
    private int directionModifier = 1;

    #endregion

    #region Public Properties

    /// <summary>
    /// Gets the value indicating the current direction this entity is facing.
    /// </summary>
    public int CurrentFacingDirection { get => directionModifier; }

    #endregion

    /// <inheritdoc/>
    public override void _Ready()
    {
        GD.Print($"Speed={moveSpeed}, Period={movingPeriod}");

        // set the inital change direction period.
        changeDirectionMs = GetNextChangeDirectionPeriod();
    }

    /// <inheritdoc/>
    public override void _Process(float delta)
    {
        // during the process method, apply movement along the "walk cycle"
        var currentTime = GetCurrentTime();
        if (IsTimeToChange(currentTime))
        {
            // update direction modifier
            directionModifier *= -1;

            // update next change directino interval
            changeDirectionMs = GetNextChangeDirectionPeriod();

            GD.Print("Basic ground frog changing direction");
        }
    }

    /// <inheritdoc/>
    public override void _PhysicsProcess(float delta)
    {
        base._PhysicsProcess(delta);

        // apply movement
        verticalVelocity -= gravity * delta;
        MoveAndSlide(new Vector3(directionModifier * moveSpeed, verticalVelocity, 0), Vector3.Up);

        if (IsOnFloor())
        {
            verticalVelocity = -0.1F;
        }
    }

    private long GetNextChangeDirectionPeriod()
    {
        return GetCurrentTime() + movingPeriod;
    }

    private long GetCurrentTime()
    {
        return DateTimeOffset.Now.ToUnixTimeMilliseconds();
    }

    private bool IsTimeToChange(long currentTime)
    {
        return changeDirectionMs - currentTime <= 0L;
    }
}