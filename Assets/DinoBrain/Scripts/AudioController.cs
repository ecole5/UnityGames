using UnityEngine;

public class AudioController : MonoBehaviour {

	public static AudioController speaker; //reference to self
	public AudioClip [] soundClips; //reference to all sound clips
	AudioSource source; //audio source component 

	void Awake(){

		//Keep one and only instance alive throughout game 
		if (speaker == null) {
			DontDestroyOnLoad (gameObject);
			speaker = this;
			source = gameObject.GetComponent<AudioSource>(); //get the audio source component 
		} else if (speaker != this)
			Destroy (gameObject);
	}

	public void playClip(int clipNumber) //play a clip
	{
		source.clip = soundClips [clipNumber];
		source.Play ();
	}

	public void cutShort(){ //cut a clip short
		source.Stop ();
	}
}
