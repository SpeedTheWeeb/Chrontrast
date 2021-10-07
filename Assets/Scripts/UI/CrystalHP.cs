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
            if (collisionInfo.collider.tag == "bullet") //Skal ændre til entity hvis vi vælger dette system
            {
                crystalhealth = crystalhealth -1;
                Debug.Log(crystalhealth);
            }

        }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "bullet") //Skal ændre til entity hvis vi vælger dette system
        {
            crystalhealth = crystalhealth - 1;
            Debug.Log(crystalhealth);
        }
    }
}
