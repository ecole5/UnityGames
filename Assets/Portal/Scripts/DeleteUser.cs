using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class DeleteUser : MonoBehaviour {


    //Get UI Text References
	public Text inputText;
	public Text statusText;


	public void submitButton(){
        //Make sure alphanumeric to avoid SqL injection
		if (!Utility.IsAlphaNumeric (inputText.text)) {
			statusText.text = "Please make sure the username is alphanumeric";
            //prevent deletion of admin account
		} else if (inputText.text == "admin"){

			statusText.text = "Cannot delete the admin account.";
		}
		else{
            //contact the server and delete 
			statusText.text = "Attempting to delete specified user.";
			StartCoroutine (deleteUser());

		}
	}
		

	private IEnumerator deleteUser() {
        //Create the form and submit it to the server
		string url = "http://pudpie.com/deleteuser.php";
		WWWForm form = new WWWForm ();
		form.AddField ("username", inputText.text);
		WWW result = new WWW(url, form);
		yield return result;
        //Network done extended use case
		if (!string.IsNullOrEmpty (result.error)) {
			statusText.text = "Could not connect to network, check connection and try again";
		}
		else {
			statusText.text = result.text;
			if (result.text == "User deleted successfully")
                //Purge All the history object of that user 
                HistoryMethods.remove (inputText.text, GameData.Prefs.spaceHist);
				HistoryMethods.remove (inputText.text, GameData.Prefs.appleHist);
				HistoryMethods.remove (inputText.text, GameData.Prefs.ninjaHist);
				HistoryMethods.remove (inputText.text, GameData.Prefs.dinoHist);
				HistoryMethods.remove (inputText.text, GameData.Prefs.portalHist);
				HistoryMethods.remove (inputText.text, GameData.Prefs.spaceHist);
                //Save the history objects
				GameData.Prefs.saveData ();

		}
	}
}
