using Godot;
using System;
using System.Collections.Generic;

public partial class GameManager : Node
{
	private static GameManager instance;
	public int nbQuestion = 0;
	List<string[]> animeData = new List<string[]>();
	List<string[]> listChoosen;
	private int getQuestionCount =0;
	Dictionary<string, List<string[]>> themeData = new Dictionary<string, List<string[]>>();

	List<string> themeList = new List<string>();
	List<string> questionPulled = new List<string>();
	private string themeChosen;

	private int score =0;
	private float volume;
	

	public static GameManager getInstance()
	{
		if (instance == null)
		{
			instance = new GameManager();
			instance.initAll();
		}
		return instance;
	}
	
	public void initAnimeData()
	{		
		animeData.Add(new string[2] { "Fullmetal Alchemist", "03 - again (TV Size) (YUI) ~ FMA Brotherhood (OST I) - [ZR].mp3" });
		animeData.Add(new string[2] { "Attack On Titan", "Attack On Titan Opening 1 Guren No Yumiya (Tv Size).mp3" });
		animeData.Add(new string[2] { "Beastars", "BEASTARS Season 2 OP (Clean)  Kaibutsu - YOASOBI  Netflix Anime.mp3" });
		animeData.Add(new string[2] { "Fire Force", "Fire Force - Opening  Inferno.mp3" });
		animeData.Add(new string[2] { "Great Teacher Onizuka", "GTO the Animation - Opening 1  Drivers High.mp3" });
		animeData.Add(new string[2] { "One Piece", "One Piece OP 1 - We Are! Lyrics.mp3" });
		animeData.Add(new string[2] { "Your Lie in April", "Shigatsu wa Kimi no Uso - Opening 1 - TV size with video.mp3" });
		animeData.Add(new string[2] { "Solo Leveling", "Solo Leveling - Opening  Level.mp3" });
		animeData.Add(new string[2] { "Black Clover", "Vicke Blanka - Black Catcher (Anime music video, TV anime「Black Clover」OP10 Theme).mp3" });
		animeData.Add(new string[2] { "Oshi no Ko", "YOASOBI  アイドル (Idol) TV Size Ver. Lyrics [Kan_Rom_Eng].mp3" });
	}
    
	public List<string[]> getAnimeData()
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
		Random i = new Random();
		int value = i.Next(0,listChoosen.Count);
		//GD.Print("listChoosen["+value+"][0] : "+listChoosen[value][0]);
		if(!questionPulled.Contains( listChoosen[value][0]))
		{
			answer[0]=listChoosen[value][0];
			answer[1]="res://ressources/audio/"+themeChosen+"/"+listChoosen[value][1];
			//GD.Print("valeur de answer : "+answer[0]+" "+answer[1]);
			questionPulled.Add(answer[0]);
			nbQuestion ++;
			return answer;
		}
		else
		{
			if(getQuestionCount < 1000)
			{
				getQuestionCount++;
				return getQuestion();
			}
			else
			{
				
				return null;
			}
		}
	}



	public void setThemeChosen(string theme)
	{
		themeChosen = theme;
		listChoosen = themeData[theme];
	}

	public string getThemeChosen()
	{
		return themeChosen ;
	}
	public void initAll()
	{
		initAnimeData();
		initThemeData();
		initThemeList();
	}

	public void resetVariables()
	{
		questionPulled.Clear();
		getQuestionCount =0;
		score =0;
		nbQuestion =0;
	}
	public List<string[]> getListChoosen()
	{
		return listChoosen;
	}

	public int getScore()
	{
		return score;
	}
	public void incrementScore()
	{
		score++;
	}

	public float getVolume()
	{
		return volume;
	}
	public void setVolume(float newVolume)
	{
		volume = newVolume;
	}

	public int getNbQuestion()
	{
		return nbQuestion;
	}
}
