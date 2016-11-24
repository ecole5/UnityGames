using UnityEngine;
using System.Collections;


//Need to serialize weapon part so we can use in the inspector
[System.Serializable]

public class Part {
	//Part properties
	public string name; 
	public float health; 
	public string[] protectedBy;
	public GameObject go; //The GameObject of this part
	public Material mat; //The Material to show damage
}
	
public class Enemy_4 : Enemy {
	//Enemy Properties
	public Vector3[] points; 
	public float timeStart; 
	public float duration = 4; 
	public Part[] parts; //The array of ship Parts

	void Start () {
		points = new Vector3[2];
		points[0] = pos;
		points[1] = pos;
		InitMovement();

		//Cache GameObject & material of each part in parts
		Transform t;
		foreach(Part prt in parts) {
			t = transform.Find(prt.name);
			if (t != null) {
				prt.go = t.gameObject;
				prt.mat = prt.go.GetComponent<Renderer>().material;
			}
		}
	}



	void InitMovement() {
		// Pick a new point to move to that is on screen using camera bounds 
		Vector3 p1 = Vector3.zero;
		float esp = Main.S.enemySpawnPadding;
		Bounds cBounds = Utils.camBounds;
		p1.x = Random.Range(cBounds.min.x + esp, cBounds.max.x - esp);
		p1.y = Random.Range(cBounds.min.y + esp, cBounds.max.y - esp);
		points[0] = points[1];
		points[1] = p1;
		timeStart = Time.time;//Time reset
	}

	public override void Move () {

		float u = (Time.time-timeStart)/duration;
		if (u>=1) { 
			InitMovement();
			u=0;
		}
		u = 1 - Mathf.Pow( 1-u, 2 ); //Ease Out easing to u
		pos = (1-u)*points[0] + u*points[1]; //	//linear interpolation 
	}
		
// This will override the OnCollisionEnter that is part of Enemy.cs
// Note that override keyword not necessary 
void OnCollisionEnter( Collision coll ) {
	GameObject other = coll.gameObject;
	switch (other.tag) {
	case "ProjectilePlayer":
		Projectile p = other.GetComponent<Projectile>();
		//stop enemy from taking damage when not on screen
		bounds.center = transform.position + boundsCenterOffset;
		if (bounds.extents == Vector3.zero || Utils.ScreenBoundsCheck(bounds,
			BoundsTest.offScreen) != Vector3.zero) {
			Destroy(other);
			break;
		}
		// Hurt this Enemy
		// Find the GameObject that was hit
		GameObject goHit = coll.contacts[0].thisCollider.gameObject;
		Part prtHit = FindPart(goHit);
		
			if (prtHit == null) { 
			
				// sometimes contacts[0] will be the ProjectilePlayer instead of the ship
			// If so look for otherCollider instead
			goHit = coll.contacts[0].otherCollider.gameObject;
			prtHit = FindPart(goHit);
		}

		// Check whether part still protected 
		if (prtHit.protectedBy != null) {
			foreach( string s in prtHit.protectedBy ) {
					
				// If one of the protecting parts hasn't been destroyed
				if (!Destroyed(s)) {
					Destroy(other); 
					return; 
				}
			}
		}
		
		// If not protected it must take damage
		// Get the damage amount
		prtHit.health -= Main.W_DEFS[p.type].damageOnHit;
		
		// Show damage on the part
		ShowLocalizedDamage(prtHit.mat);
		if (prtHit.health <= 0) {
			//Disable damaged part instead of destroying
			prtHit.go.SetActive(false);
		}

		//Whole ship destroyed check 
		bool allDestroyed = true; 
		foreach( Part prt in parts ) {
			
				if (!Destroyed(prt)) {
				allDestroyed = false; //if a part still exists the assumption is false
				break; 
			}
		}

		if (allDestroyed) { //is completed destroyed
			Main.S.ShipDestroyed( this ); //contact main
			Destroy(this.gameObject); //destroy enemy 
		}
		Destroy(other); //Destroy the ProjectileHero
		break;
	}
}
		
//These two functions find parts
Part FindPart(string n) {
	foreach( Part prt in parts ) {
		if (prt.name == n) {
			return( prt );
		}
	}
	return( null );
}
Part FindPart(GameObject go) {
	foreach( Part prt in parts ) {
		if (prt.go == go) {
			return( prt );
		}
	}
	return( null );
}
// These functions return true if part is destroyed
bool Destroyed(GameObject go) {
	return( Destroyed( FindPart(go) ) );
}
bool Destroyed(string n) {
	return( Destroyed( FindPart(n) ) );
}
bool Destroyed(Part prt) {
	if (prt == null) { 
		return(true); 
	}
	return (prt.health <= 0);
}
//Changes the color of just one Part to red instead of the whole ship
void ShowLocalizedDamage(Material m) {
	m.color = Color.red;
	remainingDamageFrames = showDamageForFrames;
	}
}