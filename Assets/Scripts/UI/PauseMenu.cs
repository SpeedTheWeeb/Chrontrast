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

    PLAYBACK_STATE goPBS;
    PLAYBACK_STATE mainPBS;
    PLAYBACK_STATE finalePBS;

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
        StopMusic();
        SceneManager.LoadScene("Game Scene");
        Time.timeScale = 1f;
    }

    public void LoadMainMenu()
    {
        StopMusic();
        SceneManager.LoadScene("main menu");
        Time.timeScale = 1f;
    }

    public void QuitGame()
    {
        StopMusic();
        Debug.Log ("QUIT");
        Application.Quit();
    }

    void StopMusic()
    {
        EventInstance bgmMain = FindObjectOfType<SpawningScript>().bgmMain;
        bgmMain.getPlaybackState(out mainPBS);
        EventInstance bgmFinale = FindObjectOfType<SpawningScript>().bgmFinale;
        bgmFinale.getPlaybackState(out finalePBS);
        EventInstance gameOverLoop = FindObjectOfType<CrystalHP>().gameOverLoop;
        gameOverLoop.getPlaybackState(out goPBS);

        if (mainPBS == PLAYBACK_STATE.PLAYING || finalePBS == PLAYBACK_STATE.PLAYING || goPBS == PLAYBACK_STATE.PLAYING)
        {
            bgmMain.release();
            bgmMain.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
            bgmFinale.release();
            bgmFinale.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
            gameOverLoop.release();
            gameOverLoop.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
        }
    }
}