using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using FMODUnity;
using FMOD.Studio;

public class PauseMenu : MonoBehaviour
{

    public static bool GamePaused = false;

    public GameObject pauseMenuUI;

    EventInstance goLoop;
    PLAYBACK_STATE playbackState;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && Gamemanager.GameEnd == false) 
        {
            if (GamePaused) 
            {
                Resume();
            } else 
            {
                Pause();
            }
        }
    }

    public void Resume () 
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        GamePaused = false;
    }

    void Pause ()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        GamePaused = true;
    }

    public void Restart()
    {
        if (playbackState != PLAYBACK_STATE.STOPPED)
            goLoop.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
        SceneManager.LoadScene("Game Scene");
        Time.timeScale = 1f;
    }

    public void LoadMainMenu()
    {
        if (playbackState != PLAYBACK_STATE.STOPPED)
            goLoop.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
        SceneManager.LoadScene("main menu");
        Time.timeScale = 1f;
    }

    public void QuitGame()
    {
        if (playbackState != PLAYBACK_STATE.STOPPED)
            goLoop.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
        Debug.Log ("QUIT");
        Application.Quit();
    }

}
