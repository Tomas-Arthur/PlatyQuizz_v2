using Godot;
using System;
using System.Collections.Generic;

public partial class initThemeChoiceList : Node
{

	[Signal]
	public delegate void checkedButtonEventHandler(string name);
	

	[Export]
	VBoxContainer vBoxContainer;

	List<string> listTheme = new List<string>();
	public GameManager instanceGM;
	
	

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		instanceGM = GameManager.getInstance();
		instanceGM.initAll();
		listTheme = instanceGM.getThemeList();
		GD.Print("nombre de themes : "+listTheme.Count);
		for (int i = 0; i <	listTheme.Count;i++)
		{
			GD.Print("nouveau bouton : "+listTheme[i]);
			 var myButton = new Button();
    		myButton.Text = listTheme[i];
    		vBoxContainer.AddChild(myButton);
			
			myButton.Pressed += () => button_Click(myButton.Text);
		}

	}

   private void button_Click(string name)
   {
		GD.Print("My name is : "+name);
		instanceGM.setThemeChosen(name);
		GD.Print("theme choosen : "+instanceGM.getThemeChosen());
		//string [] test = instanceGM.getQuestion();
		Node simultaneousScene = ResourceLoader.Load<PackedScene>("res://scene/quizz.tscn").Instantiate();
		GetTree().Root.AddChild(simultaneousScene);
   }

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(double delta)
	{
	}

	
}
