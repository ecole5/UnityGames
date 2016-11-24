using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour {

	//Enemy Attributes
	public float speed = 10f; 
	public float fireRate = 0.3f; 
	public float health = 10;
	public int score = 100; // Points earned for destroying
	public int showDamageForFrames = 2; //# frames to show damage
	public float powerUpDropChance = 1f; 
	public bool ________________;
	public Color[] originalColors;
	public Material[] materials;// All the Materials of this and children
	public int remainingDamageFrames = 0; //Damage frames left
	public Bounds bounds; //The Bounds of this and children
	public Vector3 boundsCenterOffset; // Dist of bounds.center from position


	//NEW: parses the int color values from preferences to their matching colors
	Color parseColor(int temp){
		switch (temp) {
		case (0):
			return Color.green;
		case (1):
			return Color.blue;
		default: //note C# requires default
			return Color.yellow;
		}
	}

	//NEW: returns color based on preferences and type of enemy
	Color getColor()
	{
		switch (gameObject.tag){
		case ("Enemy1"):
			return parseColor (GameData.Prefs.space.enemyColor [0]);
		case("Enemy2"):
			return parseColor (GameData.Prefs.space.enemyColor [1]);
		case("Enemy3"):
			return parseColor (GameData.Prefs.space.enemyColor [2]);
		case("Enemy4"):
			return parseColor (GameData.Prefs.space.enemyColor [3]);
		default:
			return parseColor (GameData.Prefs.space.enemyColor [4]);

		}
	}

	void Awake() {
		materials = Utils.GetAllMaterials( gameObject );
		originalColors = new Color[materials.Length];
		for (int i=0; i<materials.Length; i++) {

			Color temp = getColor (); //NEW: get user color from preferences
			materials [i].color = temp; //NEW: assign all the materials the color
			originalColors[i] = temp;  //NEW: assign to original colors so that it may be restored after hit
		}


		InvokeRepeating( "CheckOffscreen", 0f, 2f );
	}
		

	void Update() {
		Move();
		if (remainingDamageFrames>0) {
			remainingDamageFrames--;
			if (remainingDamageFrames == 0) {
				UnShowDamage();
			}
		}
	}

	public virtual void Move() {
		Vector3 tempPos = pos;
		tempPos.y -= speed * Time.deltaTime; //update y movement
		pos = tempPos;
	}
		

	//Property for getting and setting transform 
	public Vector3 pos {
		get {
			return( this.transform.position );
		}
		set {
			this.transform.position = value;
		}
	}


	void CheckOffscreen() { //used to check if enemy has gone off screen

		//Initial bound set
		if (bounds.size == Vector3.zero) {
			bounds = Utils.CombineBoundsOfChildren(this.gameObject);
			boundsCenterOffset = bounds.center - transform.position;
		}



		//update bounds to current position (happens every time)
		bounds.center = transform.position + boundsCenterOffset;

		//check if bounds off screen
		Vector3 off = Utils.ScreenBoundsCheck( bounds, BoundsTest.offScreen );
		if ( off != Vector3.zero ) { 
			
			//Check if entirely off screen
			if (off.y < 0) {
				Destroy( this.gameObject );
			}
		}
	}

	void OnCollisionEnter( Collision coll ) {
		GameObject other = coll.gameObject;
		switch (other.tag) {
		case "ProjectilePlayer":
			Projectile p = other.GetComponent<Projectile>();

			// Enemies don't take damage unless they're on screen
			// This stops the player from shooting them before they are visible
			bounds.center = transform.position + boundsCenterOffset;
			if (bounds.extents == Vector3.zero || Utils.ScreenBoundsCheck(bounds,
				BoundsTest.offScreen) != Vector3.zero) {
				Destroy(other);
				break;
			}

			//Hurt this Enemy
			ShowDamage();
			//Get the damage amount from the Projectile.type & Main.W_DEFS
			health -= Main.W_DEFS[p.type].damageOnHit;

			if (health <= 0) {
				Main.S.ShipDestroyed (this); //tells main that ship has been destroyed

				// Destroy this Enemy
				Destroy(this.gameObject);
			}

			Destroy(other);
			break;
		}
	}

	void ShowDamage() {
		foreach (Material m in materials) {
			m.color = Color.red;
		}
		remainingDamageFrames = showDamageForFrames;
	}
	void UnShowDamage() {
		for ( int i=0; i<materials.Length; i++ ) {
			materials[i].color = originalColors[i];
		}
	}
}
