using UnityEngine;
using UnityEngine.UI;

public class ScrollingBanner : MonoBehaviour {


	Rect screenRect;
	Rect textRect;
	Vector3 startPos;
	Vector3 movingPos;
	public string [] textList; 
	public Text bannerText;
	bool leftOnScreen = false;

	void Awake(){
		foreach (string word in textList){
			bannerText.text += "          " + word;
		}

	

	}
	void Start(){
		startPos = transform.position;
		movingPos = startPos;
		screenRect = new Rect(0f,0f, Screen.width, Screen.height);

	
	}
		

	void Update(){
		Vector3 [] objectCorners = new Vector3 [4];
		GetComponent<RectTransform> ().GetWorldCorners (objectCorners);

		if (screenRect.Contains(objectCorners [0])){
				leftOnScreen = true;
			}

		if (screenRect.Contains (objectCorners [0]) || !leftOnScreen) { //0 is left top edge, 2 is right top edge
			movingPos.x += 50f * Time.deltaTime;
			transform.position = movingPos;
		} 

		else {
			transform.position = startPos;
			movingPos = startPos;
			leftOnScreen = false;


		}

	}

}
