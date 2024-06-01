using Godot;
using System;

public partial class changementScene : Node
{

	private GameManager instanceGM;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		instanceGM = GameManager.getInstance();
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}


	private void _on_play_button_pressed()
	{
		
		Node simultaneousScene = ResourceLoader.Load<PackedScene>("res://scene/themeChoice.tscn").Instantiate();
		GetTree().Root.AddChild(simultaneousScene);
	}


	private void _on_exit_button_pressed()
	{
		GetTree().Quit();
	}

	private void _on_setting_button_pressed()
	{
		
		Node simultaneousScene = ResourceLoader.Load<PackedScene>("res://scene/Settings.tscn").Instantiate();
		GetTree().Root.AddChild(simultaneousScene);
	}
}
