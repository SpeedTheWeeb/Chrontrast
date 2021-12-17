using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using FMODUnity;
using FMOD.Studio;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    public Text CrysHP;
    public Text Wave;
    public Text P1ATS;
    public Text P2ATS;
    public Text P1DMG;
    public Text P2DMG;
    public static bool GamePaused = false;

    public GameObject pauseMenuUI;
    public GameObject InfoMenuUI;


    PLAYBACK_STATE victoryPBS;
    PLAYBACK_STATE goPBS;
    PLAYBACK_STATE mainPBS;
    PLAYBACK_STATE finalePBS;
    PLAYBACK_STATE tutPBS;

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

        if(SceneManager.GetActiveScene().name == "Tutorial Scene")
        {
            if (Input.GetKey(KeyCode.Space))
                InfoMenuUI.SetActive(true);
            else
                InfoMenuUI.SetActive(false);
        }
    }

    public void Resume () 
    {
        CrysHP.enabled = true;
        Wave.enabled = true;
        P1ATS.enabled = true;
        P1DMG.enabled = true;
        P2ATS.enabled = true;
        P2DMG.enabled = true;
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        GamePaused = false;
    }

    void Pause ()
    {
        pauseMenuUI.SetActive(true);
        CrysHP.enabled = false;
        Wave.enabled = false;
        P1ATS.enabled = false;
        P1DMG.enabled = false;
        P2ATS.enabled = false;
        P2DMG.enabled = false;
        Time.timeScale = 0f;
        GamePaused = true;
    }

    public void Restart()
    {
        SceneManager.LoadScene("Game Scene");
        StopMusic();
        Time.timeScale = 1f;
    }

    public void LoadMainMenu()
    {
        SceneManager.LoadScene("main menu");
        StopMusic();
        Time.timeScale = 1f;
    }

    public void QuitGame()
    {
        Debug.Log ("QUIT");
        StopMusic();
        Application.Quit();
    }

    public void StopMusic()
    {
        EventInstance bgmMain = FindObjectOfType<SpawningScript>().bgmMain;
        bgmMain.getPlaybackState(out mainPBS);

        EventInstance bgmFinale = FindObjectOfType<SpawningScript>().bgmFinale;
        bgmFinale.getPlaybackState(out finalePBS);

        EventInstance bgmVictory = FindObjectOfType<SpawningScript>().bgmVictory;
        bgmVictory.getPlaybackState(out victoryPBS);

        EventInstance gameOverLoop = FindObjectOfType<CrystalHP>().gameOverLoop;
        gameOverLoop.getPlaybackState(out goPBS);

        if (mainPBS == PLAYBACK_STATE.PLAYING || finalePBS == PLAYBACK_STATE.PLAYING || goPBS == PLAYBACK_STATE.PLAYING || tutPBS == PLAYBACK_STATE.PLAYING)
        {
            bgmMain.release();
            bgmMain.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
            bgmFinale.release();
            bgmFinale.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
            gameOverLoop.release();
            gameOverLoop.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
            bgmVictory.release();
            bgmVictory.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
        }

        if (SceneManager.GetActiveScene().name == "Tutorial Scene")
        {
            EventInstance bgmTut = FindObjectOfType<TutorialBGM>().bgmTut;
            bgmTut.getPlaybackState(out tutPBS);
            if (tutPBS == PLAYBACK_STATE.PLAYING)
            {
                bgmTut.release();
                bgmTut.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
            }
        }
    }
}