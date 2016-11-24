using UnityEngine;
using UnityEngine.UI;

//Configuration for each level 

public class LevelDesigner : MonoBehaviour
{

	//References to all the GUI elements
	public Toggle[] Toggles;
	public Text errorText;
	public InputField enemyNumber, maxPoints;
	private int activeLevel = 0; //current:LEel


	void setValues()
	{
		//Set all the GUI elements to the state defined in preferences 
		for (int i = 0; i < 5; i++) { //set toggles on/off
			Toggles [i].isOn = GameData.Prefs.space.activeEnemies [activeLevel,i];
		}
		//set number of enemies and max points per level
		enemyNumber.text = GameData.Prefs.space.enemyNumbers [activeLevel].ToString ();
		maxPoints.text = GameData.Prefs.space.levelPoints [activeLevel].ToString ();
	}


	void Awake ()
	{
		setValues ();

	}

	public void changeLevel(int level)
	{
		activeLevel = level;
		setValues();
	}

	//When turning off error message
	void disable ()
	{
		errorText.text = "";
	}

	//Display enemy invalid error
	void showError()
	{
		errorText.text = "Error: Invalid Enemy Selection";
		Invoke ("disable", 3f); //turn off error in three seconds 
		enemyNumber.text = GameData.Prefs.space.enemyNumbers [activeLevel].ToString (); //change the text back to the original value

	}

	//When changing the number of enemies field
	public void setNumberEnemies (string temp)
	{

		int temp1 = int.Parse (temp);

		switch (activeLevel) { //perform different check for each level
		case 0:
			if (temp1 < GameData.Prefs.space.enemyNumbers [1]) { //must be less than silver number of enemies
				GameData.Prefs.space.enemyNumbers [activeLevel] = temp1;
			} else { 
				showError ();
			}
			break;
		case 1:
			if (temp1 > GameData.Prefs.space.enemyNumbers [0] && temp1 < GameData.Prefs.space.enemyNumbers [2]) { //must be in-between number of bronze and gold enemies
				GameData.Prefs.space.enemyNumbers [activeLevel] = temp1;
			} else {//if not display error
				showError ();
			}
			break;
		default:
			if (temp1 > GameData.Prefs.space.enemyNumbers [1]) { //must be greater then silver enemies
				GameData.Prefs.space.enemyNumbers [activeLevel] = temp1;
			} else {//if not display error
				showError ();
			}
			break;

			
		}

	}

    //When toggles are changed
    public void toggleElement(Toggle temp)
    {
		GameData.Prefs.space.activeEnemies [activeLevel, identifyEnemy (temp)] = temp.isOn;
	}

	//Set points needed advance to next level
	public void setMaxPoint (string temp){
		GameData.Prefs.space.levelPoints [activeLevel] = int.Parse (temp);
	}

	//Identify toggle by enemy tag, to set correct one
	public int identifyEnemy (Toggle temp){
		switch (temp.tag) {
		case("Enemy1"):
			return 0;
		case("Enemy2"):
			return 1;
		case("Enemy3"):
			return 2;
		case("Enemy4"):
			return 3;
		default:
			return 4;

		}
	}
}
