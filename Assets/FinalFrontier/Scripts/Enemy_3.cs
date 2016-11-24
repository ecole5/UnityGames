﻿using UnityEngine;
using System.Collections;


public class Enemy_3 : Enemy {

	//Enemy_3 movement based on Bezier curve
	//Curve properties
	public Vector3[] points;
	public float birthTime;
	public float lifeTime = 10;

	//Remember start works because it is not used by enemy
	void Start () {
		points = new Vector3[3]; // initialize points
		points[0] = pos;

		// Set xMin and xMax 
		float xMin = Utils.camBounds.min.x+Main.S.enemySpawnPadding;
		float xMax = Utils.camBounds.max.x-Main.S.enemySpawnPadding;
		Vector3 v;

		//Random middle position in bottom half of screen
		v = Vector3.zero;
		v.x = Random.Range( xMin, xMax );
		v.y = Random.Range( Utils.camBounds.min.y, 0 );
		points[1] = v;

		//Random final position at top of screen
		v = Vector3.zero;
		v.y = pos.y;
		v.x = Random.Range( xMin, xMax );
		points[2] = v;

		//Set birthTime
		birthTime = Time.time;
	}

	public override void Move() {
		float u = (Time.time - birthTime) / lifeTime;

		if (u > 1) {
			Destroy( this.gameObject );
			return;
		}

		//Interpolate the three Bezier curve points
		Vector3 p01, p12;
		p01 = (1-u)*points[0] + u*points[1];
		p12 = (1-u)*points[1] + u*points[2];
		pos = (1-u)*p01 + u*p12;
	}
}