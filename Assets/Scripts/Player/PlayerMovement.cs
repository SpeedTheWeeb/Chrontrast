using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    //Bullet Data
    public GameObject bullet;
    public float bulletSpeed = 20f;
    public GameObject bulletSpawn;
    private float destroyBullet = 1f;

    //Player Data
    public int playerNumber = 1;
    private GameObject currentPlayer = null;

    public Vector2 speed = new Vector2(10, 10);
    Rigidbody2D r2d;
    private float xAxis;
    private float yAxis;

    //Pickup and Drop
    private bool isHolding = false;
    public bool trigger = false;
    private Collider2D collidingObject = null;
    private GameObject pickupObject;
    public float dropSpeed = 20f;
    public ItemBehavior Item;

    // Start is called before the first frame update
    void Start()
    {
        r2d = GetComponent<Rigidbody2D>();
        currentPlayer = GameObject.Find("P" + playerNumber);
        
    }

    // Update is called once per frame
    void Update()
    {
        xAxis = Input.GetAxis("HorizontalP" + playerNumber);
        yAxis = Input.GetAxis("VerticalP" + playerNumber);

        switch(playerNumber)
        {
            case 1:
                if(Input.GetKeyDown(KeyCode.A))
                {
                    bulletSpawn.transform.eulerAngles = new Vector3(0,90,0);
                }
                if (Input.GetKeyDown(KeyCode.W))
                {
                    bulletSpawn.transform.eulerAngles = new Vector3(90, 90, 0);
                }
                if (Input.GetKeyDown(KeyCode.D))
                {
                    bulletSpawn.transform.eulerAngles = new Vector3(0, 0, 0);
                }
                if (Input.GetKeyDown(KeyCode.S))
                {
                    bulletSpawn.transform.eulerAngles = new Vector3(-90, 90, 0);
                }
                break;
            case 2:
                if (Input.GetKeyDown(KeyCode.LeftArrow))
                {
                    bulletSpawn.transform.eulerAngles = new Vector3(0, 90, 0);
                }
                if (Input.GetKeyDown(KeyCode.UpArrow))
                {
                    bulletSpawn.transform.eulerAngles = new Vector3(90, 90, 0);
                }
                if (Input.GetKeyDown(KeyCode.RightArrow))
                {
                    bulletSpawn.transform.eulerAngles = new Vector3(0, 0, 0);
                }
                if (Input.GetKeyDown(KeyCode.DownArrow))
                {
                    bulletSpawn.transform.eulerAngles = new Vector3(-90, 90, 0);
                }
                break;
        }

        if(Input.GetButtonDown("Fire" + playerNumber) && isHolding)
        {
            Fire();
        }

        if (Input.GetButtonDown("Drop" + playerNumber) && trigger)
        {
            if (isHolding)
            {
                Drop();
            }
            else
            {
                PickUp(collidingObject);
            }
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        //if(pickupObject != null)
        //{
        //    pickupObject.transform.parent = null;
        //}

        trigger = false;
        collidingObject = null;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        trigger = true;
        collidingObject = collision;

    }
    private void FixedUpdate()
    {
        Vector2 mov = new Vector2(speed.x * xAxis, speed.y * yAxis);

        mov *= Time.deltaTime;

        transform.Translate(mov);
    }
    private void PickUp(Collider2D col)
    {
        col.transform.parent = currentPlayer.transform;
        pickupObject = col.gameObject;
        Item = col.GetComponent<ItemBehavior>();
        Item.Init(bulletSpawn);
        isHolding = true;
    }
    private void Drop()
    {
        Item.Throw();
        pickupObject.transform.parent = null;
        isHolding = false;
    }

    private void Fire()
    {
        GameObject clone = Instantiate(bullet, bulletSpawn.transform.position, bulletSpawn.transform.rotation);
        clone.GetComponent<Rigidbody>().velocity = bulletSpawn.transform.right * bulletSpeed;
        clone.GetComponent<Rigidbody>().AddForce(Vector2.right * bulletSpeed);

        Destroy(clone.gameObject, destroyBullet);
    }
}
