using Godot;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

public partial class QuizzManager : Node
{

	[Export]
	public Label scoreActuel;
	[Export]
	public Timer timer;

	[Export]
	public PopupPanel popupPanel;
	[Export]
	public Label popupLabel;

	[Export]
	TextEdit answerText;

	[Export]
	ItemList suggestionItemList;
/// <summary>

/// </summary>

	[Export]
	public Slider progressBar;
	[Export]
	public Slider volumeBar;
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

		audioStreamPlayer.VolumeDb = instanceGM.getVolume();
		volumeBar.Value = instanceGM.getVolume();

		questionActuel = instanceGM.getQuestion();
		if(questionActuel == null)
		{
			Node simultaneousScene = ResourceLoader.Load<PackedScene>("res://scene/endGame.tscn").Instantiate();
			GetTree().Root.AddChild(simultaneousScene);
			return;
		}
		scoreActuel.Text = "Score : "+instanceGM.getScore()+" / "+instanceGM.getNbQuestion();
		audioStreamPlayer.Stream = (AudioStream)ResourceLoader.Load(questionActuel[1]);
		listChoosen = instanceGM.getListChoosen();
		 
		 for (int i = 0;i< listChoosen.Count;i++)
		 {
			
			suggestionItemList.AddItem(listChoosen[i][0]);
		 }

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
		instanceGM.setVolume(value);
	}

private void OnTextChanged()
    {
        // Effacez les anciennes suggestions
        suggestionItemList.Clear();
		if( answerText.Text.Length > 0 )
		suggestionItemList.Visible = true;
		else
		suggestionItemList.Visible = false;
        // Obtenez le texte actuel dans le TextEdit
        string currentText = answerText.Text.ToLower();

        // Affichez les suggestions qui correspondent au texte actuel
        for (int i = 0;i< listChoosen.Count;i++)
		 {
			

            if (listChoosen[i][0].ToLower().Contains(currentText))
            {
                suggestionItemList.AddItem(listChoosen[i][0]);
            }
        }

    }

    private void OnSuggestionSelected(int index)
    {
        // Récupérez la suggestion sélectionnée dans l'ItemList
        string selectedSuggestion = suggestionItemList.GetItemText(index);

        // Remplacez le texte du TextEdit par la suggestion sélectionnée
        answerText.Text = selectedSuggestion;

    }


	private void _on_valid_button_pressed()
	{
		audioStreamPlayer.Stop();
		if(answerText.Text == questionActuel[0])
		{
			instanceGM.incrementScore();
			
			popupLabel.Text = questionActuel[0] ;
			popupLabel.Modulate = new Color(0,1,0,1);
			popupPanel.Show();
			
			timer.Start();
		}
		else
		{
			popupLabel.Text = questionActuel[0];
			popupLabel.Modulate = new Color(1,0,0,1);
			popupPanel.Show();
			timer.Start();
		}
	}

	private void _on_button_pop_up_pressed()
	{
		popupPanel.Hide();
		Node simultaneousScene = ResourceLoader.Load<PackedScene>("res://scene/quizz.tscn").Instantiate();
		GetTree().Root.AddChild(simultaneousScene);
	}

	private void _on_timer_timeout()
	{
		timer.Stop();
		popupPanel.Hide();
		Node simultaneousScene = ResourceLoader.Load<PackedScene>("res://scene/quizz.tscn").Instantiate();
		GetTree().Root.AddChild(simultaneousScene);
	}


	private void _on_btn_pressed_retour()
	{
		Node simultaneousScene = ResourceLoader.Load<PackedScene>("res://scene/menu_principal.tscn").Instantiate();
		GetTree().Root.AddChild(simultaneousScene);
	}



	

}
