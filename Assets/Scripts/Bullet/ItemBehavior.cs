using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemBehavior : MonoBehaviour
{
    public GameObject bulletSpawn;
    public float speed = 20f;
    public float timer = 1f;
    private bool isThrown = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    
    public void Init(GameObject spawn)
    {
        bulletSpawn = spawn;
    }

    public void Throw()
    {
        isThrown = true;
        //transform.rotation = bulletSpawn.transform.rotation;
    }

    // Update is called once per frame
    void Update()
    {
        //GetComponent<Rigidbody2D>().AddForce(Vector2.right * speed);
        if(isThrown)
        {
            transform.Translate(bulletSpawn.transform.right * speed * Time.deltaTime);
            timer -= Time.deltaTime;
            if(timer <= 0f)
            {
                isThrown = false;
                timer = 1f;
            }
        }
    }
}
