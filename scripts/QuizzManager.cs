using Godot;
using System;
using System.Collections.Generic;
using System.IO;

public partial class QuizzManager : Node
{

[Export]
TextEdit textEdit;
[Export]

    Popup suggestionPopup;
	[Export]

    ItemList suggestionItemList;
List<string> suggestions = new List<string>()
    {
        "Suggestion 1",
        "Suggestion 2",
        "Suggestion 3"
    };
/// <summary>

/// </summary>

	[Export]
	public Slider progressBar;
	[Export]
	public Button playButton;
	[Export]
	public Button pauseButton;
	[Export]
	public Button stopButton;
	
	[Export]
	public AudioStreamPlayer audioStreamPlayer;
	[Export]
	public MenuBar menuBar;
	public GameManager instanceGM ;
	private List<string[]> listChoosen;
	private string[] questionActuel ;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		instanceGM = GameManager.getInstance();
		playButton.Disabled = false;
		pauseButton.Disabled = true;
		stopButton.Disabled = true;


		questionActuel = instanceGM.getQuestion();
		audioStreamPlayer.Stream = (AudioStream)ResourceLoader.Load(questionActuel[1]);
		listChoosen = instanceGM.getListChoosen();
		 // Connectez le signal item_selected à la méthode de gestion de l'événement
       // suggestionItemList.ItemSelected +=  () => OnSuggestionSelected(1);

	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		progressBarUpdate();
	}


	private void _on_play_button_pressed()
	{
		if(audioStreamPlayer.StreamPaused)
		{
			GD.Print("Playing");
			audioStreamPlayer.Play(audioStreamPlayer.GetPlaybackPosition());
			playButton.Disabled = true;
			pauseButton.Disabled = false;
			stopButton.Disabled = false;
		}
		else
		{
			GD.Print("Playing");
			audioStreamPlayer.Play();
			playButton.Disabled = true;
			pauseButton.Disabled = false;
			stopButton.Disabled = false;
		}
	}

	private void _on_pause_button_pressed()
	{
		GD.Print("Paused");
		audioStreamPlayer.StreamPaused = true;
		playButton.Disabled = false;
		pauseButton.Disabled = true;
		stopButton.Disabled = false;
	}

	private void _on_stop_button_pressed()
	{
		GD.Print("Stoped");
		audioStreamPlayer.Stop();
		playButton.Disabled = false;
		pauseButton.Disabled = true;
		stopButton.Disabled = true;
	}

	private void progressBarUpdate()
	{
		if (audioStreamPlayer.HasStreamPlayback())
		{
			progressBar.Value = (audioStreamPlayer.GetPlaybackPosition()* progressBar.MaxValue)/audioStreamPlayer.Stream.GetLength();
		}
	}

	private void _on_volume_bar_value_changed(float value)
	{
		audioStreamPlayer.VolumeDb = value;
	}

private void OnTextChanged()
    {
        // Effacez les anciennes suggestions
        suggestionItemList.Clear();

        // Obtenez le texte actuel dans le TextEdit
        string currentText = textEdit.Text.ToLower();

        // Affichez les suggestions qui correspondent au texte actuel
        foreach (string suggestion in suggestions)
        {
            if (suggestion.ToLower().StartsWith(currentText))
            {
                suggestionItemList.AddItem(suggestion);
            }
        }

        // Afficher le Popup si des suggestions sont disponibles
        suggestionPopup.Visible = suggestionItemList.ItemCount > 0;
    }

    private void OnSuggestionSelected(int index)
    {
        // Récupérez la suggestion sélectionnée dans l'ItemList
        string selectedSuggestion = suggestionItemList.GetItemText(index);

        // Remplacez le texte du TextEdit par la suggestion sélectionnée
        textEdit.Text = selectedSuggestion;

        // Masquez le Popup
        suggestionPopup.Visible = false;
    }
}
