using UnityEngine;
using System.Collections;

public class PortalHome : MonoBehaviour {

	// Use this for initialization
	void Awake () {
		if (GameData.Prefs.username !="admin") {
			GameObject[] gameObjects = GameObject.FindGameObjectsWithTag ("adminOnly"); //destroy menu items tagged admin only
			foreach (GameObject target in gameObjects) {
				Destroy (target);
			}
		}
	
	}
	

}
