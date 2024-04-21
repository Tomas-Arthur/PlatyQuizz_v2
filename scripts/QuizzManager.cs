using Godot;
using System;

public partial class QuizzManager : Node
{


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


	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		playButton.Disabled = false;
		pauseButton.Disabled = true;
		stopButton.Disabled = true;
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

}
