using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && other.GetComponent<WeaponManager>().alreadyHolding == false)
        {
            other.GetComponent<WeaponManager>().weaponType = 0;
            switch(gameObject.name)
            {
                case "MeleePowerUp":                    
                    other.GetComponent<WeaponManager>().weaponType = 1;
                    other.GetComponent<WeaponManager>().alreadyHolding = true;                    
                    break;

                case "ShotgunPowerUp":                    
                    other.GetComponent<WeaponManager>().weaponType = 2;
                    other.GetComponent<WeaponManager>().alreadyHolding = true;                    
                    break;

                case "SniperPowerUp":                    
                    other.GetComponent<WeaponManager>().weaponType = 3;
                    other.GetComponent<WeaponManager>().alreadyHolding = true;                    
                    break;
            }
            Pickup(other);
        }
    }

    void Pickup(Collider2D player)
    {
        Destroy(gameObject);
    }
}
