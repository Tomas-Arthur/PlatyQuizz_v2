// cette classe repertorie toutes les données utile au jeux

import 'dart:math';
import 'package:flutter/material.dart';
import 'package:namer_app/playPage.dart';
import 'package:namer_app/quizzPage.dart';
class GameManager
{
static final GameManager _instance = GameManager._internal();
GameManager._internal();
 BuildContext? _context;
int choosenTheme = -1 ;
// Getter pour récupérer l'instance unique
  static GameManager get instance => _instance;


// tableau des themes
List<List<dynamic>> themeData = [ ['Anime', [
  ['animeSongRepository/One Piece OP 1 - We Are! Lyrics.mp3', 'One Piece'],
  ['animeSongRepository/Attack On Titan Opening 1 Guren No Yumiya (Tv Size).mp3', 'Attack On Titan'],
  ['animeSongRepository/GTO the Animation - Opening 1  Drivers High.mp3', 'Great Teacher Onizuka'],
  ['animeSongRepository/Shigatsu wa Kimi no Uso - Opening 1 - TV size with video.mp3', 'Your Lie In April'],
  ['animeSongRepository/BEASTARS Season 2 OP (Clean)  Kaibutsu - YOASOBI  Netflix Anime.mp3', 'Beastars'],
  ['animeSongRepository/YOASOBI  アイドル (Idol) TV Size Ver. Lyrics [Kan_Rom_Eng].mp3', 'Oshi No Ko'],
  ['animeSongRepository/03 - again (TV Size) (YUI) ~ FMA Brotherhood (OST I) - [ZR].mp3', 'Full Metal Alchemists'],
  ['animeSongRepository/Fire Force - Opening  Inferno.mp3', 'Fire Force'],
  ['animeSongRepository/Vicke Blanka - Black Catcher (Anime music video, TV anime「Black Clover」OP10 Theme).mp3', 'Black Clover'],
  ['animeSongRepository/Solo Leveling - Opening  Level.mp3', 'Solo Leveling'],
]
]];
int score = 0;
double volume = 0.5;
String goodAnswer ='';
bool goodAnswerGiven =false;
List<String> answers=[];
List<int> questionPulled = [];
List<String> answerPulled = [];
// sort une valeur aleatoire du theme choisi et fait en sorte de ne pas sortir une question deja posé
List<String> getQuestion(int theme)
{
  // on evite la boucle infini
  if( questionPulled.length == themeData[theme][1].length)
  {
    print("c'est la fin plus de question ");
   //quizzPage().travelBack();
   return List.empty();
  }
  List<String> selectedQuestion = [];
  int randomPulled = -1;
  randomPulled = Random().nextInt(themeData[theme][1].length);
  if(!questionPulled.contains(randomPulled))
  {
    questionPulled.add(randomPulled);
    selectedQuestion.add(themeData[choosenTheme][1][randomPulled][0]);
    selectedQuestion.add(themeData[choosenTheme][1][randomPulled][1]);
    goodAnswer = themeData[choosenTheme][1][randomPulled][1];
    print("questionPulled : "+questionPulled.toString());
    print("themeData[theme][1].length : "+themeData[theme][1].length.toString());
    return selectedQuestion;
  }
  else
  {
    
    return getQuestion(theme);
  }
   
    

}



// recupere de fausse reponse dans la liste des reponses
//themeDate[0][1] donne acces au theme 1
// themeDate[0][1][0][1] donne acces a la reponse de la question 1 du theme 1
String getAnswers(int theme, int i)
{
   if(i<themeData[theme][1].length)
   {
      int random = Random().nextInt(themeData[theme][1].length);
      if(themeData[theme][1][random][1] != goodAnswer && !answerPulled.contains(themeData[theme][1][random][1]))
      {
        print(" bad answer : "+themeData[theme][1][random][1]);
        answerPulled.add(themeData[theme][1][random][1]);
        return themeData[theme][1][random][1];
      }
      else
      {
        print(" restart anser already given : "+themeData[theme][1][random][1]);
        return getAnswers(theme,++i);
      }
   }
   else
   {
    answerPulled.clear();
    return getAnswers(theme,0);
   }
}

// vide question pulled pour une nouvelle partie
void resetQuestionPulled()
{
  questionPulled.clear();
}
// vide answer pulled pour une nouvelle partie
void resetAnswerPulled()
{
  answerPulled.clear();
}

// stock le changement de theme
int get _choosenTheme => choosenTheme;

set _choosenTheme(int a)
{
  choosenTheme = a;
}

bool getGoodAnswerGiven()
{
  return goodAnswerGiven;
}

set _goodAnswerGiven(bool a)
{
  goodAnswerGiven = a;
}

bool giveTheGoodAnswer()
{
  int random = Random().nextInt(4);
  if(random == 1 )
  {
    return true;
  }
  else
  {
    return false;
  }
}

int count=0;
String giveMeAnAnswer()
{
  count++;
  if(giveTheGoodAnswer() && goodAnswerGiven == false)
  {
    goodAnswerGiven = true;
    answers.add(goodAnswer);
    return goodAnswer;
  }
  else if(count == 4 && goodAnswerGiven == false)
  {
    goodAnswerGiven = true;
    answers.add(goodAnswer);
    return goodAnswer;
  }
  else
  {
    String answerGiven = getAnswers(choosenTheme,0);
    answers.add(answerGiven);
    return answerGiven;
  }
}


void resetVariable()
{
  resetAnswerPulled();
  //resetQuestionPulled();
  //score =0;
  goodAnswerGiven = false;
  count =0;
  answers.clear();
}

void resetScore()
{
  score =0;
}

void addScore()
{
  score++;
}

double getVolume()
{
  return volume;
}
void setVolume(double a)
{
  volume = a;
}

// Méthode pour enregistrer le BuildContext
  void setContext(BuildContext context) {
    _context = context;
  }
}