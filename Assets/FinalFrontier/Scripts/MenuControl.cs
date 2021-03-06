using UnityEngine;
using UnityEngine.SceneManagement;

//Menu controller for the space menus
public class MenuControl : MonoBehaviour {


	public void quitGame()
	{
		Destroy (MissionControl.huston.gameObject); //destroy this games manager
		PortalAudio.output.playMusic (); //start the portal music before loading it
		SceneManager.LoadScene ("PortalHome");

	}

	//Navigation Buttons that load new scenes
	public void loadSplash() {
		SceneManager.LoadScene ("Scene_SpaceSplash");
	}

	public void loadPrefs(){
		SceneManager.LoadScene ("Scene_SpacePref");
	}

	public void loadLevelDesigner(){
		SceneManager.LoadScene ("Scene_SpaceLevelDesigner");
	}
	public void loadEnemyConfig() {
		SceneManager.LoadScene ("Scene_SpaceConfigEnemies");
	}

	public void loadBackground(){
		SceneManager.LoadScene ("Scene_SpaceConfigBackground");
	}

	public void loadAudio(){
		SceneManager.LoadScene ("Scene_SpaceConfigAudio");
	}

	public void launchGame(){
		SceneManager.LoadScene ("Scene_SpaceMainGame");
	}

	public void loadHist(){
		SceneManager.LoadScene ("Scene_SpaceHistory");
	}
		
		
}
