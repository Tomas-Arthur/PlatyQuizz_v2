using Godot;
using System;
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
		if(reponse.Text != null & ressource.Text != null & cible.Text != null)
		{
			GD.Print("copy de : "+ressource.Text+" vers : "+cible.Text+ " avec comme reponse au quizz : "+reponse.Text);
			string destinationFile = Path.Combine(cible.Text,Path.GetFileName(ressource.Text));
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
}
