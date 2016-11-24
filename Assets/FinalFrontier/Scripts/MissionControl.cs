using UnityEngine;
using UnityEngine.UI;

public class MissionControl : MonoBehaviour
{

	//Will persist throughout the life of the space game to give access to certain sound prefabs and carry messages through scenes 
    public static MissionControl huston;


    public string causeOfDeath; //String to be displayed at gameover screen
    public AudioClip[] backgroundSongs, winSounds, shootSounds, destroySounds; //References to all the possible sounds for each sound type
	public Sprite[] backgroundImages; //References to the different type of background images
    void Awake()
    {

        //enact singleton pattern by destroying duplicates
        if (huston == null)
        {
            DontDestroyOnLoad(gameObject);
            huston = this;

        }
        else if (huston != this)
            Destroy(gameObject);
    }
}