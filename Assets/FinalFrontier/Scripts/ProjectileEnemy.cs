using UnityEngine;
using System.Collections;

public class ProjectileEnemy : MonoBehaviour {


	void Awake()
	{
		InvokeRepeating ("CheckOffscreen", 2f, 2f);
	}


	void CheckOffscreen() {
		if ( Utils.ScreenBoundsCheck( GetComponent<Collider>().bounds, BoundsTest.offScreen ) !=
			Vector3.zero ) {
			Destroy( this.gameObject );
		}
	}
	//Movement for enemy projectile
	void Update () {
		Vector3 tempPos = this.transform.position;
		tempPos.y -= 20f * Time.deltaTime; //update y movement
		this.transform.position = tempPos;
	}
}
