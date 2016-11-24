using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameStatus : MonoBehaviour {

	//References to all the text elements
	public Text scoreText;
	public Text levelText;
	public Text enemyText;
	public Text timeText;

	//status of play/pause
	bool paused = false;  //false when game is playing 

	//Pause button function
	public void onPause()
	{
		if (paused) { //then unpause 
			Time.timeScale = 1;
			paused = false;
		} else { //if not paused then pause 
			Time.timeScale = 0;
			paused = true;
		}
	}

	//Restart the MainGame
	public void restart()
	{
		Time.timeScale = 1; //unpauses if paused
		SceneManager.LoadScene ("Scene_SpaceMainGame");
	}

	//Go back to splash screen
	public void exit()
	{
		Time.timeScale = 1;//unpauses if paused
		SceneManager.LoadScene ("Scene_SpaceSplash");
	}
		

	void OnGUI()
	{
		scoreText.text = "Score: " + Main.S.points.ToString(); //update the score display with score from main

		//Update the GameLevel with correct name
		switch (Main.S.gameLevel) {
		case(0):
			levelText.text = "Bronze";
			break;
		case(1):
			levelText.text = "Silver";
			break;
		default:
			levelText.text = "Gold";
			break;
		}

		//Get the current time and update with just the first four values of the float
		string a;
		string b = "Time: ";
		a = Main.S.gameTime.ToString();
		for (int i = 0; i < 4; i++) {
			if (a.Length > i+1) {
				b += a [i];
			}
		}
		timeText.text = b;

		//Update the enemy kill display using values from main
		int[] killList = Main.S.killList;
		enemyText.text = "E1 " + killList [0].ToString () + " E2 " + killList [1].ToString () + " E3 " + killList [2].ToString () + " E4 " + killList [3].ToString () + " E5 " + killList [4].ToString ();
	}



}
