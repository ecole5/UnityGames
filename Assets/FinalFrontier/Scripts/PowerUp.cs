using UnityEngine;
using System.Collections;

public class PowerUp : MonoBehaviour {

	//Power Up attributes
	// Vector2s. x holds a min value and y a max value which will be called latter (notice this creative use)
	public Vector2 rotMinMax = new Vector2(15,90);
	public Vector2 driftMinMax = new Vector2(.25f,2);
	public float lifeTime = 6f; // Seconds of assistance
	public float fadeTime = 4f; // Seconds until fade 
	public bool ________________;
	public WeaponType type; //Type of the PowerUp
	public GameObject cube; 
	public TextMesh letter; 
	public Vector3 rotPerSecond; //rotation speed
	public float birthTime;

	void Awake() {
		cube = transform.Find("Cube").gameObject; //find cube reference 
		letter = GetComponent<TextMesh>(); //find text mesh
		Vector3 vel = Random.onUnitSphere; // Get Random XYZ velocity
		vel.z = 0; 
		vel.Normalize(); // Make the length of the vel 1

		//Remember normalizing a Vector3 makes it length 1
		//set the velocity length to something between the x and y values of driftMinMax
		vel *= Random.Range(driftMinMax.x, driftMinMax.y);

		GetComponent<Rigidbody>().velocity = vel;

		// Set the rotation of this GameObject to R:[0,0,0]
		transform.rotation = Quaternion.identity;

		// Quaternion.identity is equal to no rotation.
		// Set up the rotPerSecond for the Cube child using rotMinMax
		rotPerSecond = new Vector3( Random.Range(rotMinMax.x,rotMinMax.y),
			Random.Range(rotMinMax.x,rotMinMax.y),
			Random.Range(rotMinMax.x,rotMinMax.y) );
		InvokeRepeating( "CheckOffscreen", 2f, 2f ); //offscreen check occurs every two seconds 
		birthTime = Time.time;
	}

	void Update () {
		//Time based rotation update 
		cube.transform.rotation = Quaternion.Euler( rotPerSecond*Time.time );
		// Fade out the PowerUp over time
		// Given the default values, a PowerUp will exist for 10 seconds
		// and then fade out over 4 seconds.
		float u = (Time.time - (birthTime+lifeTime)) / fadeTime;
		// For lifeTime seconds, u will be <= 0. Then it will transition to 1
		// over fadeTime seconds.
		// If u >= 1, destroy this PowerUp
		if (u >= 1) {
			Destroy( this.gameObject );
			return;
		}

		// Use u to determine the alpha value of the Cube & Letter
		if (u>0) {
			Color c = cube.GetComponent<Renderer>().material.color;
			c.a = 1f-u;
			cube.GetComponent<Renderer>().material.color = c;
			c = letter.color; //letter fade
			c.a = 1f - (u*0.5f);
			letter.color = c;
		}
	}

	public void SetType( WeaponType wt ) {
		//Obtain WeaponDefinition from Main
		WeaponDefinition def = Main.GetWeaponDefinition( wt );
		// Set the color of the Cube child
		cube.GetComponent<Renderer>().material.color = def.color;
		letter.text = def.letter; //set the letter that is shown
		type = wt; //actually set the type
	}

	// This function called by the Player class when a PowerUp is collected
	public void AbsorbedBy( GameObject target ) {
		Destroy( this.gameObject );
	}

	void CheckOffscreen() { //check for drifting
		// If the PowerUp has drifted entirely off screen
		if (Utils.ScreenBoundsCheck (cube.GetComponent<Collider>().bounds,
			    BoundsTest.offScreen) != Vector3.zero) {
			//then destroy it
			Destroy (this.gameObject);
		}
	}

}