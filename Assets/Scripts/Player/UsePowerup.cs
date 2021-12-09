using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UsePowerup : MonoBehaviour
{
    public int playerNumber;
    WeaponManager info;

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
            Use();
        }
    }

    void Use()
    {
        if(info.havePowerup)
        {
            GameObject pu = info.powerup;
        }
    }
}
