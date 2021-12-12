using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using FMOD.Studio;
using FMODUnity;

public class MainMenu : MonoBehaviour
{
    EventInstance bgmLoop;
    PLAYBACK_STATE playbackState;
    
    public void PlayGame ()
    {
        bgmLoop = FindObjectOfType<MenuBGM>().bgmLoop;
        if (playbackState != PLAYBACK_STATE.STOPPED)
            bgmLoop.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
        RuntimeManager.PlayOneShot("event:/sfx/menu/start");
        SceneManager.LoadScene("Game Scene");
    }
    public void PlayTutorial ()
    {
        bgmLoop = FindObjectOfType<MenuBGM>().bgmLoop;
        if (playbackState != PLAYBACK_STATE.STOPPED)
            bgmLoop.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
        RuntimeManager.PlayOneShot("event:/sfx/menu/start");
        SceneManager.LoadScene("Tutorial Scene");
    }

    public void QuitGame () 
    {
        Debug.Log ("QUIT");
        Application.Quit();
    }
}
