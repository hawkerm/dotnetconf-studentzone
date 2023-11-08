using CommunityToolkit.Mvvm.Messaging;
using Godot;

namespace GodotPaddle.Game;

/// <summary>
/// This class is attached in the Main Scene to the 'Ball' node and 
/// represents the Ball which we're trying to stop with our Paddles. 
/// </summary>
public partial class Ball : RigidBody2D
{
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		ResetBall();
	}

	private void ResetBall()
	{
		GlobalPosition = GetViewportRect().Size / 2;

		// We want to apply an impulse (force) to our Ball when it is initialized
		// This will make it start going in a random direction when we start the game

		// First, we generate a random decimal between 0-1 and multiply it by 2*PI (360 degrees) to get an angle
		// We then rotate a unit vector by that random angle to get a random direction
		// Finally, we multiply the unit vector to give it magnitude and some oomph!
		ApplyImpulse(Vector2.Up.Rotated(GD.Randf() * 2 * Mathf.Pi) * 350f);
	}

    public override void _PhysicsProcess(double delta)
    {
        base._PhysicsProcess(delta);

		if (GlobalPosition.X < 0)
		{
			// Player 2 Scored
			ResetBall();
			StrongReferenceMessenger.Default.Send<PlayerScoredMessage>(new(2));
		}
		else if (GlobalPosition.X > GetViewportRect().Size.X)
		{
			// Player 1 Scored
			ResetBall();
			StrongReferenceMessenger.Default.Send<PlayerScoredMessage>(new(1));
		}
    }
}
