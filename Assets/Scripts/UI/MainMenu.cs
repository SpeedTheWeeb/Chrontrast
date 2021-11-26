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

    private void Start()
    {
        bgmLoop = RuntimeManager.CreateInstance("event:/bgm/menu");
        bgmLoop.start();
        bgmLoop.getPlaybackState(out playbackState);
    }

    public void PlayGame ()
    {
        if(playbackState != PLAYBACK_STATE.STOPPED)
            bgmLoop.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
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
