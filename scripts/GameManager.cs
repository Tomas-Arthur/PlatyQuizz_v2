using Godot;
using System;
using System.Collections.Generic;
using System.Text.Json;
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
    
	public List<string[]> getAnimeData()
	{
		return animeData;
	}


	
	public void addToThemeData(string theme,string[] values)
	{
		themeData[theme].Add(values) ;
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
		getDataFromJSON();
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

	public void saveDictionaryToJSON()
	{
		        // Convertir les données en JSON
        string jsonString = JsonSerializer.Serialize(themeData);

        // Écrire le JSON dans un fichier
        using (FileAccess file = FileAccess.Open("res://ressources/saves/themeData.json", FileAccess.ModeFlags.Write))
        {
            file.StoreString(jsonString);
        }
	}

	private void getDataFromJSON()
	{
		// Lire le fichier JSON
        using (FileAccess file = FileAccess.Open("res://ressources/saves/themeData.json", FileAccess.ModeFlags.Read))
        {
            if (file != null)
            {
                string jsonString = file.GetAsText();
                themeData = JsonSerializer.Deserialize<Dictionary<string, List<string[]>>>(jsonString);
            }
        }
	}

}
