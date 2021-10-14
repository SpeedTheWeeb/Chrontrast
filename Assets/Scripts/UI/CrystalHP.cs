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
<<<<<<< HEAD

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
=======
    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.CompareTag("Projectile")) //Skal ændre til entity hvis vi vælger dette system
>>>>>>> 667785792188e0dac76c5328dc8f38dcd3bad7ad
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
    private void Update()
    {
        if (crystalhealth < 1)
        {
            FindObjectOfType<Gamemanager>().Endgame();

        }
    }
}
