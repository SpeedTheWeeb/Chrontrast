using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GreenScript : MonoBehaviour
{
    public float speed = 3f;
    public int stopSpot;
    bool forward = true;
    bool isArrived = false;
    public GameObject bullet;
    public float timer = 4f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (forward)
        {
            float vel = speed * Time.deltaTime;

            transform.position = Vector3.MoveTowards(transform.position, new Vector3(0, Random.Range(-10, 11), 0), vel);
        }
        if (isArrived)
        {
            timer -= Time.deltaTime;
            if (timer <= 0)
            {
                Instantiate(bullet, transform.position, Quaternion.identity);
                timer = 2f;
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.name == "Green Stop")
        {
            forward = false;
            isArrived = true;
        }
    }
}
