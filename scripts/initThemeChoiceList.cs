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
	public GameManager instanceGM = new GameManager();
	
	

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		instanceGM.initAll();
		listTheme = instanceGM.getThemeList();
		GD.Print("nombre de themes : "+listTheme.Count);
		for (int i = 0; i <	listTheme.Count;i++)
		{
			GD.Print("nouveau bouton : "+listTheme[i]);
			 var myButton = new MyButton();
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
   }

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(double delta)
	{
	}

	
}
