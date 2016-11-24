using UnityEngine;
using UnityEngine.SceneManagement;

public class Apple_Buttons : MonoBehaviour
{

    //Apple picker menu buttons 

    public void PlayButton()
    {
        SceneManager.LoadScene("Apple_MainGame"); //loads apple main game

    }

    public void ExitButton()
    {
        PortalAudio.output.playMusic();
        SceneManager.LoadScene("PortalHome"); //goes back to portal home
    }

    public void SplashButton()
    {
        SceneManager.LoadScene("Apple_Splash"); //loads apple splash screen
    }

    public void HistoryButton()
    {
        SceneManager.LoadScene("Apple_History"); //loads apple high score screen
    }


}
