using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBulletScript : MonoBehaviour
{
    public float speed;
    // Start is called before the first frame update
    void Start()
    {
        speed = 20f;
    }

    // Update is called once per frame
    void Update()
    {
        float vel = speed * Time.deltaTime;

        transform.position = Vector3.MoveTowards(transform.position, new Vector3(0, 0, 0), vel);
    }
}
