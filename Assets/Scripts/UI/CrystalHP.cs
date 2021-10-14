using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrystalHP : MonoBehaviour
{
    public int crystalhealth;
    private void Start()
    {
        crystalhealth = 100;
    }
    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.CompareTag("Projectile")) //Skal �ndre til entity hvis vi v�lger dette system
        {
            crystalhealth = crystalhealth - 1;
            Debug.Log(crystalhealth);
            Destroy(collision.gameObject);
        }
        if(collision.gameObject.CompareTag("Bullet"))
        {
            Destroy(collision.gameObject);
        }
    }
}
