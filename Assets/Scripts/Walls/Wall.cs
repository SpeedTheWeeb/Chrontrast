using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wall : MonoBehaviour
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
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Projectile") && hitsTaken < hitsNeeded) //Skal ændre til entity hvis vi vælger dette system
        {
            hitsTaken += 1;
            Destroy(collision.gameObject);
        }
    }
}