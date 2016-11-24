using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class ChangePassword : MonoBehaviour {

    //Reference to the ui elements
	public Text inputText;
	public Text statusText;
	public GameObject backButton;

	void Awake()
	{
        //Configure the screen for first time password set if necessary
		if (GameData.Prefs.accountType == "NEW" && GameData.Prefs.username != "admin") { 
			Destroy (backButton.gameObject);
			statusText.text = "Your a first time user. Please enter your own password to continue.";
		}
	}

	public void submitButton()
	{
        //Make sure new password is alphanumeric to prevent SQL injection
		if (!Utility.IsAlphaNumeric (inputText.text)) {
			statusText.text = "Please make sure the password is alphanumeric";
		} 
        //Make sure password is not username because this is how server i

		else if (inputText.text == GameData.Prefs.username) {
			statusText.text = "Password cannot be username";
		}
		else { //connect with the server and change password
			statusText.text = "Attempting to change password.";
			StartCoroutine (changePass());

		}
	}


	public void loadHome()
	{
        //Home screen button
		SceneManager.LoadScene("PortalHome");
	}

    //Connect
	private IEnumerator changePass() {
		string url = "http://pudpie.com/changePassword.php";
		WWWForm form = new WWWForm ();
		form.AddField ("password", inputText.text);
		form.AddField ("username", GameData.Prefs.username);
		WWW result = new WWW(url, form);
		yield return result;

        //Network failure use case
		if (!string.IsNullOrEmpty (result.error)) {
			statusText.text = "Could not connect to network, check connection and try again";
		}
		else {

            //Make sure account type is regular, so that if they enter this screen from portal it doest destroy the back button
			if (GameData.Prefs.accountType == "NEW") {
				statusText.text = "Thank you for setting password";
				GameData.Prefs.accountType = "regular";
				Invoke ("loadHome", 0.5f);
			}
			else{
				statusText.text = "Password changed successfully";
			}
		}
	}
}
