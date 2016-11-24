using UnityEngine;
using System.Collections;

public class Parallax : MonoBehaviour {

	//Properties of parallax effect
	public GameObject poi; //player ship
	public GameObject[] panels; //scrolling foregrounds
	public float scrollSpeed = -30f;

	//controls how much panels react to player movement
	public float motionMult = 0.25f;
	private float panelHt; //height of panels
	private float depth; // Depth of panels


	void Start () {
		panelHt = panels[0].transform.localScale.y;
		depth = panels[0].transform.position.z;
		// Set initial positions of panels
		panels[0].transform.position = new Vector3(0,0,depth);
		panels[1].transform.position = new Vector3(0,panelHt,depth);
	}


	void Update () {
		float tY, tX=0;
		tY= Time.time * scrollSpeed % panelHt + (panelHt*0.5f);
		if (poi != null) {
			tX = -poi.transform.position.x * motionMult;
		}
		// First Position panels[0]
		panels[0].transform.position = new Vector3(tX, tY, depth);

		// Then position panels[1] in spot needed in order for the stars to be continuous
		if (tY >= 0) {
			panels[1].transform.position = new Vector3(tX, tY-panelHt, depth);
		} else {
			panels[1].transform.position = new Vector3(tX, tY+panelHt, depth);
		}
	}
}