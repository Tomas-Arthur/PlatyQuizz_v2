import 'package:flutter/material.dart';
import 'package:namer_app/quizzPage.dart';


import 'GameManager.dart';

// Create the state for the RadioListTile example 
class playPage extends StatefulWidget { 
@override 
_playPage createState() => _playPage(); 

} 

class _playPage extends State<playPage> { 
// Create a variable to store the selected value 
int nbTable = 0; 
//final GameManager gameManager = GameManager();
late ElevatedButton newButton;
 final List<List<dynamic>> buttonLabels = GameManager.instance.themeData; // a remplir avec la liste des table 
@override 
Widget build(BuildContext context) { 
	return Scaffold( 
	appBar: AppBar( 
		centerTitle: true,
    title: Center(child: Text("Choose theme")),
    backgroundColor: Color.fromARGB(255, 36, 125, 170), 
	), 
	body: SingleChildScrollView(
  child : Center(
      child: Column(
        mainAxisAlignment: MainAxisAlignment.center,
        children: List.generate(
          buttonLabels.length,
          (index) => Padding(
            padding: const EdgeInsets.all(20.0), // Ajoutez le padding souhaité
            child: SizedBox(
            width: 200.0, // Ajustez la largeur du SizedBox selon vos besoins
            height: 50.0,
            child: ElevatedButton(
              onPressed: () {
                // Action à effectuer lorsqu'un bouton est pressé
                //print('Bouton ${index + 1} pressé !');
               // print(index);
                GameManager.instance.choosenTheme = index;
                //print(GameManager.instance.themeData[0][1][1][1]);
                //print(gameManager.choosenTheme);
                GameManager.instance.resetVariable();
                GameManager.instance.resetQuestionPulled();
                GameManager.instance.resetScore();
                Navigator.push(context, MaterialPageRoute(builder: (context)=>  QuizzPage() ));
              },
              child: Text(buttonLabels[index][0].toString()),
              
        ),
      ),
    )))))); 

  
} 



} 
