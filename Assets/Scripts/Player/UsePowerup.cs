using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class UsePowerup : MonoBehaviour
{
    public GameObject Glue;
    public int playerNumber;
    WeaponManager info;
    public Image freezeImg;
    // Start is called before the first frame update
    void Start()
    {
        info = gameObject.GetComponent<WeaponManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetButtonDown("PU"+playerNumber))
        {
            Debug.Log("Using PU");
            Use();
        }
    }

    void Use()
    {
        
        if(info.havePowerup)
        {
            string pu = info.powerup;
            switch(pu.Split('_')[0])
            {
                case "Repair":
                    break;
                case "Glue":
                    SpawnGlue();
                    break;
                case "Freeze":
                    Freeze();
                    break;
            }
            
            info.powerup = null;
            info.havePowerup = false;
        }
    }

    void Freeze()
    {
        freezeImg.enabled = true;
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        foreach(GameObject enemy in enemies)
        {
            if(enemy.name.Contains("Blue"))
            {
                BlueScript blue = enemy.GetComponent<BlueScript>();
                blue.Freeze();
            }
            else if(enemy.name.Contains("Green"))
            {
                GreenScript green = enemy.GetComponent<GreenScript>();
                green.Freeze();
            }
            else if(enemy.name.Contains("Red"))
            {
                RedScript red = enemy.GetComponent<RedScript>();
                red.Freeze();
            }                
        }
        Invoke("StopFreeze", 5f);
    }

    void StopFreeze()
    {
        freezeImg.enabled = false;
    }

    void SpawnGlue()
    {
        Instantiate(Glue, transform.position, Quaternion.identity);
    }

    void RepairWall()
    {

    }
}
