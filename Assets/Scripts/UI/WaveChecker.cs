using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WaveChecker : MonoBehaviour
{
    PrepPhase prep;
    SpawningScript Spawning;
    public Text WaveUI;
    public Text downTimer;
    float timer = 10f;

    // Start is called before the first frame update
    void Start()
    {
        GameObject crystal = GameObject.Find("Crystal");
        GameObject init = GameObject.Find("InitObj");
        Spawning = (SpawningScript)crystal.GetComponent("SpawningScript");
        prep = (PrepPhase)init.GetComponent("PrepPhase");
    }

    // Update is called once per frame
    void Update()
    {
        if (GameObject.FindGameObjectsWithTag("Enemy").Length <= 0 && Spawning.totalEnemies <= 0)
        {
            StartNextWave();
        }
    }
    void StartNextWave()
    {
        prep.StartPrep();
    }
}
