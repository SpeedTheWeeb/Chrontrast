using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponent<WeaponManager>().weaponType = 3;
            switch(gameObject.name)
            {
                case "MeleePowerUp":
                    other.GetComponent<WeaponManager>().weaponType = 1;
                    break;

                case "ShotgunPowerUp":
                    other.GetComponent<WeaponManager>().weaponType = 2;
                    break;

                case "SniperPowerUp":
                    other.GetComponent<WeaponManager>().weaponType = 3;
                    break;
            }
            Pickup(other);
        }
    }

    void Pickup(Collider player)
    {
        GetComponent<MeshRenderer>().enabled = false;
        GetComponent<Collider>().enabled = false;

        // enable weapon script && hurtbox-collider

        Destroy(gameObject);
    }
}
