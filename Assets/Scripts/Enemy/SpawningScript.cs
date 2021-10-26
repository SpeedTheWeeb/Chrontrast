using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class SpawningScript : MonoBehaviour
{
    public int currentWave;
    private JsonList enemyInfo;
    public TextAsset jsonText;
    public int totalEnemies;
    public GameObject spawnSide;
    int reds;
    int blues;
    int greens;
    public bool isSpawning;

    public GameObject RedObj;
    public GameObject BlueObj;
    public GameObject GreenObj;
    // Start is called before the first frame update
    void Start()
    {
        isSpawning = false;
        currentWave = 0;
        enemyInfo = JsonUtility.FromJson<JsonList>(jsonText.text);
        InitWave();
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void InitWave()
    {
        currentWave += 1;
        Debug.Log(currentWave);
        try
        {
            reds = enemyInfo.WaveInfo[currentWave - 1].Red_Spawn;
            blues = enemyInfo.WaveInfo[currentWave - 1].Blue_Spawn;
            greens = enemyInfo.WaveInfo[currentWave - 1].Green_Spawn;

            totalEnemies = enemyInfo.WaveInfo[currentWave - 1].Red_Spawn + enemyInfo.WaveInfo[currentWave - 1].Blue_Spawn + enemyInfo.WaveInfo[currentWave - 1].Green_Spawn;

            spawnEnemies();
        }
        catch(IndexOutOfRangeException)
        {
            Debug.Log("Woo Win");
            throw null;
        }
    }

    public void spawnEnemies()
    {
        if (totalEnemies > 0)
        {
            isSpawning = true;
            Debug.Log(totalEnemies);
            int randomR = UnityEngine.Random.Range(1, reds + 1);
            int randomG = UnityEngine.Random.Range(1, greens + 1);
            int randomB = UnityEngine.Random.Range(1, blues + 1);

            //Red Spawning
            for (int r = 0; r < randomR; r++)
            {
                int rNumber = UnityEngine.Random.Range(1, 3);
                float srNumber = UnityEngine.Random.Range(-3, 4);
                spawnSide = GameObject.Find("Enemy Spawn " + rNumber);

                Vector3 pos = new Vector3(
                    spawnSide.transform.position.x,
                    spawnSide.transform.position.y + srNumber,
                    spawnSide.transform.position.z);

                GameObject redClone = Instantiate(RedObj, pos, Quaternion.identity);
                redClone.GetComponent<RedScript>().stopSpot = rNumber;
            }

            //Blue Spawning
            for (int b = 0; b < randomB; b++)
            {
                int bNumber = UnityEngine.Random.Range(1, 3);
                float sbNumber = UnityEngine.Random.Range(-3, 4);
                spawnSide = GameObject.Find("Enemy Spawn " + bNumber);

                Vector3 pos = new Vector3(
                    spawnSide.transform.position.x,
                    spawnSide.transform.position.y + sbNumber,
                    spawnSide.transform.position.z);

                GameObject blueClone = Instantiate(BlueObj, pos, Quaternion.identity);
                blueClone.GetComponent<BlueScript>().stopSpot = bNumber;
            }

            //Green Spawning
            for (int g = 0; g < randomG; g++)
            {
                int gNumber = UnityEngine.Random.Range(1, 3);
                float sgNumber = UnityEngine.Random.Range(-3, 4);
                spawnSide = GameObject.Find("Enemy Spawn " + gNumber);

                Vector3 pos = new Vector3(
                    spawnSide.transform.position.x,
                    spawnSide.transform.position.y + sgNumber,
                    spawnSide.transform.position.z);

                GameObject GreenClone = Instantiate(GreenObj, pos, Quaternion.identity);
                GreenClone.GetComponent<GreenScript>().stopSpot = gNumber;
            }
            greens -= randomG;
            reds -= randomR;
            blues -= randomB;
            totalEnemies -= randomB + randomG + randomR;
            Debug.Log(totalEnemies);
            Invoke("spawnEnemies", 10f);
        }
        else
        {
            isSpawning = false;
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
}