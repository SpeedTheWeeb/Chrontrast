using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    //Player Data
    public int playerNumber = 1;
    private GameObject currentPlayer = null;

    public Vector2 speed = new Vector2(10, 10);
    Rigidbody2D r2d;
    private float xAxis;
    private float yAxis;

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
    }
    
    private void FixedUpdate()
    {
        Vector2 mov = new Vector2(speed.x * xAxis, speed.y * yAxis);

        mov *= Time.deltaTime;

        transform.Translate(mov);
    }
    
}
