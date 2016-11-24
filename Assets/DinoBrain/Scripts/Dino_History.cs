using UnityEngine;
using UnityEngine.UI;


public class Dino_History : MonoBehaviour
{

    //Reference to text element 
    public Text showText;

    void Awake()
    {
        History.Record[] temp = GameData.Prefs.dinoHist.list; //get list of high scores

        for (int i = 0; i < GameData.Prefs.dinoHist.size; i++) //iterate through the list showing each entry in the UI
        {
            //Show one entry, with the rank
            int rank = i + 1;
            showText.text += "\nRank: " + rank + "\n";
            showText.text += "\nUsername: " + temp[i].username + "\n";
            showText.text += "Date: " + temp[i].date + "\n";
            showText.text += "Score: " + temp[i].score + "\n";
        }

    }





}


