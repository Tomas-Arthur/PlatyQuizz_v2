
import 'package:flutter/material.dart';
import 'package:namer_app/GameManager.dart';
import 'package:namer_app/main.dart';
import 'package:namer_app/playPage.dart';
import 'package:audioplayers/audioplayers.dart';

class QuizzPage extends StatefulWidget {
  @override
  _quizzPageState createState() => _quizzPageState();
}

class _quizzPageState extends State<QuizzPage>
{
  

late AudioPlayer audioPlayer;
double _volume = GameManager.instance.getVolume(); // Volume initial
List<dynamic> question = GameManager.instance.getQuestion(GameManager.instance.choosenTheme );
  
  Duration _duration = Duration() ;
  Duration _position = Duration();
  
  
  @override
  void initState()
  {
    super.initState();
    GameManager.instance.resetVariable();
   
    audioPlayer= AudioPlayer();
    audioPlayer.setVolume(_volume);
    //initAudioPlayer();
    //dispose();
  }
  @override
  void dispose() {
    audioPlayer.dispose();
    super.dispose();
  }

/*
 void initAudioPlayer() async
 {
    audioPlayer.onDurationChanged.listen((duration) {
      setState(() {
        _duration = duration ;
      });
     });

     audioPlayer.onPositionChanged.listen((position) {
      setState(() {
        _position = position;
      });
      });
 }*/


  @override
  Widget build(BuildContext context) {
    if(question.isEmpty)
    {
      GameManager.instance.resetAnswerPulled();
      GameManager.instance.resetQuestionPulled();
      question = GameManager.instance.getQuestion(GameManager.instance.choosenTheme );
      return Scaffold(
      // le titre de l'app
       appBar: AppBar(  
        centerTitle: true,
        title: Center(child: Text("Quizz")),
        backgroundColor: Color.fromARGB(255, 36, 125, 170),
       ),
        body: Center(
         child: Column(
           mainAxisAlignment: MainAxisAlignment.center,
           children: [
            Text("Fin des questions !"),
            Text("Votre score est de "+ GameManager.instance.score.toString()+" / "+ GameManager.instance.themeData[GameManager.instance.choosenTheme][1].length.toString()),
            // bouton 1
            Padding(
              padding: const EdgeInsets.all(20.0),
              child: SizedBox(
                width: 200.0,
                height: 100.0,
                child: ElevatedButton(
                  onPressed: () {
                     Navigator.push(context, MaterialPageRoute(builder: (context)=> playPage() ));
                  },
                  style: ElevatedButton.styleFrom(
                  backgroundColor: Colors.lightBlueAccent, // Background color
                  ),
                  child: Text('retour au choix des themes'),
                ),
                
              ),
            ),
            // bouton 2
            Padding(
              padding: const EdgeInsets.all(20.0),
              child: SizedBox(
                width: 200.0,
                height: 100.0,
                child: ElevatedButton(
                  onPressed: () {
                     Navigator.push(context, MaterialPageRoute(builder: (context)=>  MyHomePage() ));
                  },
                  style: ElevatedButton.styleFrom(
                  backgroundColor: Colors.lightBlueAccent, // Background color
                  ),
                  child: Text('Retour a l acceuil '),
                ),
                
              ),
            ),
           ]
        ),

      ));
      
    }
    else
    {
      String sourceMusique = question[0].toString();
      
    return Scaffold(
      // le titre de l'app
       appBar: AppBar(  
        centerTitle: true,
        title: Center(child: Text("Quizz")),
        backgroundColor: Color.fromARGB(255, 36, 125, 170),
        
        

      ),
      
      body: Center(
        child: Column(
           mainAxisAlignment: MainAxisAlignment.center,
           children: [
            // text
            // lecteur audio
            /* row [column[ElevatedButton,ElevatedButton],column[ElevatedButton,ElevatedButton]]*/
            
            Text( "Le theme choisi est " + GameManager.instance.themeData[GameManager.instance.choosenTheme][0].toString()),
            
            //Text("la reponse est "+ question[1]),
           // Text("les reponses choisis sont : "+ answers[0]),
            // lecteur audio
                  // Barre de progression
               /*  StatefulBuilder(builder: ((context, setState) {
              return Slider(value:  _position.inMilliseconds.toDouble() , onChanged: (double value) {
              setState(() {
                _position  = value as Duration;
              });
              //audioPlayer.setVolume(_volume);
            },);
            })),*/
        Row(
           mainAxisAlignment: MainAxisAlignment.center,
           children: [
        IconButton(
          icon: Icon(Icons.play_arrow),
              onPressed: () async {
                try{
                  
                  audioPlayer.play(AssetSource(sourceMusique));
                }
                catch(e)
                {
                  print("Une erreur s'est produite lors du démarrage de la lecture audio : $e");
                }
              },
            ),
            IconButton(
          icon: Icon(Icons.pause),
              onPressed: () {
                audioPlayer.pause();
              },
              
            ),
            IconButton(
          icon: Icon(Icons.stop),
              onPressed: () {
                audioPlayer.stop();
              },
              
            ),

            //curseur volume
            // statefullBuild <3 refresh only 1 widget
            StatefulBuilder(builder: ((context, setState) {
              return Slider(value: _volume, onChanged: (double value) {
              setState(() {
                _volume = value;
              });
              audioPlayer.setVolume(_volume);
              GameManager.instance.setVolume( _volume);
            },);
            }))
                       
            
           ]
        ),








// le code des reponses 
            Row(
              mainAxisAlignment: MainAxisAlignment.center,
              children: [
                
                Column(
                  mainAxisAlignment: MainAxisAlignment.center,
                  children: [
                    Padding(
              padding: const EdgeInsets.all(20.0),
              child: SizedBox(
                 width: 150.0,
                height: 100.0,
                child: ElevatedButton(
                  
                  onPressed: () {
                    //Navigator.push(context, MaterialPageRoute(builder: (context)=> playPage() ));
                    print(GameManager.instance.answers[1]);
                    print("GameManager.instance.answers[0] == GameManager.instance.goodAnswer : "+GameManager.instance.answers[1].toString() == GameManager.instance.goodAnswer.toString());
                     print("GameManager.instance.answers[0] : "+GameManager.instance.answers[0] +" GameManager.instance.goodAnswer : "+ GameManager.instance.goodAnswer);
                    if(GameManager.instance.answers[0] == GameManager.instance.goodAnswer)
                    {
                      print("score ++");
                      GameManager.instance.addScore();
                      audioPlayer.stop();
                      audioPlayer.dispose();
                      showDialog<String>(
                        context: context,
                         builder: (BuildContext context) => AlertDialog(
                          title: const Text("Bonne Reponse"),
                          content: const Text("vous avez donné la bonne reponse"),
                          actions: <Widget>[
                            TextButton(
                              onPressed: ()=> Navigator.push(context, MaterialPageRoute(builder: (context)=> QuizzPage() )),
                               child: const Text("Question suivante"),
                               )
                          ],
                         )
                         );
                    }
                    else
                    {
                      audioPlayer.stop();
                      audioPlayer.dispose();
                      showDialog<String>(
                        context: context,
                         builder: (BuildContext context) => AlertDialog(
                          title: const Text("Mauvaise Reponse"),
                          content:  Text("La bonne reponse est : "+ GameManager.instance.goodAnswer.toString()),
                          actions: <Widget>[
                            TextButton(
                              onPressed: ()
                              { 
                                 GameManager.instance.resetVariable();
                                Navigator.push(context, MaterialPageRoute(builder: (context)=> QuizzPage() ));
                                },
                               child: const Text("Question suivante"),
                               )
                          ],
                         )
                         );
                    }
                    
                    //Navigator.push(context, MaterialPageRoute(builder: (context)=> QuizzPage() ));
                  },
                  style: ElevatedButton.styleFrom(
                  backgroundColor: Colors.lightBlueAccent, // Background color
                  ),
                  child: Text(GameManager.instance.giveMeAnAnswer()),
                ),
                
              ),
            ),
            
                
                 
                
              
            
            Padding(
              padding: const EdgeInsets.all(20.0),
              child: SizedBox(
                 width: 150.0,
                height: 100.0,
                child: ElevatedButton(
                  
                  onPressed: () {
                    //Navigator.push(context, MaterialPageRoute(builder: (context)=> playPage() ));
                    print(GameManager.instance.answers[1]);
                    print("GameManager.instance.answers[0] == GameManager.instance.goodAnswer : "+GameManager.instance.answers[1].toString() == GameManager.instance.goodAnswer.toString());
                     print("GameManager.instance.answers[0] : "+GameManager.instance.answers[1] +" GameManager.instance.goodAnswer : "+ GameManager.instance.goodAnswer);
                    if(GameManager.instance.answers[1] == GameManager.instance.goodAnswer)
                    {
                      print("score ++");
                      GameManager.instance.addScore();
                      audioPlayer.stop();
                      audioPlayer.dispose();
                      showDialog<String>(
                        context: context,
                         builder: (BuildContext context) => AlertDialog(
                          title: const Text("Bonne Reponse"),
                          content: const Text("vous avez donné la bonne reponse"),
                          actions: <Widget>[
                            TextButton(
                              onPressed: ()=> Navigator.push(context, MaterialPageRoute(builder: (context)=> QuizzPage() )),
                               child: const Text("Question suivante"),
                               )
                          ],
                         )
                         );
                    }
                    else
                    {
                      audioPlayer.stop();
                      audioPlayer.dispose();
                      showDialog<String>(
                        context: context,
                         builder: (BuildContext context) => AlertDialog(
                          title: const Text("Mauvaise Reponse"),
                          content:  Text("La bonne reponse est : "+ GameManager.instance.goodAnswer.toString()),
                          actions: <Widget>[
                            TextButton(
                              onPressed: ()
                              { 
                                 GameManager.instance.resetVariable();
                                Navigator.push(context, MaterialPageRoute(builder: (context)=> QuizzPage() ));
                                },
                               child: const Text("Question suivante"),
                               )
                          ],
                         )
                         );
                    }
                  },
                  style: ElevatedButton.styleFrom(
                  backgroundColor: Colors.lightBlueAccent, // Background color
                  ),
                  child: Text(GameManager.instance.giveMeAnAnswer()),
                ),
                
              ),
            ),
                  ],
                ),
                Column(
                  mainAxisAlignment: MainAxisAlignment.center,
                  children: [
                    Padding(
              padding: const EdgeInsets.all(20.0),
              child: SizedBox(
                 width: 150.0,
                height: 100.0,
                child: ElevatedButton(
                  
                  onPressed: () {
                    //Navigator.push(context, MaterialPageRoute(builder: (context)=> playPage() ));
                    print(GameManager.instance.answers[2]);
                    print("GameManager.instance.answers[0] == GameManager.instance.goodAnswer : "+GameManager.instance.answers[2].toString() == GameManager.instance.goodAnswer.toString());
                     print("GameManager.instance.answers[0] : "+GameManager.instance.answers[2] +" GameManager.instance.goodAnswer : "+ GameManager.instance.goodAnswer);
                    if(GameManager.instance.answers[2] == GameManager.instance.goodAnswer)
                    {
                       print("score ++");
                      GameManager.instance.addScore();
                      audioPlayer.stop();
                      audioPlayer.dispose();
                      showDialog<String>(
                        context: context,
                         builder: (BuildContext context) => AlertDialog(
                          title: const Text("Bonne Reponse"),
                          content: const Text("vous avez donné la bonne reponse"),
                          actions: <Widget>[
                            TextButton(
                              onPressed: ()=> Navigator.push(context, MaterialPageRoute(builder: (context)=> QuizzPage() )),
                               child: const Text("Question suivante"),
                               )
                          ],
                         )
                         );
                    }
                    else
                    {
                      audioPlayer.stop();
                      audioPlayer.dispose();
                      showDialog<String>(
                        context: context,
                         builder: (BuildContext context) => AlertDialog(
                          title: const Text("Mauvaise Reponse"),
                          content:  Text("La bonne reponse est : "+ GameManager.instance.goodAnswer.toString()),
                          actions: <Widget>[
                            TextButton(
                              onPressed: ()
                              { 
                                 GameManager.instance.resetVariable();
                                Navigator.push(context, MaterialPageRoute(builder: (context)=> QuizzPage() ));
                                },
                               child: const Text("Question suivante"),
                               )
                          ],
                         )
                         );
                    }
                  },
                  style: ElevatedButton.styleFrom(
                  backgroundColor: Colors.lightBlueAccent, // Background color
                  ),
                  child: Text(GameManager.instance.giveMeAnAnswer()),
                ),
                
              ),
            ),
            Padding(
              padding: const EdgeInsets.all(20.0),
              child: SizedBox(
                width: 150.0,
                height: 100.0,
                child: ElevatedButton(
                  
                  onPressed: () {
                    //Navigator.push(context, MaterialPageRoute(builder: (context)=> playPage() ));
                    print(GameManager.instance.answers[3]);
                    print("GameManager.instance.answers[0] == GameManager.instance.goodAnswer : "+GameManager.instance.answers[3].toString() == GameManager.instance.goodAnswer.toString());
                    print("GameManager.instance.answers[0] : "+GameManager.instance.answers[3] +" GameManager.instance.goodAnswer : "+ GameManager.instance.goodAnswer);
                    if(GameManager.instance.answers[3] == GameManager.instance.goodAnswer)
                    {
                       print("score ++");
                      GameManager.instance.addScore();
                      audioPlayer.stop();
                      audioPlayer.dispose();
                      showDialog<String>(
                        context: context,
                         builder: (BuildContext context) => AlertDialog(
                          title: const Text("Bonne Reponse"),
                          content: const Text("vous avez donné la bonne reponse"),
                          actions: <Widget>[
                            TextButton(
                              onPressed: ()=> Navigator.push(context, MaterialPageRoute(builder: (context)=> QuizzPage() )),
                               child: const Text("Question suivante"),
                               )
                          ],
                         )
                         );
                    }
                    else
                    {
                      audioPlayer.stop();
                      audioPlayer.dispose();
                      showDialog<String>(
                        context: context,
                         builder: (BuildContext context) => AlertDialog(
                          title: const Text("Mauvaise Reponse"),
                          content:  Text("La bonne reponse est : "+ GameManager.instance.goodAnswer.toString()),
                          actions: <Widget>[
                            TextButton(
                              onPressed: ()
                              { 
                                 GameManager.instance.resetVariable();
                                Navigator.push(context, MaterialPageRoute(builder: (context)=> QuizzPage() ));
                                },
                               child: const Text("Question suivante"),
                               )
                          ],
                         )
                         );
                    }
                  },
                  style: ElevatedButton.styleFrom(
                  backgroundColor: Colors.lightBlueAccent, // Background color
                  ),
                  child: Text(GameManager.instance.giveMeAnAnswer()),
                ),
                
              ),
            ),
                  ],
                )
              ],
              
            ),
           /*  ElevatedButton(
                  
                  onPressed: () {
                    GameManager.instance.resetVariable();
                    audioPlayer.stop();
                    Navigator.push(context, MaterialPageRoute(builder: (context)=> QuizzPage() ));
                  },
                  style: ElevatedButton.styleFrom(
                  backgroundColor: Colors.lightBlueAccent, // Background color
                  ),
                  child: Text("Next question"),
                ),*/
           ],
        ),
      ),
      
      );
      
  }
  
  
  }
  
}