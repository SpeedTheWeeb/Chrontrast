using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public GameObject Bullet;
    public int PlayerNumber = 1;
   
    public Vector2 speed = new Vector2(10, 10);
    Rigidbody2D r2d;
    private GameObject currentPlayer = null;
    private float xAxis;
    private float yAxis;
    private bool isHolding = false;
    // Start is called before the first frame update
    void Start()
    {
        r2d = GetComponent<Rigidbody2D>();
        currentPlayer = GameObject.Find("P" + PlayerNumber);
    }

    // Update is called once per frame
    void Update()
    {
        xAxis = Input.GetAxis("HorizontalP" + PlayerNumber);
        yAxis = Input.GetAxis("VerticalP" + PlayerNumber);

        if(Input.GetButtonDown("Fire" + PlayerNumber))
        {
            Fire();
        }

        if(Input.GetButtonDown("Drop" + PlayerNumber))
        {
            if(isHolding)
            {
                Drop();
            }
            else
            {
                Interact();
            }
        }
    }
    
    private void FixedUpdate()
    {
        Vector2 mov = new Vector2(speed.x * xAxis, speed.y * yAxis);

        mov *= Time.deltaTime;

        transform.Translate(mov);
    }
    private void Interact()
    {

    }
    private void Drop()
    {

    }

    private void Fire()
    {
        Instantiate(Bullet, currentPlayer.transform.position, Quaternion.identity);
    }
}
