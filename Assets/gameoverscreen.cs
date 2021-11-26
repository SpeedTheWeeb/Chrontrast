
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using FMOD.Studio;
using FMODUnity;

public class gameoverscreen : MonoBehaviour
{
    EventInstance gameOverLoop;
    PLAYBACK_STATE playbackState;

    void Awake()
    {
        gameOverLoop = RuntimeManager.CreateInstance("event:/bgm/game_over");
        gameOverLoop.start();
        gameOverLoop.getPlaybackState(out playbackState);
    }

    public void Setup() 
    {
        gameObject.SetActive (true);
    }

    public void RestartButton() 
    {
        if (playbackState != PLAYBACK_STATE.STOPPED)
            gameOverLoop.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
        SceneManager.LoadScene("Game Scene"); //�ndres til startsk�rm n�r vi har s�dan en
        Time.timeScale = 1f;
    }
    

}
