using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic; //allows for the use of the List

public class ApplePicker : MonoBehaviour
{

    public GameObject basketPrefab;

    public int numberOfBaskets = 3; //number of lives

    public float basketBottomY = -14f; //position of bottom basket

    public float basketSpacingY = 2f; //spacing between baskets

    public List<GameObject> basketList; //list to hold baskets 

    void Start()
    {

        basketList = new List<GameObject>(); //creates the basket list

        for (int i = 0; i < numberOfBaskets; i++)
        { //generates stack of three baskets on screen
            GameObject tBasketGO = Instantiate(basketPrefab) as GameObject;
            Vector3 pos = Vector3.zero;
            pos.y = basketBottomY + (basketSpacingY * i);
            tBasketGO.transform.position = pos;
            basketList.Add(tBasketGO);
        }
    }
    public void AppleDestroyed()
    {

        //// Destroy all of the falling Apples when one basket has been lost 
        GameObject[] tAppleArray = GameObject.FindGameObjectsWithTag("Apple");
        foreach (GameObject tGO in tAppleArray)
        {
            Destroy(tGO);
        }

        // Get the index of the last Basket in basketList and change it so that next time it only generates two baskets
        int basketIndex = basketList.Count - 1;

        // Get a reference to the basket being removes
        GameObject tBasketGO = basketList[basketIndex];

        // Remove the Basket from the List and destroy the dead basket
        basketList.RemoveAt(basketIndex);
        Destroy(tBasketGO);

        // Restart the game if no baskets are remaining
        if (basketList.Count == 0)
        {
            HistoryMethods.addByScore(HistoryMethods.regLog(Basket.score.ToString()), GameData.Prefs.appleHist); //save apple history to server
            SceneManager.LoadScene("Apple_Splash"); //load  splash screen
        }
    }
}
