using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    // Hits needed to break
    public int hitsNeeded = 3;
    // Value used to store hits taken in order to break
    public int hitsTaken;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnCollisionEnter2D(Collision2D collisionInfo)
    {
        if (collisionInfo.collider.tag == "projectile") //Skal ændre til entity hvis vi vælger dette system
        {
            hitsTaken += 1;

            if (hitsTaken >= hitsNeeded) {
            Destroy (gameObject);
            }
        }
    }
}
