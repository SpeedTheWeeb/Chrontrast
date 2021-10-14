using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemBehavior : MonoBehaviour
{
    public GameObject bulletSpawn;
    public float speed = 20f;
    public float timer = 1f;
    private bool isThrown = false;
    public Vector2 throwingDirection;
    public bool isMedieval;

    SpriteRenderer spriteRenderer;

    //to get the throwing direction i need to access the script of the parent og the game object that has this script
    //but i only want to get that after this game object has been picked up otherwise there is not parent.

    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
    }
    
    public void Init(GameObject spawn)
    {
        bulletSpawn = spawn;
    }

    // Sets 
    public void Throw()
    {
        isThrown = true;
        //transform.rotation = bulletSpawn.transform.rotation;
    }

    // Update is called once per frame
    void Update()
    {
        //GetComponent<Rigidbody2D>().AddForce(Vector2.right * speed);
        // If the Throw funtion gets activated the item will move to the right untill a timer runs out
        if(isThrown)
        {
            transform.Translate(bulletSpawn.transform.right * speed * Time.deltaTime);
            timer -= Time.deltaTime;
            if(timer <= 0f)
            {
                //When the timer has ran out it resets itself and the condition allowing the item to move is set to false
                isThrown = false;
                timer = 1f;
            }
        }


    }

    //If the item exits a 2d collider...
    void OnTriggerExit2D(Collider2D other)
    {
        //and that 2d collider is tagged with "Rift"
        if (other.gameObject.CompareTag("Rift"))
        {
            //and if it is not medieval it must be future side
            if (isMedieval == false)
            {
                // if it exits the rift after having been on the future side, it must now be on the medieval side..
                // and isMedieval is changed, and so is the color
                spriteRenderer.color = Color.blue;
                isMedieval = !isMedieval;
            }
            // if it is medieval
            else
            {
                // if it exits the rift after having been on the medieval side, it must now be on the future side..
                // and isMedieval is changed, and so is the color
                spriteRenderer.color = Color.green;
                isMedieval = !isMedieval;
            }

        }
    }
}
