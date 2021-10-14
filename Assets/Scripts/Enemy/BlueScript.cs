using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlueScript : MonoBehaviour
{
    public float speed = 5f;
    public int stopSpot;
    bool forward = true;
    bool isArrived = false;
    public GameObject bullet;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if(forward)
        {
            float vel = speed * Time.deltaTime;

            transform.position = Vector3.MoveTowards(transform.position, new Vector3(0, 0, 0), vel);
        }

        if(isArrived)
        {
            StartCoroutine(shoot());
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Bullet")
        {
            Destroy(other);
            Destroy(gameObject);
        }
        if(other.gameObject.name == "Blue Stop")
        {
            forward = false;
            isArrived = true;
        }
    }

    IEnumerator shoot()
    {
        yield return new WaitForSeconds(2);
        Instantiate(bullet, transform.position, transform.rotation);
    }
}
