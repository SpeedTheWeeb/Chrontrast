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

        void OnCollisionEnter2D(Collision2D collisionInfo)
        {
            if (collisionInfo.collider.tag == "projectile") //Skal �ndre til entity hvis vi v�lger dette system
            {
               crystalhealth = crystalhealth -1;
            }

        }
    }
