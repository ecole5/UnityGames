using UnityEngine;

public class Apple : MonoBehaviour
{

    public static float bottomY = -20f; //safe off screen distance for apple

    void Update()
    {
        if (transform.position.y < bottomY)
        {
            Destroy(gameObject); //destroys apple
            ApplePicker apScript = Camera.main.GetComponent<ApplePicker>(); //Notify Apple picker of a missed apple
            apScript.AppleDestroyed();
        }

    }
}
