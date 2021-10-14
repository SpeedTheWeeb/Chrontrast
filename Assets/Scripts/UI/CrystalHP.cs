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

        void OnCollisionEnter(Collision collisionInfo)
        {
            if (collisionInfo.collider.tag == "projectile") //Skal ændre til entity hvis vi vælger dette system
            {
                crystalhealth = crystalhealth -1;
                Debug.Log(crystalhealth);
            }

        }
    private void OnTriggerEnter(Collider collision)
    {
        if (collision.tag == "projectile") //Skal ændre til entity hvis vi vælger dette system
        {
            crystalhealth = crystalhealth - 1;
            Debug.Log(crystalhealth);
        }
    }
}
