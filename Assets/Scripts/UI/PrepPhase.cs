using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class PrepPhase : MonoBehaviour
{
    UnityEngine.Object[] Powerups;
    SpawningScript spawnWave;
    public int nextW = 1;
    bool isPrep = false;
    public Text WaveUI;
    public Text downTimer;
    float timer = 30f;
    public GameObject PowerupSpawn;
    // Start is called before the first frame update
    void Start()
    {
        GameObject crystal = GameObject.Find("Crystal");
        spawnWave = (SpawningScript)crystal.GetComponent("SpawningScript");
        Powerups = Resources.LoadAll("Powerups");

    }

    // Update is called once per frame
    void Update()
    {
        if(isPrep)
        {
            PowerupSpawn.SetActive(true);
            downTimer.enabled = true;
            timer -= Time.deltaTime;
            downTimer.text = Math.Floor(timer).ToString();
            if (timer <= 0)
            {
                downTimer.enabled = false;
                timer = 30f;
                nextWave();
            }
        }
    }

    public void StartPrep()
    {
        nextW = spawnWave.currentWave + 1;
        isPrep = true;
    }

    void nextWave()
    {
        PowerupSpawn.SetActive(false);
        WaveUI.text = "Wave " + nextW;
        spawnWave.InitWave(nextW);
    }
    void SpawnPowerUp()
    {

    }
}
