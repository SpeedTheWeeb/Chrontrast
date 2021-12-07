using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotatableMovement : MonoBehaviour
{
    public GameObject test;
    public int playerNumber = 1;
    public float speedMultiplier = 10;
    

    GameObject currentPlayer = null;
    Rigidbody2D rb2d;
    float horizontalInput = 0;
    float verticalInput = 0;
    public Animator animator;
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        currentPlayer = GameObject.Find("Player " + playerNumber);
        animator = GetComponent<Animator>();
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

        //ANIMATION HERUNDER:
        animator.SetFloat("VelocityV", verticalInput); 
        animator.SetFloat("VelocityH", horizontalInput);

        //SKRÅ venstre
        if ((horizontalInput < 0 && verticalInput < 0)||(horizontalInput < 0 && verticalInput >0)) {
            animator.SetBool("strayleft", true);
        }
        else { animator.SetBool("strayleft", false); }

        //SKRÅ højre
        if ((horizontalInput > 0 && verticalInput < 0) || (horizontalInput > 0 && verticalInput > 0))
        {
            animator.SetBool("StrayRight", true);
        }
        else { animator.SetBool("StrayRight", false); }

        //IDLE
        if (horizontalInput == 0 && verticalInput == 0) {
            animator.SetBool("isidle", true);
        }
        else { animator.SetBool("isidle", false); }
        
    }

    private void MovePlayer()
    {
        Vector3 directionVector = new Vector3(horizontalInput, verticalInput, 0);
        rb2d.velocity = directionVector.normalized * speedMultiplier;        
    }

    void RotatePlayer()
    {
        float angle = Mathf.Atan2(horizontalInput, verticalInput) * Mathf.Rad2Deg;
        test.transform.rotation = Quaternion.AngleAxis(angle*-1, Vector3.forward);
    }
}
