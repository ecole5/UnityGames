using UnityEngine;
using System.Collections;

public class Basket : MonoBehaviour
{

    public GUIText scoreGT;
    public static int score;

    // Update is called once per frame
    void Update()
    {

        // Get mouse position on screen and convert to 3d
        Vector3 mousePos2D = Input.mousePosition;
        mousePos2D.z = -Camera.main.transform.position.z;
        Vector3 mousePos3D = Camera.main.ScreenToWorldPoint(mousePos2D);

        // Move the x position of basket to x potion of mouse
        Vector3 pos = this.transform.position;
        pos.x = mousePos3D.x;
        this.transform.position = pos;

    }

    void Start()
    {
        //Get a reference to the score counter
        GameObject scoreGO = GameObject.Find("ScoreCounter");
        scoreGT = scoreGO.GetComponent<GUIText>(); //get the guiTExt part
        score = 0;
        scoreGT.text = "0"; //set initial score to zero
    }

    void OnCollisionEnter(Collision coll)
    {


        GameObject collidedWith = coll.gameObject;
        if (collidedWith.tag == "Apple")
        { //when collision with apple                              
            Destroy(collidedWith); //destroy the apple
        }

        //Update Score                  
        score += 100; //add the score
        scoreGT.text = score.ToString(); //change back to string
        if (score > HighScore.score)
        { //update high score 
            HighScore.score = score;
        }
    }


}
