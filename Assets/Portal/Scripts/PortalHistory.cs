using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PortalHistory : MonoBehaviour {
	public Text showText;

    //Fetch each account status from the server and display it along with history record (for admin only)
	public IEnumerator checkType(History.Record [] temp) {
		string wholeText = "";
		showText.text = "Fetching History...";
		for (int i = 0; i < GameData.Prefs.portalHist.size; i++){
			
		WWWForm form = new WWWForm();
		form.AddField("username", temp[i].username);
		//Post the request and receive the results
		WWW result = new WWW("http://pudpie.com/accountStatus.php", form);
		yield return result;
			wholeText += "\nUsername: " + temp[i].username + "\n";
			wholeText  += "Date: " + temp[i].date + "\n";
			wholeText += "Time Spent: " + temp[i].time + "\n";
			if (temp [i].username == "admin")
				wholeText += "Account Status: Master" + "\n";
		else {
		 wholeText += "Account Status: " + result.text + "\n";
		}
		}
		showText.text = wholeText;
	}





	void Awake(){
		History.Record[] temp = GameData.Prefs.portalHist.list; //get portal history from GameData

		if (GameData.Prefs.username == "admin") {
			StartCoroutine (checkType (temp));
		} 

		else {

            //Display regular user history record
			for (int i = 0; i < GameData.Prefs.portalHist.size; i++) {
				if (GameData.Prefs.username != "admin") {
					if (GameData.Prefs.username == temp [i].username) {
						showText.text += "\nUsername: " + temp [i].username + "\n";
						showText.text += "Date: " + temp [i].date + "\n";
						showText.text += "Time Spent: " + temp [i].time + "\n";
					}
				} 
			} //
	


		}
	}

}