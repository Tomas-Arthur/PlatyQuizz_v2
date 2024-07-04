using Godot;
using System;
using System.Collections.Generic;

public partial class endGame : Node
{
	[Export]
	public Label valeurScore;

	GameManager instanceGM;
	private List<string[]> listChoosen;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		instanceGM = GameManager.getInstance();
		listChoosen = instanceGM.getListChoosen();
		valeurScore.Text = instanceGM.getScore().ToString() + " / "+instanceGM.getNbQuestionMax().ToString();
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	private void _on_buttonquitter_pressed()
	{
		GetTree().Quit();
	}

	private void _on_buttonretour_pressed()
	{
		//Node simultaneousScene = ResourceLoader.Load<PackedScene>("res://scene/themeChoice.tscn").Instantiate();
		//GetTree().Root.AddChild(simultaneousScene);
		GetTree().ChangeSceneToFile("res://scene/themeChoice.tscn");
	}
}
