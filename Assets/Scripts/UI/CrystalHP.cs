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
            if (collisionInfo.collider.tag == "projectile") //Skal �ndre til entity hvis vi v�lger dette system
            {
                crystalhealth = crystalhealth -1;
                Debug.Log(crystalhealth);
            }

        }
    private void OnTriggerEnter(Collider collision)
    {
        if (collision.tag == "projectile") //Skal �ndre til entity hvis vi v�lger dette system
        {
            crystalhealth = crystalhealth - 1;
            Debug.Log(crystalhealth);
        }
    }
}
