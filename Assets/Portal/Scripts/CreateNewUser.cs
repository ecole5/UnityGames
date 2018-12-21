using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class CreateNewUser : MonoBehaviour {

    //Reference to the ui elements 
	public Text inputText;
	public Text statusText;

	public void submitButton()
	{
        //Check alphanumeric to avoid SQL injection 
		if (!Utility.IsAlphaNumeric (inputText.text)) {
			statusText.text = "Please make sure the username is alphanumeric";
		} else {
            //Start server connection
			statusText.text = "Attempting to create new user.";
			StartCoroutine (newUser());

		}
	}

	private IEnumerator newUser() {
		string url = "https://evancole.io/newuser.php";
		WWWForm form = new WWWForm ();
        
        //Create form and post to server
		form.AddField ("spacePrefs", JsonUtility.ToJson (new SpacePrefs()));
		form.AddField ("portalPrefs", JsonUtility.ToJson (new PortalPrefs()));
		form.AddField ("username", inputText.text);
		WWW result = new WWW(url, form);
		yield return result;

        //This script will return nothing if it couldn't find user, so check if the return string is null or empty 
		if (!string.IsNullOrEmpty (result.error)) {
			statusText.text = "Could not connect to network, check connection and try again";
		}
		else {
			statusText.text = result.text;
		}
	}
}
