using Godot;
using System;
using System.Collections.Generic;
using System.IO;

public partial class settings : Node
{


	[Export]
	public PopupPanel popupPanel; 

	[Export]
	public Label popupLabel;

	[Export]
	public Timer timer;
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
		if(reponse.Text != "" & ressource.Text != "" & themeText.Text != "")
		{
			//GD.Print("copy de : "+ressource.Text+" vers : "+themeText.Text+ " avec comme reponse au quizz : "+reponse.Text);
			
			string destinationFile = Path.Combine(themeText.Text,Path.GetFileName(ressource.Text));
			string [] values = new string[2];
			values[0] = reponse.Text;
			values[1] = Path.GetFileName(ressource.Text);
			

			//GD.Print("destination file : "+System.Environment.CurrentDirectory+"/ressources/audio/"+destinationFile+"");
			//string currentDirectory = System.Environment.CurrentDirectory;
       		//GD.Print($"Le répertoire de travail actuel est : {currentDirectory}");
			if(!File.Exists(System.Environment.CurrentDirectory+"/ressources/audio/"+destinationFile))
			{
				on_pressed_add_theme();
				instanceGM.addToThemeData(themeText.Text,values);
				File.Copy(ressource.Text, System.Environment.CurrentDirectory+"/ressources/audio/"+destinationFile);
				popupLabel.Text = "Le fichier a été ajouté avec succès";
				popupPanel.Show();

				timer.Start();
				ressource.Text = "";
				reponse.Text = "";
				themeText.Text = "";
			}
			else
			{
				popupLabel.Text = "Le fichier existe deja";
				popupPanel.Show();
			
				timer.Start();
				ressource.Text = "";
				reponse.Text = "";
				themeText.Text = "";
			}

			instanceGM.reloadThemeData();
		}
		else
		{
			popupLabel.Text = "il manque des informations pour deplacer le fichier";
				popupPanel.Show();
			
				timer.Start();
				ressource.Text = "";
				reponse.Text = "";
				themeText.Text = "";
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
		instanceGM.reloadThemeData();
		instanceGM.saveVolume( audioStreamPlayer.VolumeDb);
		audioStreamPlayer.Stop();
		Node simultaneousScene = ResourceLoader.Load<PackedScene>("res://scene/menu_principal.tscn").Instantiate();
		GetTree().Root.AddChild(simultaneousScene);
	}

	private void on_pressed_add_theme()
	{
		if(themeText.Text != "")
		{
			string nomDossier = themeText.Text;
			if (!Directory.Exists(System.Environment.CurrentDirectory+"/ressources/audio/"+nomDossier))
			{ 
				GD.Print("on creer le dossier : "+nomDossier);
				Directory.CreateDirectory(System.Environment.CurrentDirectory+"/ressources/audio/"+nomDossier);
				instanceGM.addToThemeData(nomDossier);
				instanceGM.reloadThemeData();
				popupLabel.Text = "Le dossier a été ajouté avec succès";
				popupPanel.Show();
			
				timer.Start();
			}
			else
			{
				popupLabel.Text = "Le dossier existe déjà";
				popupPanel.Show();
			
				timer.Start();
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
		if(suggestionItemList.ItemCount == 0)
		{
			suggestionItemList.Visible = false;
		}
	}
	private void OnSuggestionSelected(int index)
	{
		// Récupérez la suggestion sélectionnée dans l'ItemList
		string selectedSuggestion = suggestionItemList.GetItemText(index);

		// Remplacez le texte du TextEdit par la suggestion sélectionnée
		themeText.Text = selectedSuggestion;
		suggestionItemList.Visible = false;
	}

	public void _on_timer_timeout()
	{
		timer.Stop();
		popupPanel.Hide();
	}
	
}
