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

    /* 
        This "OnCollisionEnter2D" checks for what I assume to be any
        2D collision. This needs to be changed in the future to check
        for a specific colision made by certain objects (attacks from
        enemies in the game). 
        Long story short, I don't know how this check is made in Unity
        (sorry). Also these objects don't exist, so I wouldn't know the
        name of the objects I'm checking for anyways.
    */ 

    void OnCollisionEnter2D(Collision2D collisionInfo)
    {
        hitsTaken += 1;
        
        if (hitsTaken >= hitsNeeded) {
            Destroy (gameObject);
        }
    }
}
