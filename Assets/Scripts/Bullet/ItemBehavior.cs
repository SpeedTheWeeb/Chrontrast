using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    private Sprite Sprite_future_melee;
    private Sprite Sprite_medieval_melee;
    private Sprite Sprite_future_medium;
    private Sprite Sprite_medieval_medium;
    private Sprite Sprite_future_long;
    private Sprite Sprite_medieval_long;
    //Assets/Resources/Sprites/Weapons/Weapon_Future_MediumRange.png
    //to get the throwing direction i need to access the script of the parent og the game object that has this script
    //but i only want to get that after this game object has been picked up otherwise there is not parent.

    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        Sprite_future_melee = Resources.Load<Sprite>("Sprites/Weapons/Weapon_Future_Melee");
        Sprite_medieval_melee = Resources.Load<Sprite>("Sprites/Weapons/swordMed");
        Sprite_future_medium = Resources.Load<Sprite>("Sprites/Weapons/Weapon_Future_MediumRange");
        Sprite_medieval_medium = Resources.Load<Sprite>("Sprites/Weapons/wandMed");
        Sprite_future_long = Resources.Load<Sprite>("Sprites/Weapons/Weapon_Future_LongRange");
        Sprite_medieval_long = Resources.Load<Sprite>("Sprites/Weapons/bowMed");
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
                    spriteRenderer.sprite = Sprite_medieval_melee;
                }
                if (weapon_type == "MR")
                {
                    spriteRenderer.sprite = Sprite_medieval_medium;
                }
                if (weapon_type == "LR")
                {
                    spriteRenderer.sprite = Sprite_medieval_long;
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
                    spriteRenderer.sprite = Sprite_future_melee;
                }
                if (weapon_type == "MR")
                {
                    spriteRenderer.sprite = Sprite_future_medium;
                }
                if (weapon_type == "LR")
                {
                    spriteRenderer.sprite = Sprite_future_long;
                }
                isMedieval = true;
            }

        }
    }
}
