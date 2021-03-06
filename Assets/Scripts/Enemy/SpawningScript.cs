using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using FMOD.Studio;
using FMODUnity;

public class SpawningScript : MonoBehaviour
{
    public Text waveUI;
    public Text counterUI;
    public int currentWave;
    private JsonList enemyInfo;
    public TextAsset jsonText;
    public int totalEnemies;
    public GameObject spawnSide;
    int reds;
    int blues;
    int greens;
    int totalSpawn;
    public bool isSpawning;
    Vector3 rPos, bPos, gPos;
    public GameObject RedObjFan;
    public GameObject RedObjMed;
    public GameObject BlueObjFan;
    public GameObject BlueObjMed;
    public GameObject GreenObjFan;
    public GameObject GreenObjMed;
    int arenaEnemies;
    //FMOD

    public EventInstance bgmMain;
    public EventInstance bgmFinale;
    public EventInstance bgmVictory;
    public bool prepPhase;
    float phase = 0f;
    float choir;
    float brass;
    float harp;
    bool beginGame;

    // Start is called before the first frame update
    void Start()
    {
        isSpawning = false;

        //FMOD
        bgmMain = RuntimeManager.CreateInstance("event:/bgm/main");
        bgmFinale = RuntimeManager.CreateInstance("event:/bgm/finale");
        bgmVictory = RuntimeManager.CreateInstance("event:/bgm/victory");
        phase = 0f;
        choir = 0f;
        brass = 0f;
        harp = 0f;
        bgmMain.start();
        
        currentWave = 0;
        enemyInfo = JsonUtility.FromJson<JsonList>(jsonText.text);

        waveUI.text = "Starting Game";
    }

    //IEnumerator StartGame()
    //{
    //    waveUI.text = "Starting Game";
    //    for(int i = 5; i > 0; i--)
    //    {
    //        yield return new WaitForSeconds(1);
    //        counterUI.text = i.ToString();
    //    }
    //    counterUI.text = "";
    //    waveUI.text = "Wave 1";
    //    InitWave(1);
    //}

    // Update is called once per frame
    void FixedUpdate()
    {
        prepPhase = GameObject.Find("InitObj").GetComponent<PrepPhase>().isPrep;
        BGM();
    }
    public void InitWave(int Wave)
    {
        currentWave += 1;
        try
        {
            reds = enemyInfo.WaveInfo[Wave - 1].Red_Spawn;
            blues = enemyInfo.WaveInfo[Wave - 1].Blue_Spawn;
            greens = enemyInfo.WaveInfo[Wave - 1].Green_Spawn;
            totalSpawn = enemyInfo.WaveInfo[Wave - 1].Total;

            totalEnemies = enemyInfo.WaveInfo[Wave - 1].Red_Spawn + enemyInfo.WaveInfo[Wave - 1].Blue_Spawn + enemyInfo.WaveInfo[Wave - 1].Green_Spawn;
            StartCoroutine(countEnemies());
            spawnEnemies();
        }
        catch(IndexOutOfRangeException)
        {
            Debug.Log("Woo Win");
            FindObjectOfType<Gamemanager>().Victory();
            StartVictoryMusic();
        }
    }
    IEnumerator countEnemies()
    {
        while(isSpawning)
        {
            arenaEnemies = GameObject.FindGameObjectsWithTag("Enemy").Length;
            yield return new WaitForSeconds(1);
        }
    }
    public void spawnEnemies()
    {
        if (totalEnemies > 0)
        {
            if (totalSpawn > arenaEnemies)
            {
                isSpawning = true;
                int randomR = UnityEngine.Random.Range(0, reds + 1);
                int randomG = UnityEngine.Random.Range(0, greens + 1);
                int randomB = UnityEngine.Random.Range(0, blues + 1);

                //Red Spawning
                for (int r = 0; r < randomR; r++)
                {
                    int rNumber = UnityEngine.Random.Range(1, 7);

                    spawnSide = GameObject.Find("Enemy Spawn " + rNumber);

                    if (rNumber <= 2)
                    {
                        float srNumber = UnityEngine.Random.Range(-5, 5);
                        rPos = new Vector3(
                            spawnSide.transform.position.x,
                            spawnSide.transform.position.y + srNumber,
                            spawnSide.transform.position.z);
                    }
                    else
                    {
                        float srNumber = UnityEngine.Random.Range(-3, 3);
                        rPos = new Vector3(
                            spawnSide.transform.position.x + srNumber,
                            spawnSide.transform.position.y,
                            spawnSide.transform.position.z);

                    }

                    if (rNumber % 2 == 0)
                    {
                        GameObject redClone = Instantiate(RedObjFan, rPos, Quaternion.identity);
                    }
                    else
                    {
                        GameObject redClone = Instantiate(RedObjMed, rPos, Quaternion.identity);
                    }
                }

                //Blue Spawning
                for (int b = 0; b < randomB; b++)
                {
                    int bNumber = UnityEngine.Random.Range(1, 7);
                    spawnSide = GameObject.Find("Enemy Spawn " + bNumber);

                    if (bNumber <= 2)
                    {
                        float sbNumber = UnityEngine.Random.Range(-3, 3);
                        bPos = new Vector3(
                            spawnSide.transform.position.x,
                            spawnSide.transform.position.y + sbNumber,
                            spawnSide.transform.position.z);
                    }
                    else
                    {
                        float sbNumber = UnityEngine.Random.Range(-4, 4);
                        bPos = new Vector3(
                            spawnSide.transform.position.x + sbNumber,
                            spawnSide.transform.position.y,
                            spawnSide.transform.position.z);


                    }

                    if (bNumber % 2 == 0)
                    {
                        GameObject blueClone = Instantiate(BlueObjFan, bPos, Quaternion.identity);
                    }
                    else
                    {
                        GameObject blueClone = Instantiate(BlueObjMed, bPos, Quaternion.identity);
                    }
                }

                //Green Spawning
                for (int g = 0; g < randomG; g++)
                {
                    int gNumber = UnityEngine.Random.Range(1, 7);

                    spawnSide = GameObject.Find("Enemy Spawn " + gNumber);

                    if (gNumber <= 2)
                    {
                        float sgNumber = UnityEngine.Random.Range(-5, 5);
                        gPos = new Vector3(
                            spawnSide.transform.position.x,
                            spawnSide.transform.position.y + sgNumber,
                            spawnSide.transform.position.z);
                    }
                    else
                    {
                        float sgNumber = UnityEngine.Random.Range(-3, 3);
                        gPos = new Vector3(
                            spawnSide.transform.position.x + sgNumber,
                            spawnSide.transform.position.y,
                            spawnSide.transform.position.z);

                    }

                    if (gNumber % 2 == 0)
                    {
                        GameObject GreenClone = Instantiate(GreenObjFan, gPos, Quaternion.identity);
                    }
                    else
                    {
                        GameObject GreenClone = Instantiate(GreenObjMed, gPos, Quaternion.identity);
                    }
                }
                greens -= randomG;
                reds -= randomR;
                blues -= randomB;
                totalEnemies -= randomB + randomG + randomR;
            }
            Invoke("spawnEnemies", 5f);
        }
        else
        {
            StopCoroutine(countEnemies());
            isSpawning = false;
        }
    }

    public void BGM()
    {
        bgmMain.setParameterByName("Prep-Wave", phase);
        bgmMain.setParameterByName("Choir", choir);
        bgmMain.setParameterByName("Brass", brass);
        bgmMain.setParameterByName("Harp", harp);

        if (prepPhase)
            phase = 0f;
        else
            phase = 1f;

        if (currentWave >= 2 && currentWave < 5)
        {
            brass = 1f;
        }
        if (currentWave >= 3 && currentWave < 5)
        {
            choir = 1f;
        }
        if (currentWave >= 4 && currentWave < 5)
        {
            harp = 1f;
        }

        if (currentWave == 5)
        {
            bgmMain.getPlaybackState(out PLAYBACK_STATE pbsMain);
            if(pbsMain == PLAYBACK_STATE.PLAYING)
                bgmMain.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);

            bgmFinale.getPlaybackState(out PLAYBACK_STATE pbsFinale);
                            
            if (pbsFinale != PLAYBACK_STATE.PLAYING)
            {                
                bgmFinale.start();                
            }                        
        }

        if(currentWave >= 6)
        {
            bgmMain.getPlaybackState(out PLAYBACK_STATE pbsMain);
            if (pbsMain == PLAYBACK_STATE.PLAYING)
                bgmMain.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);

            bgmFinale.getPlaybackState(out PLAYBACK_STATE pbsFinale);
            if (pbsFinale == PLAYBACK_STATE.PLAYING)
                bgmFinale.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);

            bgmVictory.getPlaybackState(out PLAYBACK_STATE pbsVictory);

            if (pbsVictory != PLAYBACK_STATE.PLAYING)
            {
                bgmVictory.start();
            }
        }
    }

    void StartVictoryMusic()
    {
        bgmMain.getPlaybackState(out PLAYBACK_STATE mainPBS);
        bgmFinale.getPlaybackState(out PLAYBACK_STATE finalePBS);


        if (mainPBS == PLAYBACK_STATE.PLAYING || finalePBS == PLAYBACK_STATE.PLAYING)
        {
            bgmMain.release();
            bgmMain.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
            bgmFinale.release();
            bgmFinale.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
            bgmVictory = RuntimeManager.CreateInstance("event:/bgm/victory");
            bgmVictory.start();
        }
    }
}

[System.Serializable]
public class JsonList
{
    public EnemyModel[] WaveInfo;
}
[System.Serializable]
public class EnemyModel
{
    public int Wave;
    public int Red_Spawn;
    public int Blue_Spawn;
    public int Green_Spawn;
    public int Total;
}