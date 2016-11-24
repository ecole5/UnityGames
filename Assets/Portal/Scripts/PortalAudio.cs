using UnityEngine;

public class PortalAudio : MonoBehaviour {

    //Get AudioSources And Clips
    public AudioSource music; //for background music
    public AudioSource click; //for button clips
    public AudioClip[] soundClips; //reference to all sound clips
	public static PortalAudio output;

    void Awake()
    {
		 //Enact singleton design pattern
			if (output == null)
			{
				DontDestroyOnLoad(gameObject);
				output = this;
				click.clip = soundClips[0]; //click sound is at index 0
				
			}
			else if (output != this){
				Destroy(gameObject);
			}
				
		
    }

	//Plays click sound
    public void playClick()
    {
        click.Play();
    }




	public void stop()
	{
		music.Stop ();
	}



    public void playMusic() //enter song one two or three
    {
		music.clip = soundClips [GameData.Prefs.portal.musicChoice + 1];
		music.volume = GameData.Prefs.portal.musicVolume;
		music.Play ();
	}

    public void playLogin() //play login song
	{
		music.clip = soundClips [1]; //login music is always the same
		music.Play ();
	}

}

