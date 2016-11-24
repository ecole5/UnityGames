using UnityEngine;
using UnityEngine.SceneManagement;

public class Dino_Menu : MonoBehaviour {


	//Load the splash screen
	public void SplashButton() {
		AudioController.speaker.cutShort ();// make sure long win sound stop splaying 
		if (Dino_MainGame.gameStats != null){
			Destroy (Dino_MainGame.gameStats.gameObject); //make sure next game is fresh
		}
		SceneManager.LoadScene ("Dino_Splash");
	}

	public void PlayButton() {
		AudioController.speaker.cutShort ();// make sure long win sound stop splaying 
		if (Dino_MainGame.gameStats != null){
			Destroy (Dino_MainGame.gameStats.gameObject); //make sure next game is fresh
		}
		SceneManager.LoadScene ("Dino_MainGame"); //load the game
	}
	public void HistoryButton() {
		SceneManager.LoadScene ("Dino_History");
	}
	public void QuitButton() {
		Destroy (AudioController.speaker.gameObject); //destroy the dino sound controller
		PortalAudio.output.playMusic (); //restart portal music
   
		SceneManager.LoadScene ("PortalHome");
	}
		
}
