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
    public bool isFrozen = false;
    public string sfxShoot;
    SpriteRenderer sprite;

    private void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
    }
    // Update is called once per frame
    void Update()
    {
        if(forward && !isFrozen)
        {
            float vel = speed * Time.deltaTime;

            transform.position = Vector3.MoveTowards(transform.position, new Vector3(0, Random.Range(-10, 11), 0), vel);
        }
    }

    public void Freeze()
    {
        isFrozen = true;
        sprite.color = new Color(152 / 255f, 208 / 255f, 250 / 255f);
        Invoke("Thaw", 5f);
    }

    void Thaw()
    {
        isFrozen = false;
        sprite.color = Color.white;
    }

    IEnumerator Attack()
    {
        while (isArrived && !isFrozen)
        {
            Instantiate(bullet, transform.position, Quaternion.identity);
            RuntimeManager.PlayOneShot(sfxShoot);

            yield return new WaitForSeconds(timer);
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
            isArrived = true;
            forward = false;
            StartCoroutine(Attack());
        }
    }
}
