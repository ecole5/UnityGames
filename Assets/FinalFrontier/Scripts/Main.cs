using UnityEngine;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

//******All stage 2 mods are indicated with NEW.*****//

public class Main : MonoBehaviour
{
	//Some Enemy Stuff
	static public Main S;
	static public Dictionary<WeaponType, WeaponDefinition> W_DEFS;
    public List<GameObject> prefabEnemies; //list of enemies
	public float enemySpawnPerSecond; //Enemies/second

	//NEW Data-----------------------------------------------------------------------------------------------------
	public GameObject[] allEnemies; //list of all enemies to be used to build custom prefab enemies at each level
	public int[] killList; //record of enemies killed
	public AudioSource destroyBox;//audio source for destroy sounds
	public AudioSource GameMusic; //Audio source for game music
	public AudioSource winBox; //Audio source for win sounds
	public AudioSource shootBox; //Audio source for shoot sound
	bool aliveCheck = false; //starts checking to see when all the remaining enemies have went off screen when set true
	public int gameLevel = 0; //shows the current level of game
	public int points; //current game points
	public float gameTime = 0f; //the time played in the game
	private int enemyCount;//number of enemies spawned in the current level so far
	//EndNew-----------------------------------------------------------------------------------------------------------------------

	public float enemySpawnPadding = 1.5f;
// Padding for position
	public WeaponDefinition[] weaponDefinitions;
	public GameObject prefabPowerUp;
	public WeaponType[] powerUpFrequency = new WeaponType[] {
		WeaponType.blaster, WeaponType.blaster,
		WeaponType.spread,
		WeaponType.shield
	};

	public bool ________________;
	public WeaponType[] activeWeaponTypes;
	public float enemySpawnRate;
	//Spawn delay (calculated at runtime)

	//NEW-----------------------------------------------------------------------------------
	public void selectEnemies (int level) //sets the bronze stage 
	{
		
		//Places all the selected enemies for this level into the Prefab enemies list
		prefabEnemies = new List<GameObject>();
		for (int i = 0; i < 5; i++) {
			if (GameData.Prefs.space.activeEnemies[level,i] == true) {
				prefabEnemies.Add(allEnemies [i]);
			}
		}
	}


	//EndNEW---------------------------------------------------------------------------------------------------------------------------------

	public void ShipDestroyed (Enemy e)
	{

		//NEW---------------------------------------------------------------------------
		//Play desired ship destroyed sound 
		destroyBox.clip = MissionControl.huston.destroySounds [GameData.Prefs.space.destroyChoice];
		destroyBox.Play ();

		//adjust game points based on enemy type, and update kill list
		switch (e.tag) {
		case ("Enemy1"):
			points += int.Parse (GameData.Prefs.space.enemyPoints [0]);
			++killList [0];
			break;

		case ("Enemy2"):
			++killList [1];
			points += int.Parse (GameData.Prefs.space.enemyPoints [1]);
			break;

		case ("Enemy3"):
			++killList [2];
			points += int.Parse (GameData.Prefs.space.enemyPoints [2]);
			break;
		case ("Enemy4"):
			++killList [3];
			points += int.Parse (GameData.Prefs.space.enemyPoints [3]);
			break;
		default:
			++killList [4];
			points += int.Parse (GameData.Prefs.space.enemyPoints [4]);
			break;
		}

		//Mo niter for level changes
		if (GameData.Prefs.space.levelPoints [gameLevel] <= points) {
			if (gameLevel == 0) { //switch to silver
				winBox.clip = MissionControl.huston.winSounds[GameData.Prefs.space.winChoice[0]];
                winBox.Play();
                selectEnemies(1);
                gameLevel++;
			} else if (gameLevel == 1) { //switch to gold
				winBox.clip = MissionControl.huston.winSounds[GameData.Prefs.space.winChoice[1]];
                winBox.Play();
                selectEnemies(2);
                gameLevel++;
			} else { //end game
				//Play the chosen end game sound 
				winBox.clip = MissionControl.huston.winSounds [GameData.Prefs.space.winChoice [2]]; 
				winBox.Play ();
				//The game over screen will display you win
				MissionControl.huston.causeOfDeath = "You Win!";
				//Launch GameOver Screen
				gameOver ();
			}
		}
		//End NEW-----------------------------------------------------------------------

		// Potentially generate a PowerUp
		if (Random.value <= e.powerUpDropChance) {
			// Random.value generates a value between 0 & 1 exclusive
			// If the e.powerUpDropChance is 0.50f, a PowerUp will be generated 50% of time
			int ndx = Random.Range (0, powerUpFrequency.Length); //choses power up
			WeaponType puType = powerUpFrequency [ndx];
			// Spawn a PowerUp
			GameObject go = Instantiate (prefabPowerUp) as GameObject;
			PowerUp pu = go.GetComponent<PowerUp> ();
			//set proper weapon type
			pu.SetType (puType);
			//Set power up to position of destroyed ship
			pu.transform.position = e.transform.position;
		}
	}

	void Awake ()
	{
		//NEW--------------------------------------------------------------------------
		//Configure volume for all sound Boxes
		GameMusic.volume = GameData.Prefs.space.backgroundVolume;
		shootBox.volume = GameData.Prefs.space.shootVolume;
		winBox.volume = GameData.Prefs.space.winVolume;
		destroyBox.volume = GameData.Prefs.space.destroyVolume;

		//Start background music
		GameMusic.clip = MissionControl.huston.backgroundSongs [GameData.Prefs.space.backgroundChoice];
		GameMusic.Play ();

        //configure bronze
        selectEnemies(0);


		//EndNew---------------------------------------------------------------------------

		S = this;

		// Set Utils.camBounds
		Utils.SetCameraBounds (this.GetComponent<Camera> ());

		//convert desired enemies per second into spawn rate (0.5 enemy/sec -> 2 second delay)
		enemySpawnRate = 1f / enemySpawnPerSecond;

		// Invoke spawn enemy after delay but only once because its invoke not InvokeRepeating!!!
		enemyCount++; //NEW, make sure to count the first enemy spawned
		Invoke ("SpawnEnemy", enemySpawnRate);

		//Dic with WeaponType as the key
		W_DEFS = new Dictionary<WeaponType, WeaponDefinition> ();
		foreach (WeaponDefinition def in weaponDefinitions) {
			W_DEFS [def.type] = def;
		}
	}


	static public WeaponDefinition GetWeaponDefinition (WeaponType wt)
	{
		// Check to make sure that the key exists in the Dictionary
		if (W_DEFS.ContainsKey (wt)) {
			return(W_DEFS [wt]);

		}
		return(new WeaponDefinition ()); //upon failure
	}

	void Start ()
	{

		//Set Utils.camBounds (fixes error where bounds are off when screen is maximized)
		Utils.SetCameraBounds (this.GetComponent<Camera> ());

		activeWeaponTypes = new WeaponType[weaponDefinitions.Length];
		for (int i = 0; i < weaponDefinitions.Length; i++) {
			activeWeaponTypes [i] = weaponDefinitions [i].type;
		}
	}

	public void SpawnEnemy ()
	{


		if (enemyCount <= GameData.Prefs.space.enemyNumbers [gameLevel]) { //NEW, only create enemies until the target number of enemies has been reached
			// Pick a random enemy prefab to instantiate from the list
			int ndx = Random.Range (0, prefabEnemies.Count ); //random index
			GameObject go = Instantiate (prefabEnemies [ndx]) as GameObject; //instantiate index at that prefab

			// Position x random and y set of enemy
			Vector3 pos = Vector3.zero;
			float xMin = Utils.camBounds.min.x + enemySpawnPadding;
			float xMax = Utils.camBounds.max.x - enemySpawnPadding;
			pos.x = Random.Range (xMin, xMax); //x is random
			pos.y = Utils.camBounds.max.y + enemySpawnPadding; //y is always the same based of padding
			go.transform.position = pos;
			enemyCount++; ///NEW add to the number of enemies spawned at this level 
		} else { //NEW once all enemies have been spawned start checking if they are all are destroyed
			aliveCheck = true;
		}
			
		//Instead of using InvokeRepeating we use a recursive function
		Invoke ("SpawnEnemy", enemySpawnRate);
	}

	//NEW, now instead of restarting when the player dies or wins or does not reach the point target they will go to a game over screen
	public void DelayedGameOver (float delay)
	{ //called from player once shield is gone
		Invoke ("gameOver", delay); //allows us to use the delay
	}

	//NEW loads the new game over screen
	void gameOver ()
	{
		string temp;
        //Convert game level to string to record in the history
		switch (gameLevel) {
		case 0: 
			temp = "Bronze";
			break;
		case 1:
			temp = "Silver";
			break;
		default:
			temp = "Gold";
			break;
				
		}
		HistoryMethods.addByScore (HistoryMethods.spaceLog (points.ToString(), temp), GameData.Prefs.spaceHist); //save history to server
		SceneManager.LoadScene ("Scene_SpaceGameOver");
	}

	//NEW. If the player does not meet the
	void Update ()
	{
		gameTime += Time.deltaTime;//NEW update the time that the current game has run for
		if (aliveCheck) {  //when all the enemies have been spawned for that level we start checking if they are still active 
			int temp = 0;
			temp += GameObject.FindGameObjectsWithTag ("Enemy1").Length;
			temp += GameObject.FindGameObjectsWithTag ("Enemy2").Length;
			temp += GameObject.FindGameObjectsWithTag ("Enemy3").Length;
			temp += GameObject.FindGameObjectsWithTag ("Enemy4").Length;
			temp += GameObject.FindGameObjectsWithTag ("Enemy5").Length;
			if (temp == 0) { //if no enemies exists and the stage has not advanced it is possible that player has missed the point target 
				if (points < GameData.Prefs.space.levelPoints [gameLevel]) {
					MissionControl.huston.causeOfDeath = "Missed Point Target";
					gameOver ();
				} else { //marginal case where player advanced by killing the last possible enemy 
					aliveCheck = false;
				}
			}
		}
	}


}