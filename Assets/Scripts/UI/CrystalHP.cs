using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrystalHP : MonoBehaviour
{
    public int crystalhealth;
    public bool crystaldeath = false; // husk, måske slet hvis deathscreen virker
    private void Start()
    {
        crystalhealth = 100;
        
    }

        void OnCollisionEnter(Collision collisionInfo)
        {
            if (collisionInfo.collider.tag == "projectile") //tag imod skade fra fjendeskud
            {
                crystalhealth = crystalhealth -1;
                Debug.Log(crystalhealth);
            }

        }
    private void OnTriggerEnter(Collider collision)
    {
        if (collision.tag == "projectile") //tag skade fra fjendeskud
        {
            crystalhealth = crystalhealth - 1;
            Debug.Log(crystalhealth);
        }
    }
    private void Update()
    {
        if (crystalhealth < 1)
        {
            FindObjectOfType<Gamemanager>().Endgame();

        }
    }
}
