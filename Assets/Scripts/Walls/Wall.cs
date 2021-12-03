using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;
using FMOD.Studio;

public class Wall : MonoBehaviour
{
    // Hits needed to break
    public int hitsNeeded = 3;
    // Value used to store hits taken in order to break
    public int hitsTaken;
    // string for differentiating future and past sprites and sfx
    public string playerSide;

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
        if (collision.gameObject.CompareTag("Projectile")) //Skal ændre til entity hvis vi vælger dette system
        {
            hitsTaken += 1;
            Destroy(collision.gameObject);

            if (hitsTaken >= hitsNeeded) 
            {
                switch (playerSide)
                {
                    case "future":
                        RuntimeManager.PlayOneShot("event:/sfx/props/walls/future/destroyed");
                        //Debug.Log("event:/sfx/props/walls/future/destroyed");
                        break;
                    case "past":
                        RuntimeManager.PlayOneShot("event:/sfx/props/walls/past/destroyed");
                        //Debug.Log("event:/sfx/props/walls/past/destroyed");
                        break;
                }

                gameObject.SetActive(false);
            }
            else
            {
                switch (playerSide)
                {
                    case "future":
                        RuntimeManager.PlayOneShot("event:/sfx/props/walls/future/damaged");
                        //Debug.Log("event:/sfx/props/walls/future/damaged");
                        break;
                    case "past":
                        RuntimeManager.PlayOneShot("event:/sfx/props/walls/past/damaged");
                        //Debug.Log("event:/sfx/props/walls/past/damaged");
                        break;
                }
            }
        }
    }
}