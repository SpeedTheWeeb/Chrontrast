using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ItemBehavior : MonoBehaviour
{
    public GameObject bulletSpawn;
    public float speed = 0.1f;
    public float timer = 0.7f;
    private bool isThrown = false;
    //public Vector2 throwingDirection;
    public bool isMedieval;
    public string weapon_type;
    public Vector2 Direction;
    private SpriteRenderer spriteRenderer;
    private Sprite CyberswordChip;
    private Sprite FrostTome;
    private Sprite RocketChip;
    private Sprite FireTome;
    private Sprite RailgunChip;
    private Sprite LightningTome;
    public static int tradesMade;
    //Assets/Resources/Sprites/Weapons/Weapon_Future_MediumRange.png
    //to get the throwing direction i need to access the script of the parent og the game object that has this script
    //but i only want to get that after this game object has been picked up otherwise there is not parent.

    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        CyberswordChip = Resources.Load<Sprite>("FloorWeapons/WeaponChipCybersword");
        FrostTome = Resources.Load<Sprite>("FloorWeapons/FrostTome");
        RocketChip = Resources.Load<Sprite>("FloorWeapons/WeaponChipRockets");
        FireTome = Resources.Load<Sprite>("FloorWeapons/FireTome");
        RailgunChip = Resources.Load<Sprite>("FloorWeapons/WeaponChipRailgun");
        LightningTome = Resources.Load<Sprite>("FloorWeapons/LightningTome");
    }
    
    public void Init(GameObject spawn)
    {
        bulletSpawn = spawn;
    }
    public void dirInit(Vector2 dir)
    {
        //PlayerInteract getDirection = Player.GetComponent<PlayerInteract>();
        Direction = dir;
    }
    // Sets 
    public void Throw()
    {
        isThrown = true;
    }

    // Update is called once per frame
    void Update()
    {
        //GetComponent<Rigidbody2D>().AddForce(Vector2.right * speed);

        // If the Throw funtion gets activated the item will move to the right untill a timer runs out
        if(isThrown)
        {
            //GetComponent<Rigidbody2D>().AddForce(Direction * speed);
            transform.Translate(Direction * speed * Time.deltaTime);

            //Stops the moving after a while
            Invoke("throwMove", timer);
        }
    }

    void throwMove()
    {
        isThrown = false;
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Wall")
        {
            Debug.Log("Stop");
            isThrown = false;
        }

        if (col.gameObject.CompareTag("Rift"))
        {
            //and if it is not medieval it must be future side
            if (!isMedieval)
            {
                // if it exits the rift after having been on the future side, it must now be on the medieval side..
                // and isMedieval is changed, and so is the color
                if (weapon_type == "CR")
                {
                    spriteRenderer.sprite = FrostTome;
                }
                if (weapon_type == "MR")
                {
                    spriteRenderer.sprite = FireTome;
                }
                if (weapon_type == "LR")
                {
                    spriteRenderer.sprite = LightningTome;
                }
                isMedieval = false;
            }
            // if it is medieval
            else
            {
                // if it exits the rift after having been on the medieval side, it must now be on the future side..
                // and isMedieval is changed, and so is the color
                if (weapon_type == "CR")
                {
                    spriteRenderer.sprite = CyberswordChip;
                }
                if (weapon_type == "MR")
                {
                    spriteRenderer.sprite = RocketChip;
                }
                if (weapon_type == "LR")
                {
                    spriteRenderer.sprite = RailgunChip;
                }
                isMedieval = true;
            }

            tradesMade++;

        }
    }
}
