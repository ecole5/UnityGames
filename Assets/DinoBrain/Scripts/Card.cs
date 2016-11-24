using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Card : MonoBehaviour
{

    public Sprite[] picList; //references all the sprites for the card backs and the empty card front
    public int value = 0;
    Image card; //reference to the image game object

    void Awake()
    {
        card = gameObject.GetComponent<Image>(); //set the reference to the image 
    }

    public void flipBack()
    {
        card.sprite = picList[value]; //show the back of the card at the card value
    }

    public void flipFront()
    {
        card.sprite = picList[7]; //remove the sprite to reveal the front material 
    }

    public void setValue(int value)
    {
        this.value = value; //set the value of the card
    }

    public int getValue()
    {
        return value; //return the value of the card
    }

    public void hide()
    {
        card.sprite = picList[6]; //set blank sprite
        Destroy(GetComponent<EventTrigger>()); //detach event trigger
    }

}
