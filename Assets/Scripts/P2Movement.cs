using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class P2Movement : MonoBehaviour
{
    public Vector2 speed = new Vector2(10, 10);
    Rigidbody2D r2d;
    private float xAxis;
    private float yAxis;
    // Start is called before the first frame update
    void Start()
    {
        r2d = GetComponent<Rigidbody2D>();

    }

    // Update is called once per frame
    void Update()
    {
        xAxis = Input.GetAxis("HorizontalP2");
        yAxis = Input.GetAxis("VerticalP2");
    }

    private void FixedUpdate()
    {
        Vector2 mov = new Vector2(speed.x * xAxis, speed.y * yAxis);

        mov *= Time.deltaTime;

        transform.Translate(mov);
    }
}
