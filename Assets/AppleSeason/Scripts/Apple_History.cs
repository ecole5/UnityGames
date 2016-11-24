using UnityEngine;
using UnityEngine.UI;

public class Apple_History : MonoBehaviour
{

    //This class displays the high scores of Apple season

    //Reference to the text element
    public Text showText;

    void Awake()
    {
        History.Record[] temp = GameData.Prefs.appleHist.list; //get list of all the history entries from game data


        for (int i = 0; i < GameData.Prefs.appleHist.size; i++)
        { //loop through all the entries
            int rank = i + 1;
            //Show one entry
            showText.text += "\nRank: " + rank + "\n";
            showText.text += "Username: " + temp[i].username + "\n";
            showText.text += "Date: " + temp[i].date + "\n";
            showText.text += "Score: " + temp[i].score + "\n";
        }

    }

}


