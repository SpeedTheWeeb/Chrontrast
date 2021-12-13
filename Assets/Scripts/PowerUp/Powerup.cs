using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMOD.Studio;
using FMODUnity;

public class Powerup : MonoBehaviour
{
    WeaponManager weapon;
    public GameObject player;
    string PUType;
    string PUName;

    // Start is called before the first frame update
    void Start()
    {
        PUName = gameObject.name.Split('_')[0];
        PUType = gameObject.name.Split('_')[1];

    }

    // Update is called once per frame
    void Update()
    {

    }
    public void activatePU()
    {
        weapon = player.GetComponent<WeaponManager>();
        GameObject[] objects = GameObject.FindGameObjectsWithTag("PowerUp");

        GetPowerup();
        foreach (GameObject game in objects)
        {
            if (game.name.Contains(player.name) && !game.name.Contains("Clone"))
            {
                Destroy(game);
            }
        }
        
    }

    void GetPowerup()
    {
        if(PUType.Contains("Str"))
        {
            Strength();
        }
        else if (PUType.Contains("Util"))
        {
            Utility();
        }
        else if (PUType.Contains("Def"))
        {
            Defense();
        }
    }

    void Strength()
    {
        switch(PUName)
        {
            case "ATS":
                weapon.sniperASMod +=  0.2f;
                weapon.splashASMod +=  0.5f;
                weapon.meleeASMod +=  0.2f;
                weapon.powerup = null;
                weapon.havePowerup = false;
                break;
            case "Stronger":
                weapon.sniperDmgMod += 2f;
                weapon.splashDmgMod += 20f;
                weapon.meleeDmgMod +=  5f;
                weapon.powerup = null;
                weapon.havePowerup = false;
                break;
        }
    }

    void Utility()
    {
        //TODO
        switch (PUName)
        {
            case "Freeze":
                break;
            case "Glue":
                break;
        }
    }

    void Defense()
    {
        switch (PUName)
        {
            case "Heal":
                CrystalHP hp = GameObject.Find("Crystal").GetComponent<CrystalHP>();
                hp.crystalhealth = hp.crystalhealth + 20;
                hp.UpdateHealth();
                RuntimeManager.PlayOneShot("event:/sfx/powerups/heal");
                weapon.powerup = null;
                weapon.havePowerup = false;
                break;
            case "Repair":
                break;
        }
    }


}
