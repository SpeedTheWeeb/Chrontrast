using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Powerup : MonoBehaviour
{
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
        GetPowerup();
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
                break;
            case "Stronger":
                break;
        }
    }

    void Utility()
    {
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
                break;
            case "Repair":
                break;
        }
    }
}
