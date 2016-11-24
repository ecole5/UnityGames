using UnityEngine;

public class AppleTree : MonoBehaviour
{

    //Start By Defining The Properties
    public GameObject applePrefab;

    //Speed at which apple tree moves (m/s)
    public float speed = 1f;

    //Left and right boundary of tree movement
    public float gameBoundry = 10f;

    //Chance the unit will change directions
    public float directionChangeProb = 0.1f;

    //Time delay of apple drop
    public float dropDelay = 1f;


    void Start()
    {
        //starts doping the apple at the rate of drop delay
        InvokeRepeating("DropApple", 2f, dropDelay);
    }

    void DropApple()
    {
        //actually created apple object and sets its position
        GameObject apple = Instantiate(applePrefab) as GameObject;
        apple.transform.position = transform.position;
    }

    // Update is called once per frame
    void Update()
    {

        //Basic Movement
        Vector3 pos = transform.position; //use transform to get 
        pos.x += speed * Time.deltaTime; //number of seconds since last frame (makes game time based)
        transform.position = pos;  //use transform to set 


        //Three Movement Change Possibilities
        if (pos.x < -gameBoundry)
        {
            speed = Mathf.Abs(speed); //move right on left boundary case
        }
        else if (pos.x > gameBoundry)
        {
            speed = -Mathf.Abs(speed); //move left on right boundary case
        }
        else if (Random.value < directionChangeProb)
        { //if not at boundary probability based switch 
            speed *= -1; //change direction 
        }

    }
}
