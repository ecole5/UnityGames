[System.Serializable]
public class SpacePrefs {
   
	public int[] enemyColor; //colors for enemy(index represents enemy type with 0 being enemy0)
    public string[] enemyPoints; //points for killing enemy (index represents enemy type with 0 being enemy0)
    public int[] levelPoints; //points need to advance to next level (index represents level, with bronze being zero)
    public float winVolume, backgroundVolume, shootVolume, destroyVolume; //volume settings for each sound
    public int backgroundChoice, shootChoice, destroyChoice; //the index of the related audio clip arrays which represents the currently selected track 
	public int[] winChoice; //the index of the related audio clip array where index 0 is winChoice for bronze, and index 1 is for silver...
    public bool[,] activeEnemies; //enemies chosen for each level. Index 0 represents enemy0
    public int[] enemyNumbers; //number of enemies for each level, index 0 represents bronze...
    public int ChosenBackground; //index of the backgroundImages array which represents chosen background

   public SpacePrefs()
    { 
		//Set the defaults for new users
		enemyColor = new int [5]{0,1,2,0,1}; 
		enemyPoints = new string [5]{"1","2","3","4","5"}; 
		levelPoints = new int [3]{5,20,30}; 
		winVolume = 100f;
		backgroundVolume = 100f;
		shootVolume = 100f;
		destroyVolume = 100f;
		backgroundChoice = 0;
		shootChoice = 0;
		destroyChoice = 0;
		winChoice = new int[3] {0,0,0};
		activeEnemies = new bool[3, 5] {
			{ true, true, false, false, false },
			{ false, true, true, true, false },
			{false,false,true,true,true}
		};
		enemyNumbers = new int[3] {10,20,30};
		ChosenBackground = 0;
      
    }


}
