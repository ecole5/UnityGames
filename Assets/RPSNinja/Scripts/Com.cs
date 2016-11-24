using UnityEngine;
using System.Collections;

//Script to detect collision, attached to all the prefabs (the computers player)

public class Com : MonoBehaviour {

	private MainAction actionScript; //reference to the driver scripts (the driver is a prefab that controls the game logic)
	GameObject temp;

	void OnTriggerEnter(Collider other) {
		actionScript.getResult (); //get the result of the match up using the drivers script

	}
		
	void Update () {
		if (temp == null) {
			temp = GameObject.FindGameObjectWithTag ("driver"); //get a reference to the driver
			actionScript = temp.GetComponent<MainAction> (); //get a reference to the drivers script
		}
	}
}
