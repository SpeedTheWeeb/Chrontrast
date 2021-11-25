using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WaveChecker : MonoBehaviour
{
    SpawningScript Spawning;
    public Text WaveUI;
    public Text downTimer;
    float timer = 10f;
    // Start is called before the first frame update
    void Start()
    {
        GameObject crystal = GameObject.Find("Crystal");
        Spawning = (SpawningScript)crystal.GetComponent("SpawningScript");
    }

    // Update is called once per frame
    void Update()
    {
        if (GameObject.FindGameObjectsWithTag("Enemy").Length <= 0 && Spawning.totalEnemies <= 0)
        {
            downTimer.enabled = true;
            timer -= Time.deltaTime;
            downTimer.text = Math.Floor(timer).ToString();
            if (timer <= 0)
            {
                downTimer.enabled = false;
                timer = 10f;
                StartNextWave();
            }
        }
    }
    void StartNextWave()
    {
        int nextWave = Spawning.currentWave + 1;
        WaveUI.text = "Wave " + nextWave;
        Spawning.InitWave(nextWave);
    }
}
