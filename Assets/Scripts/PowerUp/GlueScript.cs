using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlueScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Enemy"))
        {
            RedScript movSpeed = collision.GetComponent<RedScript>();
            movSpeed.speed = movSpeed.speed * 0.1f;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            RedScript movSpeed = collision.GetComponent<RedScript>();
            movSpeed.speed = movSpeed.baseSpeed;
        }
    }
}
