using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class PrepPhase : MonoBehaviour
{
    SpawningScript spawnWave;
    public int nextW;
    bool isPrep = false;
    public Text WaveUI;
    public Text downTimer;
    float timer = 10f;

    // Start is called before the first frame update
    void Start()
    {
        GameObject crystal = GameObject.Find("Crystal");
        spawnWave = (SpawningScript)crystal.GetComponent("SpawningScript");
        
    }

    // Update is called once per frame
    void Update()
    {
        if(isPrep)
        {
            downTimer.enabled = true;
            timer -= Time.deltaTime;
            downTimer.text = Math.Floor(timer).ToString();
            if (timer <= 0)
            {
                downTimer.enabled = false;
                timer = 10f;
                nextWave();
            }
        }
    }

    void SpawnPowerUp()
    {

    }

    public void StartPrep()
    {
        nextW = spawnWave.currentWave + 1;
        isPrep = true;
    }

    void nextWave()
    {
        WaveUI.text = "Wave " + nextW;
        spawnWave.InitWave(nextW);
    }
}
