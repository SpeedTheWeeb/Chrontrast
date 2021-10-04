using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    //Bullet Data
    public Rigidbody2D bullet;
    public float bulletSpeed = 20f;
    public GameObject bulletSpawn;

    //Player Data
    public int playerNumber = 1;
    private GameObject currentPlayer = null;

    public Vector2 speed = new Vector2(10, 10);
    Rigidbody2D r2d;
    private float xAxis;
    private float yAxis;
    private bool isHolding = false;

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

                }
                break;
            case 2:
                break;
        }

        if(Input.GetButtonDown("Fire" + playerNumber))
        {
            Fire();
        }

        if(Input.GetButtonDown("Drop" + playerNumber))
        {
            if(isHolding)
            {
                Drop();
            }
            else
            {
                PickUp();
            }
        }
    }
    
    private void FixedUpdate()
    {
        Vector2 mov = new Vector2(speed.x * xAxis, speed.y * yAxis);

        mov *= Time.deltaTime;

        transform.Translate(mov);
    }
    private void PickUp()
    {
        isHolding = true;
    }
    private void Drop()
    {
        isHolding = false;
    }

    private void Fire()
    {
        Rigidbody2D clone = Instantiate(bullet, transform.position, Quaternion.identity);
        clone.velocity = transform.right * bulletSpeed;
        clone.AddForce(Vector2.right * bulletSpeed);
        //clone.transform.Translate(clone.velocity * Time.deltaTime);
        //clone.GetComponent<Rigidbody2D>().AddForce(clone.transform.forward * bulletSpeed);
    }
}
