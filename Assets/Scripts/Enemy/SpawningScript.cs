using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawningScript : MonoBehaviour
{
    public int currentWave;
    private JsonList enemyInfo;
    public TextAsset jsonText;
    private int totalEnemies;
    public GameObject spawnSide;

    public GameObject RedObj;
    public GameObject BlueObj;
    public GameObject GreenObj;
    // Start is called before the first frame update
    void Start()
    {
        currentWave = 1;
        spawnEnemies(1);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void spawnEnemies(int Wave)
    {
        enemyInfo = JsonUtility.FromJson<JsonList>(jsonText.text);

        for (int r = 0; r < enemyInfo.WaveInfo[currentWave - 1].Red_Spawn; r++)
        {
            int rNumber = Random.Range(1, 3);
            float srNumber = Random.Range(-3, 4);
            spawnSide = GameObject.Find("Enemy Spawn " + rNumber);

            Vector3 pos = new Vector3(
                spawnSide.transform.position.x,
                spawnSide.transform.position.y + srNumber,
                spawnSide.transform.position.z);

            GameObject redClone = Instantiate(RedObj, pos, Quaternion.identity);
            redClone.GetComponent<RedScript>().stopSpot = rNumber;
        }

        for (int b = 0; b < enemyInfo.WaveInfo[currentWave - 1].Blue_Spawn; b++)
        {
            int bNumber = Random.Range(1, 3);
            float sbNumber = Random.Range(-3, 4);
            spawnSide = GameObject.Find("Enemy Spawn " + bNumber);

            Vector3 pos = new Vector3(
                spawnSide.transform.position.x,
                spawnSide.transform.position.y + sbNumber,
                spawnSide.transform.position.z);

            GameObject blueClone = Instantiate(BlueObj, pos, Quaternion.identity);
            blueClone.GetComponent<BlueScript>().stopSpot = bNumber;
        }
        for (int g = 0; g < enemyInfo.WaveInfo[currentWave - 1].Green_Spawn; g++)
        {
            int gNumber = Random.Range(1, 3);
            float sgNumber = Random.Range(-3, 4);
            spawnSide = GameObject.Find("Enemy Spawn " + gNumber);

            Vector3 pos = new Vector3(
                spawnSide.transform.position.x,
                spawnSide.transform.position.y + sgNumber,
                spawnSide.transform.position.z);

            GameObject GreenClone = Instantiate(GreenObj, pos, Quaternion.identity);
            GreenClone.GetComponent<GreenScript>().stopSpot = gNumber;
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