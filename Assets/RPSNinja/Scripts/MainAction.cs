using UnityEngine;
using UnityEngine.SceneManagement;

//attached to the driver prefab, used to control the logic of the game, through each round and during the end game 

public class MainAction : MonoBehaviour {
	
	TextUpdate counters; //reference to all the scores
	GameObject chosenOne; //user chosen player
	public GameObject[] COMprefabs; //list of possible players for computer
	GameObject com; //computer chosen player
	Vector3 temp; 
	public string result; //result of game


	public void getResult() //gets result of object v object
	{

		//destroys losing object and called statUpdate to update scores properly
		if (chosenOne.name == com.tag) { //tie
			Destroy (com.gameObject);
			Destroy (chosenOne.gameObject);
			statUpdate (0, 0, 1);
		} else if (chosenOne.name == "Rock" && com.tag == "Paper") { //rock v paper
			Destroy (chosenOne.gameObject);
			statUpdate (0, 1, 1);
		} else if (chosenOne.name == "Rock" && com.tag == "Scissor") { //rock v scissor
			Destroy (com.gameObject);
			statUpdate (1, 0, 1);
		} else if (chosenOne.name == "Paper" && com.tag == "Rock") { //paper v rock
			Destroy (com.gameObject);
			statUpdate (1, 0, 1);
		} else if (chosenOne.name == "Paper" && com.tag == "Scissor") { //paper v scissor
			Destroy (chosenOne.gameObject);
			statUpdate (0, 1, 1);
		} else if (chosenOne.name == "Scissor" && com.tag == "Paper") { //scissor v paper
			Destroy (com.gameObject);
			statUpdate (1, 0, 1);
		} else if (chosenOne.name == "Scissor" && com.tag == "Rock") { //scissor v rock
			Destroy (chosenOne.gameObject);
			statUpdate (0, 1, 1);
		}
		if (counters.round == 10) { //move to end screen if game on tenth round
			Invoke ("endGame", 1.5f);
		}
		else {
			Invoke ("reset", 1.5f); //move to next rounds
		}
	}

	void endGame() //when the game ends this function is called
	{
		HistoryMethods.addByScore(HistoryMethods.regLog(counters.playerScore.ToString()),GameData.Prefs.ninjaHist);

		DontDestroyOnLoad (gameObject); //keeps this alive for the endgame

		//determines winer and sets result
		if (counters.playerScore == counters.comScore) {
			result = "Tie Game";
		} else if (counters.playerScore > counters.comScore) {
			result = "Player Wins";
		} else {
			result = "Computer Wins";
		}
		Destroy (counters.gameObject); //destroy the text gameobject
		SceneManager.LoadScene ("RPS_GameOver"); //loads the end screen

	}

	void reset() //refresh to the MainGame after each round
	{
		SceneManager.LoadScene ("RPS_MainGame"); 
	}

	void statUpdate(int playerScore, int comScore, int round) //function that updates all the scores
	{
		counters.playerScore += playerScore;
		counters.comScore += comScore;
		counters.round += round;
	}

	void Awake() 
	{
		//Get reference to the object the players selected
		chosenOne = GameObject.FindGameObjectWithTag ("Selected");
		Vector3 pos = new Vector3 (7,3,-1);
		//set to starting position to the left
		chosenOne.transform.position = pos;

		//Choose which element the computer will use
		int temp = Random.Range(0,3);
		com = Instantiate (COMprefabs [temp]); //create chosen prefab
		pos.x = -7; //set initial position to the right
		com.transform.position = pos;

		//Get reference to parent object that contains all the text elements in the MainGame (player score, computer score, round)
		GameObject temp1 = GameObject.Find("Text"); 
		counters = temp1.GetComponent<TextUpdate> (); //get reference to the TextUpdate script, which holds the scores
	}

void Update () {
		if (com != null) { //move the computer left until collision
			temp = com.transform.position;
			temp.x += Time.deltaTime * 3f;
			com.transform.position = temp;
		}
		if (chosenOne != null) { //move the computer right until collision
			temp = chosenOne.transform.position;
			temp.x -= Time.deltaTime * 3f;
			chosenOne.transform.position = temp;
		}
	}
}
