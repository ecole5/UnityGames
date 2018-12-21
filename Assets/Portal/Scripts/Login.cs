using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

using System.Collections;


public class Login : MonoBehaviour {

	//Fields to receive data from
	public Text userName, passWord, status;
    public GameObject prefs;

	void Start(){
		PortalAudio.output.playLogin (); //play login music
	}
		
		
    //Quit the application 
	public void quitbutton(){
		Application.Quit ();
	}

    public void loginAction (string result)
     {
        status.text = result;
        if (result == "Login Successful")
		{   
			status.text = "Loading Data";
            Instantiate(prefs); //load the object which holds all the game data

            //Set the username 
			GameData.Prefs.setUsername(userName.text);
			GameData.Prefs.loadData(); //load the data from the server
		
        }

    }


	//Authenticate username and p()password with the server, logic is performed on the server
	private IEnumerator Authenticate(string username, string password)
	{
		status.text = "Connecting to network";

		//Connect to the server and make request
		string url = "https://evancole.io/authenticateV2.php";
		string secretKey = "DrOuda";

		string hash = Utility.newHash (secretKey);
  
		//Create Form with relevant data
		WWWForm form = new WWWForm();
		form.AddField("username", username);
		form.AddField("password", password);
		form.AddField ("hash", hash);

		//Post the request and receive the results
		WWW result = new WWW(url, form);
		yield return result;

        //Network down extended use case
		if (!string.IsNullOrEmpty (result.error)) {
			status.text = "Could not connect to network, check connection and try again";
		}
		else {
			loginAction(result.text); 
		}
       
     
    }
	//Submit button
    public void loginButton() {
        PortalAudio.output.playClick();
        if (Utility.IsAlphaNumeric(userName.text) && Utility.IsAlphaNumeric(passWord.text)) { //if alphanumeric begin server authentication
            StartCoroutine(Authenticate(userName.text, passWord.text));
        }
        else //not alphanumeric
        {
			status.text = "Username and password must be letter or number with no spaces";
        }
        
		
	}
		

}
