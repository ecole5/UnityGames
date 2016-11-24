using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

public class MainPortalButtons : MonoBehaviour {

	//Set the background image based on preferences for all portal scenes
	void Start(){
		GameObject.FindGameObjectWithTag ("Background").GetComponent<Image> ().sprite = GameData.Prefs.backgroundImages [GameData.Prefs.portal.backgroundChoice]; 
	}

	//Start the apple game
	public void PlayApple(){
		PortalAudio.output.playClick(); //play click sound
		PortalAudio.output.stop(); //stop the portal music
		SceneManager.LoadScene ("Apple_Splash"); //load the game

	}
		
	public void deleteUser(){
		PortalAudio.output.playClick(); //play click sound
		SceneManager.LoadScene ("DeleteUser"); //load the delete user screen
	}

	public void unblockUser(){
		PortalAudio.output.playClick(); //play click sound
		SceneManager.LoadScene ("Unblock"); //load the unblock screen 
	}
		
	public void changePassword(){
		PortalAudio.output.playClick(); //play click sound
		SceneManager.LoadScene ("Password"); //load the password set screen
	}

	public void portalHome(){
		PortalAudio.output.playClick(); //play click sound
		SceneManager.LoadScene ("PortalHome"); //load the portal home screen
	}
		
	//Start the ninja game
	public void PlayNinja(){
		PortalAudio.output.playClick(); //play click sound
		PortalAudio.output.stop (); //stop portal music
		SceneManager.LoadScene ("RPS_Splash"); //load the ninja game
	}

	//Start the space game
	public void PlaySpace(){
		PortalAudio.output.playClick(); 
		PortalAudio.output.stop ();
		SceneManager.LoadScene ("Scene_SpaceSplash");
	}

	//Start the dino game
	public void PlayDino(){
		PortalAudio.output.playClick();
		PortalAudio.output.stop ();
		SceneManager.LoadScene ("Dino_Splash");
	}

	//New User screen
	public void newUser(){
		PortalAudio.output.playClick();
		SceneManager.LoadScene ("NewUser");
	}

	//Load portal audio screen
	public void configAudio(){
		PortalAudio.output.playClick();
		SceneManager.LoadScene ("PortalAudioConfig");
	}

	//Load portal background config screen
	public void configBackground(){
		PortalAudio.output.playClick();
		SceneManager.LoadScene ("PortalBackgroundConfig");
	}
		
	//Load the portal history screen
	public void PlayHistroy(){
		PortalAudio.output.playClick();
		SceneManager.LoadScene ("PortalHistory");
	}

	//Exit to the login screen
	public void Exit(){
		PortalAudio.output.playClick(); //play click sound
		HistoryMethods.addByDate(HistoryMethods.portalLog(Time.realtimeSinceStartup.ToString()),GameData.Prefs.portalHist);
	
		GameData.Prefs.saveData (); //save all game data to server
		SceneManager.LoadScene ("Login"); //load login screen
	}
}
