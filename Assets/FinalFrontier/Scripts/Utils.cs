using UnityEngine;
using System.Collections;
using System.Collections.Generic;


//Instead of passing a string into BoundsTest to choses what type of BoundsTest it will be we use an menu
//Enums are a clean method for doing these, when we have only a few possible variables and methods
//They prevent spelling mistakes a support auto complete 
public enum BoundsTest {
	center, //Is the center of the GameObject on screen?
	onScreen, //Are the bounds entirely on screen?
	offScreen //Are the bounds entirely off screen?
}


public class Utils : MonoBehaviour {

	//Encapsulate two bounds 
	public static Bounds BoundsUnion( Bounds b0, Bounds b1 ) {

		if ( b0.size == Vector3.zero && b1.size != Vector3.zero ) { // 1
			return( b1 ); //return b1 is b0 is 0
		} 

		else if ( b0.size != Vector3.zero && b1.size == Vector3.zero ) {
			return( b0 ); //return b0 if b1 is 0
		} 

		else if ( b0.size == Vector3.zero && b1.size == Vector3.zero ) {
			return( b0 );  //if they are both 0 return b0 which is zero
		}

		//If neither of them are zero modify b0 to include the b1.min and b1.max
		b0.Encapsulate(b1.min); //Bounds.encapsalate is the API for growing bounds 
		b0.Encapsulate(b1.max);
		return( b0 ); //return stretched b0
	}


	public static Bounds CombineBoundsOfChildren(GameObject go) {

		// create empty bound
		Bounds b = new Bounds(Vector3.zero, Vector3.zero); 

		//Every GameObject that appears on screen must have a renderer component
		//If renderer exists expand b to contain its bounds
		if (go.GetComponent<Renderer>() != null) {
			b = BoundsUnion(b, go.GetComponent<Renderer>().bounds);
		}

		//Colider are used for the physics engine 
		//If colider exists expand b to contain its bounds
		if (go.GetComponent<Collider>() != null) {
			b = BoundsUnion(b, go.GetComponent<Collider>().bounds);
		}

		// Recursively apply this expansion to each child

		//We use the transform class to get all the child transforms and t

		foreach( Transform t in go.transform ) {  //use the transform class to get all the child transforms &Q
			b = BoundsUnion( b, CombineBoundsOfChildren( t.gameObject ) ); //Through the child transform we can actually get its attached GameObject
		}

		return( b ); //return new bounds 
	}

	//NEW CONCEPT "Property": sadly gets and sets a private data member, get or set is called automatically 
	//This static property is the boundary of the camera, it only has a get so it is read only 
	static public Bounds camBounds { 
		get {

			//if _camBounds not set 
			if (_camBounds.size == Vector3.zero) {
				
				SetCameraBounds(); // SetCameraBounds using the default Camera
			}
			return( _camBounds );
		}
	}

	//Private static field that the camBounds property actually uses
	static private Bounds _camBounds; 

	//Method to actually set camera boundaries, used by camBounds 
	//Important to note this will only work with orthographic camera with rotation 0,0,0
	public static void SetCameraBounds(Camera cam=null) { // you can specify what camera you want to use

		// If no Camera was passed in then we will default to setting the main camera 
		if (cam == null) 
			cam = Camera.main; 
		
		//Create to vector3 points 
		Vector3 topLeft = new Vector3( 0, 0, 0 );
		Vector3 bottomRight = new Vector3( Screen.width, Screen.height, 0 ); //Bottom of screen is the maximum

		// Screen width and height are in pixels but we must convert those tot he stranded world unit
		Vector3 boundTLN = cam.ScreenToWorldPoint( topLeft );
		Vector3 boundBRF = cam.ScreenToWorldPoint( bottomRight );

		//The near and far clip plane determine the closest and farthest render distance of camera
		// Adjust the z component of TLN and BRF so we span the entire clip plane 
		boundTLN.z += cam.nearClipPlane;
		boundBRF.z += cam.farClipPlane;

		//Find the center of the Bounds
		Vector3 center = (boundTLN + boundBRF)/2f; //calculation
		_camBounds = new Bounds( center, Vector3.zero ); //center the new bound

		//Expand _camBounds to encapsulate the edges 
		_camBounds.Encapsulate( boundTLN );
		_camBounds.Encapsulate( boundBRF );
	}

	// Test camBounds against defined bounds using specified "test" enum 
	public static Vector3 ScreenBoundsCheck(Bounds bnd, BoundsTest test = BoundsTest.center) {
		return( BoundsInBoundsCheck( camBounds, bnd, test ) ); 
	}

	// Checks to see whether the smaller bounds LilB are within the Bigger boudnsB
	public static Vector3 BoundsInBoundsCheck( Bounds bigB, Bounds lilB, BoundsTest test = BoundsTest.onScreen ) {

		//Based on the test selected the behavior of this method will change 

		// Get center of lilB
		Vector3 pos = lilB.center;

		// Initialize the offset at [0,0,0]
		Vector3 off = Vector3.zero;

		switch (test) {

		// The center test determines what offset would have to be apple to lilB to move its center back inside bigB
		case BoundsTest.center:
			if ( bigB.Contains( pos ) ) {
				return( Vector3.zero );
			}
			if (pos.x > bigB.max.x) {
				off.x = pos.x - bigB.max.x;
			} else if (pos.x < bigB.min.x) {
				off.x = pos.x - bigB.min.x;
			}
			if (pos.y > bigB.max.y) {
				off.y = pos.y - bigB.max.y;
			} else if (pos.y < bigB.min.y) {
				off.y = pos.y - bigB.min.y;
			}
			if (pos.z > bigB.max.z) {
				off.z = pos.z - bigB.max.z;
			} else if (pos.z < bigB.min.z) {
				off.z = pos.z - bigB.min.z;
			}
			return( off );

		// The onScreen test determines what offset would have to be applied to keep all of lilB inside bigB
		case BoundsTest.onScreen:
			if ( bigB.Contains( lilB.min ) && bigB.Contains( lilB.max ) ) {
				return( Vector3.zero );
			}
			if (lilB.max.x > bigB.max.x) {
				off.x = lilB.max.x - bigB.max.x;
			} else if (lilB.min.x < bigB.min.x) {
				off.x = lilB.min.x - bigB.min.x;
			}
			if (lilB.max.y > bigB.max.y) {
				off.y = lilB.max.y - bigB.max.y;
			} else if (lilB.min.y < bigB.min.y) {
				off.y = lilB.min.y - bigB.min.y;
			}
			if (lilB.max.z > bigB.max.z) {
				off.z = lilB.max.z - bigB.max.z;
			} else if (lilB.min.z < bigB.min.z) {
				off.z = lilB.min.z - bigB.min.z;
			}
			return( off );
		
		// The offScreen test determines what off would need to be applied to move any tiny part of lilB inside of bigB
		case BoundsTest.offScreen:
			bool cMin = bigB.Contains( lilB.min );
			bool cMax = bigB.Contains( lilB.max );
			if ( cMin || cMax ) {
				return( Vector3.zero );
			}
			if (lilB.min.x > bigB.max.x) {
				off.x = lilB.min.x - bigB.max.x;
			} else if (lilB.max.x < bigB.min.x) {
				off.x = lilB.max.x - bigB.min.x;
			}
			if (lilB.min.y > bigB.max.y) {
				off.y = lilB.min.y - bigB.max.y;
			} else if (lilB.max.y < bigB.min.y) {
				off.y = lilB.max.y - bigB.min.y;
			}
			if (lilB.min.z > bigB.max.z) {
				off.z = lilB.min.z - bigB.max.z;
			} else if (lilB.max.z < bigB.min.z) {
				off.z = lilB.max.z - bigB.min.z;
			}
			return( off );
		}
		return( Vector3.zero );
	}

	// This function will iteratively climb up the transform.parent tree until it either finds a parent with a tag != "Untagged" or no parent
	public static GameObject FindTaggedParent(GameObject go) { 

		// If this gameObject has a tag
		if (go.tag != "Untagged") { 
			// then return this gameObject
			return(go);
		}

		// If there is no parent
		if (go.transform.parent == null) { 
			// We've reached the top of the hierarchy with no interesting tag
			// So return null
			return( null );
		}

		// then we must recursively climb up the tree
		return( FindTaggedParent( go.transform.parent.gameObject ) ); 
	}
	// This version of the function handles things if a Transform is passed in
	public static GameObject FindTaggedParent(Transform t) { 
		return( FindTaggedParent( t.gameObject ) );
	}



	// Returns a list of all Materials on this GameObject or its children
	static public Material[] GetAllMaterials( GameObject go ) {
		List<Material> mats = new List<Material>();
		if (go.GetComponent<Renderer>() != null) {
			mats.Add(go.GetComponent<Renderer>().material);
		}
		foreach( Transform t in go.transform ) {
			mats.AddRange( GetAllMaterials( t.gameObject ) );
		}
		return( mats.ToArray() );
	}
}
