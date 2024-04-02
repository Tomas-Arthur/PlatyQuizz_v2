import 'package:english_words/english_words.dart';
import 'package:flutter/material.dart';
import 'package:provider/provider.dart';

class hostPage extends StatelessWidget
{
  const hostPage({super.key});
  @override
  Widget build(BuildContext context) {
    return Scaffold(
      // le titre de l'app
       appBar: AppBar(  
        centerTitle: true,
        title: Center(child: Text("Host Lobby ... work in progress")),
        backgroundColor: Color.fromARGB(255, 36, 125, 170),
      ),);
  }
  
}