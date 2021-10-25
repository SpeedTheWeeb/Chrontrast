using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedScript : MonoBehaviour
{
    public float speed;
    public int stopSpot;
    bool forward = true;
    // Start is called before the first frame update
    void Start()
    {
        speed = Random.Range(2, 10);
    }

    // Update is called once per frame
    void Update()
    {
        if (forward)
        {
            float vel = speed * Time.deltaTime;

            transform.position = Vector3.MoveTowards(transform.position, new Vector3(0, Random.Range(-10, 11), 0), vel);
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Bullet")
        {
            Destroy(other.gameObject);
            Destroy(gameObject);
        }
        if (other.gameObject.name == "Red Stop")
        {
            forward = false;
        }
    }
    //private void OnTriggerEnter(Collider other)
    //{
    //    if(other.gameObject.tag == "Bullet")
    //    {
    //        Destroy(other.gameObject);
    //        Destroy(gameObject);
    //    }
    //    if (other.gameObject.name == "Red Stop")
    //    {
    //        forward = false;
    //    }
    //}
}
