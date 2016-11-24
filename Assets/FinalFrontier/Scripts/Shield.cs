using UnityEngine;
using System.Collections;

public class Shield : MonoBehaviour {

	//Shield Properties
	public float rotationsPerSecond = 0.1f; //shield rotation speed 
	public bool ________________;
	public int levelShown = 0; //initial shield level set to zero

	void Update () {

		//Get shield level from parent
		int currLevel = Mathf.FloorToInt( Player.S.shieldLevel ); //flooring ensures that we actually jump to a new x offset

		//Update shield level if different
		if (levelShown != currLevel) {
			levelShown = currLevel;
			Material mat = this.GetComponent<Renderer>().material; //gets the material

			// Adjust the texture (offset) based on the new or current level 
			mat.mainTextureOffset = new Vector2( 0.2f*levelShown, 0 ); 
		}

		//Shield rotation animation
		float rZ = (rotationsPerSecond*Time.time*360) % 360f; // 3
		transform.rotation = Quaternion.Euler( 0, 0, rZ );
	
	}
}
