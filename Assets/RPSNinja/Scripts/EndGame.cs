using UnityEngine;
using UnityEngine.UI; //needed to access the UI Text element
using UnityEngine.SceneManagement;

//This class has methods to change scenes, quit the application and update the GameOver result text

public class EndGame : MonoBehaviour {
	
	Text resultText;  //result text
	GameObject temp; 
	MainAction driverScript; 

	public void MainMenu(){ //return to main menu
		Destroy (temp); //destroy the driver object
		SceneManager.LoadScene("RPS_Splash"); //load the main menu screen
	}
		
	public void replay(){
		Destroy (temp); //destroy the driver object
		SceneManager.LoadScene("RPS_LoadScore");
	}

	void Awake(){
		temp = GameObject.FindGameObjectWithTag ("driver"); //get a reference to the driver
		driverScript = temp.GetComponent<MainAction> (); //get reference to the driver script
		resultText = GetComponent<Text> (); //get a reference to the 'result text'
		resultText.text = driverScript.result; //update based on the result text
	}
}