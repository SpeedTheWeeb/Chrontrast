using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using FMOD.Studio;
using FMODUnity;
using JetBrains.Annotations;

public class CrystalHP : MonoBehaviour
{
    public Text TextUI;
    public int crystalhealth;
    public CameraShake cameraShake;

    public EventInstance gameOverLoop;
    PLAYBACK_STATE mainPBS;
    PLAYBACK_STATE finalePBS;

    private void Start()
    {
        crystalhealth = 100;
        UpdateHealth();

    }

    public void UpdateHealth()
    {
        TextUI.text = "Crystal HP: " + crystalhealth;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Projectile")) //Skal �ndre til entity hvis vi v�lger dette system
        {
            HP(1);
            Destroy(collision.gameObject);
        }
        if (collision.gameObject.CompareTag("Bullet"))
        {
            Destroy(collision.gameObject);
        }
    }
    public void HP(int damage)
    {
        crystalhealth = crystalhealth - damage;
        UpdateHealth();
        StartCoroutine(cameraShake.Shake(.15f, .2f));
        RuntimeManager.PlayOneShot("event:/sfx/props/crystal/damaged");

        if (crystalhealth <= 0)
        {
            //Invoke("EndGame", 0.5f);
            RuntimeManager.PlayOneShot("event:/sfx/props/crystal/destroyed");
            StartCoroutine(EndGame());
            StartGameOverMusic();           

            // play animation crystal shatter
            Time.timeScale = 0f;
        }
    }
    IEnumerator EndGame()
    {
        yield return new WaitForSecondsRealtime(2f);
        FindObjectOfType<Gamemanager>().Endgame(); // invoke to postpone game over screen until animation and sfx for the shattered crystal is finished                
    }

    void StartGameOverMusic()
    {
        EventInstance bgmMain = FindObjectOfType<SpawningScript>().bgmMain;
        bgmMain.getPlaybackState(out mainPBS);
        EventInstance bgmFinale = FindObjectOfType<SpawningScript>().bgmFinale;
        bgmFinale.getPlaybackState(out finalePBS);

        if (mainPBS == PLAYBACK_STATE.PLAYING || finalePBS == PLAYBACK_STATE.PLAYING)
        {
            bgmMain.release();
            bgmMain.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
            bgmFinale.release();
            bgmFinale.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
            gameOverLoop = RuntimeManager.CreateInstance("event:/bgm/game_over");
            gameOverLoop.start();
        }
    }
}
