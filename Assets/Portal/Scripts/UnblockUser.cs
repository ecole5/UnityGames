using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UnblockUser: MonoBehaviour {

    //Ui text references
	public Text inputText;
	public Text statusText;


	public void submitButton(){
        //Make sure entered username is alphanumeric to prevent SQL injection
		if (!Utility.IsAlphaNumeric (inputText.text)) {
			statusText.text = "Please make sure the username is alphanumeric";
		} 
		else{
            //update status and connect to server
			statusText.text = "Attempting to unblock specified user.";
			StartCoroutine (unblockUser());

		}
	}
		

	private IEnumerator unblockUser() {
		string url = "https://pudpie.com/unblock.php";
		WWWForm form = new WWWForm ();
		form.AddField ("username", inputText.text);
		WWW result = new WWW(url, form);
		yield return result;
        //Network down extended use case
		if (!string.IsNullOrEmpty (result.error)) {
			statusText.text = "Could not connect to network, check connection and try again";
		}
		else {
			statusText.text = result.text;
	
		}
	}
}
