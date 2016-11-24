using UnityEngine;
using UnityEngine.SceneManagement;

//buttons for the ninja game menus
public class Menu : MonoBehaviour {

	public void playGame(){ 
		SceneManager.LoadScene ("RPS_LoadScore");
	}
	public void loadSplash(){ 
		SceneManager.LoadScene ("RPS_Splash");
	}
	public void loadHist(){ 
		SceneManager.LoadScene ("RPS_History");
	}
	public void quit(){ 
		PortalAudio.output.playMusic ();
		SceneManager.LoadScene ("PortalHome");
	}



}
