using UnityEngine;
using UnityEngine.UI;

public class PortalBackgroundConfig : MonoBehaviour {

    //References to UI elements
	public Image preview; //reference to preview image
	public Dropdown selector;
	void Awake()
	{
        //Set the Ui to reflect current selected values in game data
		selector.value = GameData.Prefs.portal.backgroundChoice;
		preview.sprite = GameData.Prefs.backgroundImages [GameData.Prefs.portal.backgroundChoice]; 
	}

 
	public void setBackground(int choice)
	{
        ///Update the background in the UI and game data
		GameData.Prefs.portal.backgroundChoice = choice; //change selection in preferences 
		preview.sprite = GameData.Prefs.backgroundImages [choice]; 

	}
		
		
}
