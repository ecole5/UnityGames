using UnityEngine;

public class HighScore : MonoBehaviour
{

    static public int score = 1000;

    void Awake()
    {
        // Reads existing score if it exists
        if (PlayerPrefs.HasKey("ApplePickerHighScore"))
        {
            score = PlayerPrefs.GetInt("ApplePickerHighScore");
        }
        //Sets High Score
        PlayerPrefs.SetInt("ApplePickerHighScore", score);
    }


    // Update is called once per frame
    void Update()
    {
        //display updates
        GUIText gt = this.GetComponent<GUIText>();
        gt.text = "High Score: " + score;

        // If new high score reach update in PlayPrefs 
        if (score > PlayerPrefs.GetInt("ApplePickerHighScore"))
        {
            PlayerPrefs.SetInt("ApplePickerHighScore", score);
        }
    }
}

