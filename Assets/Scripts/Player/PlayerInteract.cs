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
    public Vector2 throwingDirection;

    private GameObject currentPlayer = null;
    public int playerNumber;
    public Vector2 direction;
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
                    direction = Vector2.left * 100;
                    throwingDirection = Vector2.left;
                }
                else if (Input.GetKeyDown(KeyCode.W))
                {
                    direction = Vector2.up * 100;
                    throwingDirection = Vector2.up;
                }
                else if(Input.GetKeyDown(KeyCode.D))
                {
                    direction = Vector2.right * 100;
                    throwingDirection = Vector2.right;
                }
                else if (Input.GetKeyDown(KeyCode.S))
                {
                    direction = Vector2.down * 100;
                    throwingDirection = Vector2.down;
                }
                break;
            case 2:
                if (Input.GetKeyDown(KeyCode.LeftArrow))
                {
                    direction = Vector2.left*100;
                    throwingDirection = Vector2.left;
                }
                else if (Input.GetKeyDown(KeyCode.UpArrow))
                {
                    direction = Vector2.up*100;
                    throwingDirection = Vector2.up;
                }
                else if (Input.GetKeyDown(KeyCode.RightArrow))
                {
                    direction = Vector2.right*100;
                    throwingDirection = Vector2.right;
                }
                else if (Input.GetKeyDown(KeyCode.DownArrow))
                {
                    direction = Vector2.down*100;
                    throwingDirection = Vector2.down;
                }
                break;

        }

        if (Input.GetButtonDown("Fire" + playerNumber) && isHolding)
        {
            Fire();
        }

        if (Input.GetButtonDown("Drop" + playerNumber) && (trigger || isHolding))
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

    //Pick up object
    //Is to be changed to delete the item and change a variable in a weapon_manager script to a number that corrosponds with the weapon type
    private void PickUp(Collider2D col)
    {
        //gameObject.GetComponent<WeaponManager>.weaponType = col.GetComponent<ItemBehavior>().weaponType;
        //Destroy(col);
        if(col.CompareTag("Objective"))
        {
            col.transform.parent = currentPlayer.transform;
            pickupObject = col.gameObject;
            Item = col.GetComponent<ItemBehavior>();
            Item.Init(bulletSpawn);
            
            isHolding = true;
        }
    }

    //Is to be changed to look at the variable in the weapon_manager script that determines what weapon is being held and then instansiate
    //the weapon with a velocity that is apropriate
    private void Drop()
    {
        Item.Throw();
        Item.dirInit(throwingDirection);
        pickupObject.transform.parent = null;
        isHolding = false;
    }

    private void Fire()
    {
        GameObject clone = Instantiate(bullet, transform.position, Quaternion.identity);
        clone.GetComponent<Rigidbody2D>().AddForce(direction * bulletSpeed);

        Destroy(clone.gameObject, destroyBullet);
    }
}
