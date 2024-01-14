using Godot;
using System;

public partial class Player : Area2D
{
	[Export] // Keyword allows value set in inspector
	public int speed { get; set; } = 500; // Speed of Player object (pixels/sec)
	public Vector2 ScreenSize; // Game window size
	
	
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		ScreenSize = GetViewportRect().Size;
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		var velocity = Vector2.Zero; // Movement vector for player object
		
		// Start movement in a cardinal direction
		// Up
		if (Input.IsActionPressed("move_up")) 
		{
			velocity.Y += -1;
		};
		// Down
		if (Input.IsActionPressed("move_down")) 
		{
			velocity.Y += 1;
		};
				// Left
		if (Input.IsActionPressed("move_left")) 
		{
			velocity.X += -1;
		};
		// Right
		if (Input.IsActionPressed("move_right")) 
		{
			velocity.X += 1;
		};
		
		var animatedSprite2D = GetNode<AnimatedSprite2D>("AnimatedSprite2D");
		// If there is movement, play movement animation.
		if (velocity.Length() > 0) 
		{ 
			velocity = velocity.Normalized() * speed;
			animatedSprite2D.Play();
		} 
		else // If movement done, stop the animation of the Sprite.
		{
			animatedSprite2D.Stop();
		}
		
		Position += velocity * (float)delta;
		
		// Limit(Clamp) the Position X/Y between 0 and ScreenSize.X/Y
		// Prevents player sprite from moving off the screen.
		Position = new Vector2(
			x: Mathf.Clamp(Position.X, 0, ScreenSize.X),
			y: Mathf.Clamp(Position.Y, 0, ScreenSize.Y)
		);
		
		// Change animation used depending on the movement vector
		if (velocity.X != 0) // Vertical movement
		{
			animatedSprite2D.Animation = "walk";
			
			// Change animation direction either horisontally or vertically
			animatedSprite2D.FlipV = false; // Method to flip the animated vertically
			animatedSprite2D.FlipH = velocity.X < 0;
		} 
		else if (velocity.Y != 0) // Horisontal movement
		{
			animatedSprite2D.Animation = "up";
			animatedSprite2D.FlipV = velocity.Y > 0;
		}
	}
}
