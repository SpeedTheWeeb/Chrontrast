using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
    bool Trigger = false;
    GameObject player;
    private void Update()
    {
        if(Input.GetButtonDown("Drop1") && Trigger)
        {
            chooseWeapon();
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        Trigger = false;
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && other.GetComponent<WeaponManager>().alreadyHolding == false)
        {
            Trigger = true;
            player = other.gameObject;
        }
    }

    void chooseWeapon()
    {
        player.GetComponent<WeaponManager>().weaponType = 0;
        switch(gameObject.name)
        {
            case "MeleePowerUp":
                player.GetComponent<WeaponManager>().weaponType = 1;
                player.GetComponent<WeaponManager>().alreadyHolding = true;
                break;

            case "MeleePowerUp(Clone)":
                player.GetComponent<WeaponManager>().weaponType = 1;
                player.GetComponent<WeaponManager>().alreadyHolding = true;
                break;

            case "ShotgunPowerUp":
                player.GetComponent<WeaponManager>().weaponType = 2;
                player.GetComponent<WeaponManager>().alreadyHolding = true;
                break;

            case "ShotgunPowerUp(Clone)":
                player.GetComponent<WeaponManager>().weaponType = 2;
                player.GetComponent<WeaponManager>().alreadyHolding = true;
                break;

            case "SniperPowerUp":
                player.GetComponent<WeaponManager>().weaponType = 3;
                player.GetComponent<WeaponManager>().alreadyHolding = true;                    
                break;


            case "SniperPowerUp(Clone)":
                player.GetComponent<WeaponManager>().weaponType = 3;
                player.GetComponent<WeaponManager>().alreadyHolding = true;
                break;
        }
        Pickup(player);
        
    }

    void Pickup(GameObject pl)
    {
        Destroy(gameObject);
    }
}
