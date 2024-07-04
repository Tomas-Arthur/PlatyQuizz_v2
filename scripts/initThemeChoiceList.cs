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
		//instanceGM.initAll();
		listTheme = instanceGM.getThemeList();
		

		
		for (int i = 0; i <	listTheme.Count;i++)
		{
			GD.Print("nouveau bouton : "+listTheme[i]);
			 var myButton = new Button();
    		myButton.Text = listTheme[i];
    		vBoxContainer.AddChild(myButton);
			if(instanceGM.getNombreQuestion(listTheme[i]) > 0)
			{
				myButton.Pressed += () => button_Click(myButton.Text);
			}
			else
			{
				myButton.Disabled = true;
			}
			
		}

	}

   private void button_Click(string name)
   {
		GD.Print("My name is : "+name);
		instanceGM.setThemeChosen(name);
		GD.Print("theme choosen : "+instanceGM.getThemeChosen());
		//string [] test = instanceGM.getQuestion();
		instanceGM.resetVariables();
		Node simultaneousScene = ResourceLoader.Load<PackedScene>("res://scene/quizz.tscn").Instantiate();
		GetTree().Root.AddChild(simultaneousScene);
   }

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(double delta)
	{
	}

	private void _on_btn_pressed_retour()
	{
		Node simultaneousScene = ResourceLoader.Load<PackedScene>("res://scene/menu_principal.tscn").Instantiate();
		GetTree().Root.AddChild(simultaneousScene);
	}
	

	private void on_pressed_save_to_json()
	{
		instanceGM.saveDictionaryToJSON();
	}

}
