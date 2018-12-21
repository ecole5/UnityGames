using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
#pragma warning disable 0219 //disable variable assigned but not used warning

//This class holds all the game data, both history and preferences
//It has methods to load and save the data to the server
//It is a singleton and never gets destroyed


public class GameData: MonoBehaviour
{ 
	//Game data objects
    public string username; //username of the active user
	public string accountType; //used to determine which scene to use after login (if they need to set password or not)
    public SpacePrefs space; //preferences for final frontier game
	public PortalPrefs portal; //user preferences for the portal
    //All the shared history objected for the games and portal
	public History spaceHist;
	public History portalHist;
	public History ninjaHist;
	public History dinoHist;
	public History appleHist;
	public Sprite []backgroundImages; //possible background images for all portal screens

    public static GameData Prefs; //reference to self 


	//Note user information is either derived through logic or stored in data base table using php, only preferences and history are delivered from database as objects
	//Every player has their own space and portal prefs object
	//The history objects are communal 

   void Awake() { 
        //set static reference and destroy and dont allow any others to be created (will be created in the login screen)
		if (Prefs == null)
		{ DontDestroyOnLoad(gameObject);
			Prefs = this;
		}
		else if (Prefs != this){
			Destroy(gameObject); }
    }

    //Load data from server (this will be issued by other objects)
	public void loadData(){
		StartCoroutine (loadServerData ()); //start the coroutine for download
	}

	//Load data from server
	private IEnumerator loadServerData(){
		//Send username to load script on server
		string url = "https://evancole.io/loadV2.php?";
		string post = url + "username=" + username;
		WWW result = new WWW (post);
		yield return result;
	
		//Now break apart objects
		char delimiter= '#';
		string[] parts = result.text.Split(delimiter);


		//Undo Serialization
		space = JsonUtility.FromJson<SpacePrefs>(parts[0]);
		portal = JsonUtility.FromJson<PortalPrefs>(parts[1]);
		spaceHist = JsonUtility.FromJson<History>(parts[2]);
		portalHist = JsonUtility.FromJson<History>(parts[3]);
		ninjaHist = JsonUtility.FromJson<History>(parts[4]);
		dinoHist = JsonUtility.FromJson<History>(parts[5]);
		appleHist = JsonUtility.FromJson<History>(parts[6]);

        //Get account status, to determine action at login
		WWWForm form = new WWWForm();
		form.AddField("username", username);
		//Post the request and receive the results
		WWW result2 = new WWW("https://evancole.io/accountStatus.php", form);
		yield return result2;
		accountType = result2.text;


		PortalAudio.output.playMusic (); //put here to make sure it plays after loading the data
		if (accountType == "NEW" && username != "admin" ) {//for non admin's, force them to the set password screen if accounts are new
			SceneManager.LoadScene("Password"); 
		} else {
			SceneManager.LoadScene("PortalHome"); //otherwise load portal home
		}


	}
    
     /*Function to reset the history objects, use carefully
	public void resetUserData(){	
		StartCoroutine (saveDataServer (new SpacePrefs(), new PortalPrefs(), new History(), new History(), new History(), new History(), new History() ));
	}*/
    
    //Save all the currently loaded objects
	public void saveData(){	
		StartCoroutine (saveDataServer (space, portal, appleHist,portalHist, spaceHist, ninjaHist, dinoHist ));
	}

	//Saves data to the server
	private IEnumerator saveDataServer(SpacePrefs t1, PortalPrefs t2, History apple, History portal, History space, History ninja, History dino) {
		string url = "https://evancole.io/saveV2.php";
		WWWForm form = new WWWForm ();
        //Serialize all the objects and add them to a form 
		form.AddField ("spacePrefs", JsonUtility.ToJson (t1));
		form.AddField ("portalPrefs", JsonUtility.ToJson (t2));
		form.AddField ("portalHist", JsonUtility.ToJson (portal));
		form.AddField ("appleHist", JsonUtility.ToJson (apple));
		form.AddField ("spaceHist", JsonUtility.ToJson (space));
		form.AddField ("ninjaHist", JsonUtility.ToJson (ninja));
		form.AddField ("dinoHist", JsonUtility.ToJson (dino));
		form.AddField ("username", username);
        //submit the form to the URL and have the php deliver the result
		WWW result = new WWW(url, form);
		yield return result; //wait for the result
    }

	//Create new user script
	private IEnumerator newUser(){
		SpacePrefs t1 = new SpacePrefs();
		PortalPrefs t2 = new PortalPrefs ();
		string url = "https://evancole.io/newuser.php?";
		string post = url + "spacePrefs=" + JsonUtility.ToJson(space) + "&portalPrefs=" + JsonUtility.ToJson(portal) + "&username=" + username;
		WWW result = new WWW(post);
		yield return result;
	}

    //Set the username
    public void setUsername (string username) {
        this.username = username;
    }
		
}
