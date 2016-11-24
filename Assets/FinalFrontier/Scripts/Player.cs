using UnityEngine;
using System.Collections;
public class Player : MonoBehaviour {

	static public Player S; //Player is a singleton design pattern, only one object allowed 

	public float gameRestartDelay = 2f; //delay for game restart

	//Ship Movement Properties 
	public float speed = 30; 
	public float rollMult = -45; //changes the roll
	public float pitchMult = 30; //changes the pitch

	// Ship status info
	[SerializeField] //because _shieldLevel will be accessed with a property it should be private however we serialize so we see it in the inspector 
	private float _shieldLevel = 1; //initial level of the shield

	//Weapon fields
	public Weapon[] weapons;
	public bool _____; 
	public Bounds bounds; //bounds of the ship


	public delegate void WeaponFireDelegate();
	// Create a WeaponFireDelegate field named fireDelegate.
	public WeaponFireDelegate fireDelegate;

	void Awake() {
		S = this; // Set the object to be the singleton
		bounds = Utils.CombineBoundsOfChildren(this.gameObject); //calculates bounds
	}

	void Start() {

		ClearWeapons(); //reset weapons
		weapons[0].SetType(WeaponType.blaster); //start with blaster

	}

	void Update () {

		// Get user input from the Input Manager which allows user to customize the input for horizontal and vertical movement
		float xAxis = Input.GetAxis("Horizontal"); 
		float yAxis = Input.GetAxis("Vertical"); 


		Vector3 pos = transform.position; //current position of ship
	
		//position of x and y is updated based on speed and time and user input
		pos.x += xAxis * speed * Time.deltaTime;  //Time.deltaTime is time in seconds it took to complete last frame
		pos.y += yAxis * speed * Time.deltaTime;  //NTS: Difference between time based game 
		transform.position = pos; //actually update the position of the craft 

		bounds.center = transform.position; //the center of the bounds is updated based on ship position 

		// Keep the ship in the screen bounds 
		Vector3 off = Utils.ScreenBoundsCheck(bounds, BoundsTest.onScreen); //perform on screen check, different tests will allow the ship to go off screen in different amounts 
		if ( off != Vector3.zero ) { //if offscreen 
			pos -= off; //adjusts position to be back onscreen by the offset
			transform.position = pos; //update position 
		}

		//Update the rotation of the ship
		//Much like Vector3 repeats a position, Quaternion are objects that represent rotation
		//Quaternion.Euler will return Quaternion which rotates x,y,z degrees around their respective axis
		//We use pitch and roll to alter rotation
		transform.rotation = Quaternion.Euler(yAxis*pitchMult,xAxis*rollMult,0);

		// Use the fireDelegate to fire Weapons
		//make sure the Axis("Jump") button is pressed
		//Then to avoid error ensure that fireDelegate isn't null to avoid an error
		if (Input.GetAxis("Jump") == 1 && fireDelegate != null) { // 1
			fireDelegate();
		}
			

	}
	// This variable holds a reference to the last triggering GameObject
	public GameObject lastTriggerGo = null; // 1

	void OnTriggerEnter(Collider other) { 

		// Find the tag of other.gameObject or its parent GameObjects
		GameObject go = Utils.FindTaggedParent(other.gameObject);

		//When there exists parent with a tag
		if (go != null) {

			if (go == lastTriggerGo) { 
				return;
			}

			lastTriggerGo = go; 

			if ( go.tag == "Enemy1" || go.tag == "Enemy2" || go.tag == "Enemy3" || go.tag == "Enemy4" || go.tag == "Enemy5") { //NEW Modified: shield reduced by enemy of any tag
				shieldLevel--;
				Destroy(go); 
			
			} else if (go.tag == "PowerUp") { //shield triggers by power up
				AbsorbPowerUp(go);

			} else {
				print("Triggered: "+go.name); 
			}

		} else {
			print("Triggered: "+other.gameObject.name);//just for testing!
		}
	}

	public void AbsorbPowerUp( GameObject go ) {
		PowerUp pu = go.GetComponent<PowerUp>();
		switch (pu.type) {
		case WeaponType.shield: //If it's the shield
			shieldLevel++;
			break;
		default: //If isn't any Weapon PowerUp
			// Check  current weapon type
			if (pu.type == weapons[0].type) {
				Weapon w = GetEmptyWeaponSlot(); // Find available weapon
				if (w != null) {
					w.SetType(pu.type);
				}
			} else {
				//If this is a different weapon
				ClearWeapons();
				weapons[0].SetType(pu.type);
			}
			break;
		}
		pu.AbsorbedBy( this.gameObject );
	}
	Weapon GetEmptyWeaponSlot() {
		for (int i=0; i<weapons.Length; i++) {
			if ( weapons[i].type == WeaponType.none ) {
				return( weapons[i] );
			}
		}
		return( null );
	}
	void ClearWeapons() {
		foreach (Weapon w in weapons) {
			w.SetType(WeaponType.none);
		}
	}

	//shield level property to make sure shield level stays in appropriate bounds  
	public float shieldLevel {
		get {
			return( _shieldLevel ); 
		}
		set {
			_shieldLevel = Mathf.Min( value, 4 ); //Mathf.Min ensures that value is not greater than 4

			//Destroys player if shield is less than zero
			if (value < 0) { 
				Destroy(this.gameObject);
				MissionControl.huston.causeOfDeath = "You Lose: Ship Destroyed";
				Main.S.DelayedGameOver (gameRestartDelay); //NEW: go to end game after delay

			}
		}
	}
}