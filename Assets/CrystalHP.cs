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
            if (collisionInfo.collider.tag == "projectile") //Skal ændre til entity hvis vi vælger dette system
            {
               crystalhealth = crystalhealth -1;
            }

        }
    }
