using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
public class PrepPhase : MonoBehaviour
{
    GameObject[] Powerups;
    SpawningScript spawnWave;
    public List<GameObject> PUStr;
    public List<GameObject> PUUtil;
    public List<GameObject> PUDef;

    int nextW = 1;
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
        Powerups = Resources.LoadAll("Powerups", typeof(GameObject)).Cast<GameObject>().ToArray();
        for (int i = 0; i < Powerups.Length; i++)
        {
            switch (Powerups[i].name.Split('_')[1])
            {
                case "Str":
                    PUStr.Add(Powerups[i]);
                    break;
                case "Util":
                    PUUtil.Add(Powerups[i]);
                    break;
                case "Def":
                    PUDef.Add(Powerups[i]);
                    break;
            }
        }
    }

    private void FixedUpdate()
    {
        if(isPrep)
        {
            
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
        PowerupSpawn.SetActive(true);
        SpawnPowerUp();
    }

    void nextWave()
    {
        GameObject[] AllPowerUps = GameObject.FindGameObjectsWithTag("PowerUp");

        for(int pu = 0; pu < AllPowerUps.Length; pu++)
        {
            Destroy(AllPowerUps[pu]);
        }
        PowerupSpawn.SetActive(false);
        isPrep = false;
        WaveUI.text = "Wave " + nextW;
        spawnWave.InitWave(nextW);
    }
    void SpawnPowerUp()
    {
        for(int p = 1; p <= 2; p++)
        {
            int StrRNG = UnityEngine.Random.Range(0, PUStr.Count);
            int UtilRNG = UnityEngine.Random.Range(0, PUUtil.Count);
            int DefRNG = UnityEngine.Random.Range(0, PUDef.Count);

            GameObject objStr = PUStr[StrRNG];
            GameObject objUtil = PUUtil[UtilRNG];
            GameObject objDef = PUDef[DefRNG];

            GameObject StrSpawn = GameObject.Find("PU-Str"+p);
            GameObject UtilSpawn = GameObject.Find("PU-Util" + p);
            GameObject DefSpawn = GameObject.Find("PU-Def" + p);

            Instantiate(objStr, StrSpawn.transform.position, Quaternion.identity);
            Instantiate(objUtil, UtilSpawn.transform.position, Quaternion.identity);
            Instantiate(objDef, DefSpawn.transform.position, Quaternion.identity);
        }
    }
}