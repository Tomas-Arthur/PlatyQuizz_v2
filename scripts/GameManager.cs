using Godot;
using System;
using System.Collections.Generic;
using System.Text.Json;
public partial class GameManager : Node
{
	private static GameManager instance;
	public int nbQuestion = 1;

	public int nbQuestionMax = 10;
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
	
	public List<string[]> getAnimeData()
	{
		return animeData;
	}


	
	public void addToThemeData(string theme,string[] values)
	{
		List<string[]> themeValue = new List<string[]>();
		if( !themeData.ContainsKey(theme))
		{
			
			themeValue.Add(values);
			themeData.Add(theme,themeValue);
		}
		else
		{
			themeValue = themeData[theme];
			themeValue.Add(values);
			themeData[theme] = themeValue ;
		}
	}
		public void addToThemeData(string theme)
	{
		List<string[]> themeValue = new List<string[]>();
		if( !themeData.ContainsKey(theme))
		{
			//themeValue.Add(values);
			themeData.Add(theme,themeValue);
			
		}
	}

	public void initThemeList()
	{
		themeList.Clear();
		foreach (string i in themeData.Keys)
		{
			themeList.Add(i);
		}
	}

	public int getNombreQuestion(string theme)
	{

		return themeData[theme].Count;
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
		
		if(!questionPulled.Contains( listChoosen[value][0]))
		{
			answer[0]=listChoosen[value][0];
			answer[1]=System.Environment.CurrentDirectory+"/ressources/audio/"+themeChosen+"/"+listChoosen[value][1];
			//res://ressources/audio/anime/03 - again (TV Size) (YUI) ~ FMA Brotherhood (OST I) - [ZR].mp3
			questionPulled.Add(answer[0]);
			//nbQuestion ++;
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
		getDataFromJSON();
		initThemeList();
		getSettingsFromJSON();
	}

	public void resetVariables()
	{
		questionPulled.Clear();
		getQuestionCount =0;
		score =0;
		nbQuestion =1;
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

	public void incrementNbQuestion()
	{
		nbQuestion++;
	}

	public void saveDictionaryToJSON()
	{
				// Convertir les données en JSON
		string jsonString = JsonSerializer.Serialize(themeData);

		// Écrire le JSON dans un fichier
		using (FileAccess file = FileAccess.Open(System.Environment.CurrentDirectory+"/ressources/saves/themeData.json", FileAccess.ModeFlags.Write))
		{
			file.StoreString(jsonString);
		}
	}

	private void getDataFromJSON()
	{
		// Lire le fichier JSON
		using (FileAccess file = FileAccess.Open(System.Environment.CurrentDirectory+"/ressources/saves/themeData.json", FileAccess.ModeFlags.Read))
		{
			if (file != null)
			{
				string jsonString = file.GetAsText();
				themeData = JsonSerializer.Deserialize<Dictionary<string, List<string[]>>>(jsonString);
			}
		}
		foreach (var item in themeData)
		{
			//GD.Print(item);
		}
	}

	public void reloadThemeData()
	{
		saveDictionaryToJSON();
		getDataFromJSON();
		initThemeList();
	}

	public void saveVolumeAndNbQuestion(float volume,string nbQuestion)
	{
		string[] volumeAndNbQuestionToSave = new string[4];
		volumeAndNbQuestionToSave[0] = "volume";
		volumeAndNbQuestionToSave[1] = volume.ToString();
		volumeAndNbQuestionToSave[2] = "nbQuestionParQuizz";
		volumeAndNbQuestionToSave[3] = nbQuestion.ToString();
		string jsonString = JsonSerializer.Serialize(volumeAndNbQuestionToSave);
		using (FileAccess file = FileAccess.Open(System.Environment.CurrentDirectory+"/ressources/saves/settings.json", FileAccess.ModeFlags.Write))
		{
			file.StoreString(jsonString);
		}
	}

	public int getNbQuestionMax()
	{
		return nbQuestionMax;
	}
	private void getSettingsFromJSON()
	{
		// Lire le fichier JSON
		using (FileAccess file = FileAccess.Open(System.Environment.CurrentDirectory+"/ressources/saves/settings.json", FileAccess.ModeFlags.Read))
		{
			if (file != null)
			{
				string jsonString = file.GetAsText();
				string[] volumeAndNbQuestionMaxToSave = new string[4];
				volumeAndNbQuestionMaxToSave = JsonSerializer.Deserialize<string[]>(jsonString);
				volume = volumeAndNbQuestionMaxToSave[1].ToFloat();
				nbQuestionMax = volumeAndNbQuestionMaxToSave[3].ToInt();
			}
		}
	}

	public void setNbQuestionMax(int nb)
	{
		if(nb > 0)
		{
			nbQuestionMax = nb;
		}
		else
		{
			nbQuestionMax = 10;
		}
	}

}
