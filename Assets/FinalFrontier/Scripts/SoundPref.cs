using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class SoundPref : MonoBehaviour
{

	//Get references to all the dropdowns and sliders
	public Dropdown backgroundMusic, winMusic, shootSound, destroySound, levelSelect;
	public Slider backgroundVolume, winVolume, shootVolume, destroyVolume;

	public AudioSource juxBox;
	//will play all the sounds in this configuration scene
	public AudioClip click;
	//reference to the click track


	bool initalSet = true;
	//when true preview all sounds from playing (otherwise they would all play when the scene was loaded)

	void Start ()
	{
		//Set all the volumes and tracks to the ones specified in the preferences object
		backgroundVolume.value = GameData.Prefs.space.backgroundVolume;
		winVolume.value = GameData.Prefs.space.winVolume;
		shootVolume.value = GameData.Prefs.space.shootVolume;
		destroyVolume.value = GameData.Prefs.space.destroyVolume;
		backgroundMusic.value = GameData.Prefs.space.backgroundChoice;
		winMusic.value = GameData.Prefs.space.winChoice [0]; //default to show bronze level
		shootSound.value = GameData.Prefs.space.shootChoice;
		destroySound.value = GameData.Prefs.space.destroyChoice;
		initalSet = false; //now we do want to hear the preview upon a change of one of these fields 

	}

	//Controls which level the win music is being set for
	public void setLevel ()
	{
		winMusic.value = GameData.Prefs.space.winChoice [levelSelect.value];
	}

	//Used to stop music after being called by invoke with delay (for long tracks)
	public void stopEarly ()
	{
		juxBox.Stop ();
	}
		

	//The following functions set which track is played in different sceneries-------------------
	public void setWin ()
	{
		if (!initalSet) {//do not run on initial set 
			//Track sample will always be full volume
			juxBox.volume = 1; 
			//Update preferences
			GameData.Prefs.space.winChoice [levelSelect.value] = winMusic.value;
			//Get sample sound
			juxBox.clip = MissionControl.huston.winSounds [winMusic.value];
			//Play sound 
			juxBox.Play ();
		}
	}

	public void setBackground ()
	{
		if (!initalSet) {//do not run on initial set 
			juxBox.volume = 1; 
			juxBox.clip = MissionControl.huston.backgroundSongs [backgroundMusic.value];
			GameData.Prefs.space.backgroundChoice = backgroundMusic.value;
			juxBox.Play ();
			Invoke ("stopEarly", 3); //cut the sound after 3 seconds because background tracks are long

		}

	}

	public void setShoot ()
	{
		if (!initalSet) {
			juxBox.volume = 1;
			juxBox.clip = MissionControl.huston.shootSounds [shootSound.value];
			GameData.Prefs.space.shootChoice = shootSound.value;
			juxBox.Play ();
		}
	}

	public void setDestroy ()
	{
		if (!initalSet) {
			juxBox.volume = 1;
			juxBox.clip = MissionControl.huston.destroySounds [destroySound.value];
			GameData.Prefs.space.destroyChoice = destroySound.value;
			juxBox.Play ();
		}
	}
	//-------------------------------------------------------

	//The Following Functions set the volume based on the values of the sliders
	public void setBackgroundVolume ()
	{
		if (!initalSet) {//do not run on initial set 

			//Update the volume in preferences
			GameData.Prefs.space.backgroundVolume = backgroundVolume.normalizedValue;
			//Update the volume for the audio source
			juxBox.volume = GameData.Prefs.space.backgroundVolume;
			//Load the sample clip into the audio source
			juxBox.clip = click; 
			//Play the audio source
			juxBox.Play ();
		}

	}

	public void setWinVolume ()
	{
		if (!initalSet) {
			
			GameData.Prefs.space.winVolume = winVolume.normalizedValue;
			juxBox.volume = GameData.Prefs.space.winVolume;
			juxBox.clip = click;
			juxBox.Play ();
		}

	}

	public void setShootVolume ()
	{
		if (!initalSet) {
			GameData.Prefs.space.shootVolume = shootVolume.normalizedValue;
			juxBox.volume = GameData.Prefs.space.shootVolume;
			juxBox.clip = click;
			juxBox.Play ();
		}

	}

	public void setDestroyVolume ()
	{
		if (!initalSet) { 
			GameData.Prefs.space.destroyVolume = destroyVolume.normalizedValue;
			juxBox.volume = GameData.Prefs.space.destroyVolume;
			juxBox.clip = click;
			juxBox.Play ();
		}

	}
	//--------------------------------------------------------

}
