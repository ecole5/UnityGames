using UnityEngine;
using UnityEngine.UI;


//Class drives the behavior of the enemy configuration screen
public class EnemyPref : MonoBehaviour {


	//Get references to UI input fields and drop downs
	public Dropdown enemySelect;
	public Dropdown colorSelect;
	public InputField currentPoint;
	public int activeEnemy = 0;

	void Awake(){
		//Set fields and text to currently selected values from preferences
		currentPoint.text =  GameData.Prefs.space.enemyPoints [activeEnemy];
		colorSelect.value = GameData.Prefs.space.enemyColor [activeEnemy];
	}

	public void setPoints(string cool){
		//update points gained from killing a specific enemy in preferences
		GameData.Prefs.space.enemyPoints [activeEnemy] = cool;
	}

	public void selectEnemy (){
		//Selects which enemy is going to be modified
		activeEnemy = enemySelect.value;
		//Shows color and point value for that enemy at the moment
		currentPoint.text =  GameData.Prefs.space.enemyPoints [activeEnemy];
		colorSelect.value = GameData.Prefs.space.enemyColor [activeEnemy];
	}

	public void selectColor (){
		//update color for specific enemy in preferences
		GameData.Prefs.space.enemyColor [activeEnemy] = colorSelect.value;
	} 

}
