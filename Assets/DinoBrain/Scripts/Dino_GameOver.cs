using UnityEngine;
using UnityEngine.UI;

public class Dino_GameOver : MonoBehaviour
{

    //references to UI elements
    public Text scoreText;
    public Text resultText;
    public Text timeText;

    void Awake()
    {
        //Set the end game score text
        scoreText.text = "Score " + Dino_MainGame.gameStats.score;

        //Set the end game result text
        if (Dino_MainGame.gameStats.score == 0)
        { //if score is 0
            resultText.text = "You Lose";
            AudioController.speaker.playClip(4); //play long sad sound
        }
        else
        { //if score is not 0
            resultText.text = "You Win";
            AudioController.speaker.playClip(3); //play long win sound
        }
        //Set the end game time to four digest
        timeText.text = "Time " + ((int)Dino_MainGame.gameStats.time).ToString();

    }



}
