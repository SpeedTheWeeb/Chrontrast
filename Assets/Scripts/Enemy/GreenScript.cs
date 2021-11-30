using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;
using FMOD.Studio;

public class GreenScript : MonoBehaviour
{
    public float speed = 3f;
    public int stopSpot;
    bool forward = true;
    bool isArrived = false;
    public GameObject bullet;
    public float timer = 4f;

    public string sfxShoot;

    // Update is called once per frame
    void Update()
    {
        if(forward)
        {
            float vel = speed * Time.deltaTime;

            transform.position = Vector3.MoveTowards(transform.position, new Vector3(0, Random.Range(-10, 11), 0), vel);
        }
        if(isArrived)
        {
            timer -= Time.deltaTime;
            if (timer <= 0)
            {
                Instantiate(bullet, transform.position, Quaternion.identity);
                RuntimeManager.PlayOneShot(sfxShoot);
                timer = 2f;
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Bullet")
        {
            GetComponent<EnemyHealth>().TakeDamage(50);

        }
        if (other.gameObject.name == "Green Stop")
        {
            forward = false;
            isArrived = true;
        }
    }
}
