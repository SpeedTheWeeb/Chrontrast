using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteract : MonoBehaviour
{
    //Pickup and Drop
    private bool isHolding = false;
    public bool trigger = false;
    private Collider2D collidingObject = null;
    private GameObject pickupObject;
    public float dropSpeed = 20f;
    public ItemBehavior Item;

    //Bullet Data
    public GameObject bullet;
    public float bulletSpeed = 20f;
    public GameObject bulletSpawn;
    private float destroyBullet = 1f;

    private GameObject currentPlayer = null;
    public int playerNumber;
    // Start is called before the first frame update
    void Start()
    {
        currentPlayer = GameObject.Find("P" + playerNumber);
    }

    // Update is called once per frame
    void Update()
    {
        switch (playerNumber)
        {
            case 1:
                if (Input.GetKeyDown(KeyCode.A))
                {
                    bulletSpawn.transform.eulerAngles = new Vector3(0, 90, 0);
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

        if (Input.GetButtonDown("Fire" + playerNumber) && isHolding)
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
