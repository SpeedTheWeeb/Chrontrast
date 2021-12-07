using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        GetPowerup();
        Destroy(gameObject);
        switch(player.name)
        {
            case "P1":
                break;
            case "P2":
                break;
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
                weapon.sniperASMod +=  2f;
                weapon.splashASMod +=  5f;
                weapon.meleeASMod +=  3f;
                break;
            case "Stronger":
                weapon.sniperDmgMod += 2f;
                weapon.splashDmgMod += 20f;
                weapon.meleeDmgMod +=  5f;
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
                hp.crystalhealth += 20;
                hp.UpdateHealth();
                break;
            case "Repair":
                break;
        }
    }
}
