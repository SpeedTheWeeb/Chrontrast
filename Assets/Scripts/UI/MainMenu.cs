using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    private void Start()
    {
        // FMOD.Studio.Pl bgmLoop = FMODUnity.RuntimeManager.CreateInstance("event:/bgm/menu");

    }

    public void PlayGame ()
    {
        // bgmloop.release();
        SceneManager.LoadScene("Game Scene");
    }
    public void PlayTutorial ()
    {
        Debug.Log ("TUTORIAL");
        //SceneManager.LoadScene("Tutorial Scene");
    }

    public void QuitGame () 
    {
        Debug.Log ("QUIT");
        Application.Quit();
    }
}
