using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PortalAudioConfig: MonoBehaviour
{

	//Get references to all the dropdowns and sliders
	public Dropdown portalMusic;
	public Slider portalVolume;




	bool initalSet = true;
	//when true preview all sounds from playing (otherwise they would all play when the scene was loaded)

	void Start ()
	{
		//Set all the volumes and tracks to the ones specified in the preferences object
		portalVolume.value = GameData.Prefs.portal.musicVolume;
		portalMusic.value = GameData.Prefs.portal.musicChoice-1;
		initalSet = false; //now we do want to hear the preview upon a change of one of these fields 

	}



	//The following functions set which track is played in different scenarios-------------------
	public void setBackgroundMusic ()
	{
		if (!initalSet) {//do not run on initial set 
			//Update preferences
			GameData.Prefs.portal.musicChoice = portalMusic.value+1;
			//Get sample sound
			PortalAudio.output.playMusic();
		}
	}
		


	//-------------------------------------------------------

	//The Following Functions set the volume based on the values of the sliders
	public void setBackgroundVolume ()
	{
		if (!initalSet) {//do not run on initial set 
			PortalAudio.output.stop();
			//Update the volume in preferences
			GameData.Prefs.portal.musicVolume = portalVolume.normalizedValue;
			//Load the sample clip into the audio source
			PortalAudio.output.playMusic();
			PortalAudio.output.playClick();


	
		}

	}





}
