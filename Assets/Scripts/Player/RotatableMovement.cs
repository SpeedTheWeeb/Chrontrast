using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotatableMovement : MonoBehaviour
{
    public int playerNumber = 1;
    private GameObject currentPlayer = null;
    Rigidbody2D rb2d;

    private float horizontalInput = 0;
    private float verticalInput = 0;
    public float speedMultiplier = 10;
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        currentPlayer = GameObject.Find("P" + playerNumber);
    }

    void Update()
    {
        GetPlayerInput();        
    }

    private void FixedUpdate()
    {
        MovePlayer();
        if(horizontalInput != 0 || verticalInput != 0)
            RotatePlayer();
    }

    private void GetPlayerInput()
    {
        horizontalInput = Input.GetAxisRaw("HorizontalP" + playerNumber);
        verticalInput = Input.GetAxisRaw("VerticalP" + playerNumber);
    }

    private void MovePlayer()
    {
        Vector3 directionVector = new Vector3(horizontalInput, verticalInput, 0);
        rb2d.velocity = directionVector.normalized * speedMultiplier;
    }

    void RotatePlayer()
    {
        float angle = Mathf.Atan2(verticalInput, horizontalInput) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }
}
