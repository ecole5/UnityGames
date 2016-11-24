using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Dino_MainGame : MonoBehaviour {

	//UI References
	public Text clock;
	public Text scoreText;
	public Text cardsLeftText;

	//Values to track
	public int score = 1000; 
	public int cardsLeft = 12; 
	public float time = 0;

	public Card [] cardList; //reference to all the cards
	public static Dino_MainGame gameStats; //persistent reference to this object
	Card firstCard = null; //firstCard in choose sequence
	Card secondCard = null; //secondCard to be compared to first in choose sequence
	public bool cardsActive = true; //this will allow the win/loses sequence to commence before letting play click on next card
	bool gameEnded = false; //state when game ended is true so cards cannot be pressed
	bool count = true; //controls clock action

	public void Awake() {
		gameStats = this; //singleton pattern
		DontDestroyOnLoad(gameObject); //keep this object alive for endGame
		shuffle (); //shuffle the cards 
	}

	void shuffle()
	{	
		int[] unShuffled = new int[12]{0,0,1,1,2,2,3,3,4,4,5,5};
		System.Random rnd = new System.Random ();

		for (int i = 12; i > 1; i--) { //simple fisher Yates shuffle )
			int j = rnd.Next (i); //find next element to swap 
			//Swap
			int temp = unShuffled [j];
			unShuffled[j] = unShuffled[i-1];
			unShuffled [i -1] = temp;	
		}

		for (int i = 0; i < 12; i++) { //update the values of the cards
			cardList [i].setValue (unShuffled [i]); 
		}
	}

	void loadEnd() { 
		SceneManager.LoadScene ("Dino_GameOver"); //load the gameOver screen
	}


	void playWin()
	{
		AudioController.speaker.playClip (1); //play short win sound
	}

	void playLose()
	{
		AudioController.speaker.playClip (2); //play short lose sound
	}

	void yesMatch() {


		//Show the old graphics 
		secondCard.hide ();
		firstCard.hide ();

		//Reset the active cards 
		firstCard = null; 
		secondCard = null;

		//Update cards left text after delay
		cardsLeftText.text = "Cards Left " + cardsLeft;

		cardsActive = true;
	}

	void noMatch() {
		
		//Flip both cards back to front graphic 
		secondCard.flipFront ();
		firstCard.flipFront ();

		//Reset the active cards
		firstCard = null; 
		secondCard = null;
		scoreText.text = "Score " + score;

		cardsActive = true;
	}
		

	//Game Logic
	public void cardClicked(int index)
	{ 

		if (firstCard == null && cardsActive && !gameEnded){ //no cards assigned
			AudioController.speaker.playClip (0); //play click
			firstCard = cardList [index]; //keep reference to first card
			firstCard.flipBack (); //reveals the back side of the card
		}

		else if (secondCard == null && cardList[index] != firstCard && cardsActive && !gameEnded){ //first card assigned but second not, and card clicked is not first

			AudioController.speaker.playClip (0); //play click

			secondCard = cardList [index]; //keep reference to second card
			secondCard.flipBack();  //show the second card 

			if (firstCard.getValue () == secondCard.getValue()) { //there is a match with firstCard
				cardsActive = false;
				cardsLeft -= 2; //Adjust cards left 
				Invoke ("playWin", 0.3f); //give enough time to click
				Invoke ("yesMatch", 0.6f);//resets the cards
			}

			else { //there is not a match with firstCard
				cardsActive = false;
				score -= 40; //decrease score
				Invoke ("playLose", 0.3f);
				Invoke ("noMatch", 0.6f);
			}
				
			//GameOver Check
			if (cardsLeft == 0 || score == 0) { 
				gameEnded = true;
				count = false; //stop = false
				HistoryMethods.addByScore (HistoryMethods.regLog (score.ToString ()), GameData.Prefs.dinoHist); //save history to server
				Invoke ("loadEnd", 2f); //invoke the game over screen in 2 seconds
			}

		} //end card match check 

	} //end card clicked 


	void Update () {
		if (count) {
			clock.text = "Time " + ((int)time).ToString () + "  ";
			time += Time.deltaTime;
		}
	}
		
} //end script