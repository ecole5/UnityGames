using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameOver : MonoBehaviour {

	//Get reference to the result text object
	public Text resultText; 

	void Awake()
	{
		//sets the game over result from the MissionControl object which was set in main
		resultText.text = MissionControl.huston.causeOfDeath; 
	}

}
