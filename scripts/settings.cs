using Godot;
using System;
using System.Collections.Generic;
using System.IO;

public partial class settings : Node
{
		public GameManager instanceGM ;
	[Export]
	public FileDialog fileDialogRessources;
	[Export]
	TextEdit reponse;
	[Export]
	TextEdit ressource;

	[Export]
	TextEdit cible;
	[Export]
	FileDialog fileDialogCible;
	private List<string> listTheme;

	[Export]
	TextEdit themeText;

	[Export]
	ItemList suggestionItemList;

	// settings son
	[Export]
	AudioStreamPlayer audioStreamPlayer;
	[Export]
	public Slider volumeBar;
	

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		instanceGM = GameManager.getInstance();
		volumeBar.Value = instanceGM.getVolume();
		audioStreamPlayer.VolumeDb = instanceGM.getVolume();
		listTheme = instanceGM.getThemeList();
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	private void _on_test_otest_button_pressed()
	{
		fileDialogRessources.Show();
	}

	private void onFileSelected(string path)
	{
		ressource.Text = path;
	}

	private void _on_add_button_pressed()
	{
		if(reponse.Text != null & ressource.Text != null & themeText.Text != null)
		{
			GD.Print("copy de : "+ressource.Text+" vers : "+themeText.Text+ " avec comme reponse au quizz : "+reponse.Text);
			string destinationFile = Path.Combine(themeText.Text,Path.GetFileName(ressource.Text));
			//GD.Print(""+destinationFile+"");
			File.Copy(ressource.Text, destinationFile);
		}
	}

	private void _on_btn_cible_pressed()
	{
		fileDialogCible.Show();
	}

	private void _on_file_d_emplacement_dir_selected(string path)
	{
		cible.Text = path;
	}



	private void _on_volume_bar_value_changed(float value)
	{
		
		audioStreamPlayer.VolumeDb = value;
		instanceGM.setVolume(value);
	}

	private void _on_Pressed_To_Play_Test()
	{
		audioStreamPlayer.Play();
	}

	private void _on_Pressed_To_Stop_Test()
	{
		audioStreamPlayer.Stop();
	}


	private void _on_btn_pressed_retour()
	{
		Node simultaneousScene = ResourceLoader.Load<PackedScene>("res://scene/menu_principal.tscn").Instantiate();
		GetTree().Root.AddChild(simultaneousScene);
	}

	private void on_pressed_add_theme()
	{
		if(themeText.Text != "")
		{
			if (!Directory.Exists("res://ressources/audio/"+themeText.Text))
			{
				Directory.CreateDirectory("res://ressources/audio/"+themeText.Text);
			}
		}
	}

	private void _on_answer_text_changed()
	{
		 // Effacez les anciennes suggestions
        suggestionItemList.Clear();
		if( themeText.Text.Length > 0 )
		suggestionItemList.Visible = true;
		else
		suggestionItemList.Visible = false;
        // Obtenez le texte actuel dans le TextEdit
        string currentText = themeText.Text.ToLower();

        // Affichez les suggestions qui correspondent au texte actuel
        for (int i = 0;i< listTheme.Count;i++)
		 {
			

            if (listTheme[i].ToLower().Contains(currentText))
            {
                suggestionItemList.AddItem(listTheme[i]);
            }
        }
	}
}
