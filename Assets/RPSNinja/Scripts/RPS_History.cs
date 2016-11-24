using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class RPS_History : MonoBehaviour {

	//Reference to text element 
	public Text showText;



	void Awake(){
		History.Record [] temp = GameData.Prefs.ninjaHist.list;

		for (int i = 0; i < GameData.Prefs.ninjaHist.size; i++) {
			int rank = i+1;
			showText.text += "\nRank: " + rank + "\n";
			showText.text += "Username: " + temp[i].username + "\n";
			showText.text += "Date: " + temp[i].date + "\n";
			showText.text += "Score: " + temp[i].score + "\n";
		}

	} 






}
