using UnityEngine;
using UnityEngine.SceneManagement;

//this script is loaded in an empty scene before the rest of the game elements are loaded
//By not having it the MainGame scene we prevent duplicate copies from being made when we reload the MainGame
//(instead of keeping a reference and destroying any new ones, which caused race conditions)

public class TextUpdate : MonoBehaviour {

	public int playerScore, comScore, round; //player score, computer score, round data 

	//References to the text elements
	TextMesh playerText; 
	TextMesh comText;
	TextMesh roundText;

	void Awake () { 
		DontDestroyOnLoad (gameObject); //dont destroy this gameobject when loading new scenes, thus we keep it alive when we refresh the MainGame and perverse the score

		//Get all the references to the various text elements that needed updating
		Transform temp = transform.Find ("ComScore");
		comText = temp.gameObject.GetComponent("TextMesh") as TextMesh;
		temp = transform.Find ("PlayerScore");
		playerText = temp.gameObject.GetComponent("TextMesh") as TextMesh;
		temp = transform.Find ("Round");
		roundText = temp.gameObject.GetComponent("TextMesh") as TextMesh;

		SceneManager.LoadScene ("RPS_MainGame"); //start the first MainGame
	}
		
void Update()
	{
		//Update the text with the current values (scores and round), this will be updated by the driver
		playerText.text = "Player: " + playerScore.ToString();
		comText.text = "Com: " + comScore.ToString();
		roundText.text = "Round: " + round.ToString();
	}

}