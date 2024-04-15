using Godot;
using System;
using System.Collections.Generic;

public partial class GameManager : Node
{
	private GameManager instance;
	
	List<List<string>> animeData = new List<List<string>>();
	
	Dictionary<string, List<List<string>>> themeData = new Dictionary<string, List<List<string>>>();

	List<string> themeList = new List<string>();
	List<string> questionPulled = new List<string>();
	private List<List<string>> themeChosen;
	public GameManager getInstance()
	{
		if (instance == null)
		{
			instance = new GameManager();
		}
		return instance;
	}
	
	public void initAnimeData()
	{		
		animeData.Add(new List<string> { "Fullmetal Alchemist", "res://ressources/audio/anime/03 - again (TV Size) (YUI) ~ FMA Brotherhood (OST I) - [ZR].mp3" });
		animeData.Add(new List<string> { "Attack On Titan", "res://ressources/audio/anime/Attack On Titan Opening 1 Guren No Yumiya (Tv Size).mp3" });
		animeData.Add(new List<string> { "Beastars", "res://ressources/audio/anime/BEASTARS Season 2 OP (Clean)  Kaibutsu - YOASOBI  Netflix Anime.mp3" });
		animeData.Add(new List<string> { "Fire Force", "res://ressources/audio/anime/Fire Force - Opening  Inferno.mp3" });
		animeData.Add(new List<string> { "Great Teacher Onizuka", "res://ressources/audio/anime/GTO the Animation - Opening 1  Drivers High.mp3" });
		animeData.Add(new List<string> { "One Piece", "res://ressources/audio/anime/One Piece OP 1 - We Are! Lyrics.mp3" });
		animeData.Add(new List<string> { "Your Lie in April", "res://ressources/audio/anime/Shigatsu wa Kimi no Uso - Opening 1 - TV size with video.mp3" });
		animeData.Add(new List<string> { "Solo Leveling", "res://ressources/audio/anime/Solo Leveling - Opening  Level.mp3" });
		animeData.Add(new List<string> { "Black Clover", "res://ressources/audio/anime/Vicke Blanka - Black Catcher (Anime music video, TV anime「Black Clover」OP10 Theme).mp3" });
		animeData.Add(new List<string> { "Oshi no Ko", "res://ressources/audio/anime/YOASOBI  アイドル (Idol) TV Size Ver. Lyrics [Kan_Rom_Eng].mp3" });
	}
    
	public List<List<string>> getAnimeData()
	{
		return animeData;
	}

	public	void initThemeData()
	{
		themeData.Add("anime",animeData);
	}
	
	public void initThemeList()
	{
		foreach (string i in themeData.Keys)
		{
			themeList.Add(i);
		}
	}

	public List<string> getThemeList()
	{
		return themeList;
	}

	public string[] getQuestion()
	{
		string[] answer = new string[2];

		return answer;
	}



	public void setThemeChosen(string theme)
	{
		themeChosen = themeData[theme];
	}

	

}
