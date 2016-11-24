using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class SpaceHistory : MonoBehaviour {

    //Reference to text element in this screen
	public Text showText;

	void Awake(){
		History.Record [] temp = GameData.Prefs.spaceHist.list; //get list of all high scores from game data

		for (int i = 0; i < GameData.Prefs.spaceHist.size; i++) { //iterate through and display each entry 
			int rank = i+1;
			showText.text += "\nRank: " + rank + "\n";
			showText.text += "Username: " + temp[i].username + "\n";
			showText.text += "Date: " + temp[i].date + "\n";
			showText.text += "Score: " + temp[i].score + "\n";
			showText.text += "Level: " + temp[i].level + "\n";
		}

	} 

} 




