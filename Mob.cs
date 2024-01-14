using Godot;
using System;

public partial class Mob : RigidBody2D
{
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		var animatedSprite2D = GetNode<AnimatedSprite2D>("AnimatedSprite2D");
		
		// Retrieve all registered frames (fly, swim, walk)
		string[] mobTypes = animatedSprite2D.SpriteFrames.GetAnimationNames();
		
		// Choose a random animation frame, set as the animation of the Mob
		// randi() % n selects a random integer between 0 and n-1
		animatedSprite2D.Play(mobTypes[GD.Randi() % mobTypes.Length]);
		
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
}

private void _on_visible_on_screen_notifier_2d_screen_exited()
{
	QueueFree();
}

}
