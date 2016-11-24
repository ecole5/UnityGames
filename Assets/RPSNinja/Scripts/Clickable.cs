using UnityEngine;
#pragma warning disable 0219 //disable variable assigned but not used warning

public class Clickable : MonoBehaviour {

	private Vector3 org;
	private bool hover = false; //hover is true if user is hovering over any of the screen objects (rock, paper of scissor)
	public GameObject actionPrefab; //the driver prefab that drives the game logic

	void Awake()
	{
		org = transform.position; //get the original position of the object
	}
		

	void OnMouseOver()
	{
		hover = true; //hover is true of mouse is over an object
	}


	void OnMouseDown() //when object is clicked
	{
		//remove the other objects from the screen
		string[] players = { "Rock", "Paper", "Scissor" };
		string temp = this.name;
		for (int i = 0; i < 3; i++) {
			if (temp != players [i]) {
				Destroy (GameObject.Find (players [i]));
			}
	
		}
        tag = "Selected"; //set selected object wit tag
		GameObject temp1 = Instantiate (actionPrefab) as GameObject; //starts the main action script
		Destroy (this); //destroy this script, not the object, so the jiggling stops
	}


	void jiggle() //jiggle the game object on hover
	{
		float newX = transform.position.x + (Mathf.Sin (10f * Time.time) * 0.05f); // move  in a sin pattern 0.05 units left and right from origin
		transform.position = new Vector3 (newX, transform.position.y, transform.position.z); //set the transform
	}

	void Update(){
		
		if (hover == true) { //activate jiggle
			jiggle ();
			hover = false;
		} 

		else { //smooth return to origin if mouse is not hovering anymore
			if (transform.position.x < org.x-0.01 || transform.position.x > org.x +0.01) {jiggle ();}
		}

			
	}//end update

}//end class
		

