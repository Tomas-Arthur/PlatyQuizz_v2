

import 'dart:io';

import 'package:english_words/english_words.dart';
import 'package:flutter/material.dart';

import 'package:provider/provider.dart';
import 'package:flutter/services.dart'; 


import 'playPage.dart';
import 'hostPage.dart';


void main() {
  runApp(MyApp());
 
}

class MyApp extends StatelessWidget {
  const MyApp({super.key});

  @override
  Widget build(BuildContext context) {
    return ChangeNotifierProvider(
      create: (context) => MyAppState(),
      child: MaterialApp(
        title: 'Platy Quizz',
        theme: ThemeData(
          useMaterial3: true,
          colorScheme: ColorScheme.fromSeed(seedColor: Color.fromARGB(255, 36, 125, 170)),
        ),
        home: MyHomePage(),
      ),
    );
  }
}

class MyAppState extends ChangeNotifier {
  /*var current = WordPair.random();
  
  void getPlay() {
    current = WordPair.random();
    notifyListeners();
  }*/
}

class MyHomePage extends StatelessWidget {
  @override
  Widget build(BuildContext context) {     
    return Scaffold(
      // le titre de l'app
       appBar: AppBar(  
        centerTitle: true,
        title: Center(child: Text("Platy Quizz")),
        backgroundColor: Color.fromARGB(255, 36, 125, 170),
      ),  

      //les 3 boutons
      body: Center(
        
        child: Column(
          mainAxisAlignment: MainAxisAlignment.center,
          children: [
            
           //bouton 1
            Padding(
              padding: const EdgeInsets.all(20.0),
              child: SizedBox(
                width: 100.0,
                height: 50.0,
                child: ElevatedButton(
                  
                  onPressed: () {
                    Navigator.push(context, MaterialPageRoute(builder: (context)=> playPage() ));
                  },
                  style: ElevatedButton.styleFrom(
                  backgroundColor: Colors.lightBlueAccent, // Background color
                  ),
                  child: Text('Play'),
                ),
                
              ),
            ),
            //bouton 2
            Padding(
              padding: const EdgeInsets.all(20.0),
              child: SizedBox(
                width: 100.0,
                height: 50.0,
                child: ElevatedButton(
                  onPressed: () {
                     Navigator.push(context, MaterialPageRoute(builder: (context)=> const hostPage() ));
                  },
                  style: ElevatedButton.styleFrom(
                  backgroundColor: Colors.lightBlueAccent, // Background color
                  ),
                  child: Text('Host'),
                ),
                
              ),
            ),
            //bouton 3
            Padding(
              padding: const EdgeInsets.all(20.0),
              child: SizedBox(
                width: 100.0,
                height: 50.0,
                child: ElevatedButton(
                  onPressed: () {
                     exit(0); 
                  },
                  style: ElevatedButton.styleFrom(
                  backgroundColor: Colors.lightBlueAccent, // Background color
                  ),
                  child: Text('Exit'),
                ),
                
              ),
            ),

            
          ],
        ),
      ),
    );
  }
}





