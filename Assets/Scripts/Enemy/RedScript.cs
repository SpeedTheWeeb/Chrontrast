using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedScript : MonoBehaviour
{
    public float speed;
    float baseSpeed;
    public int stopSpot;
    bool forward = true;
    GameObject Crystal;
    CrystalHP crystalHP;
    bool isArrived = false;
    float timer = 4f;
    // Start is called before the first frame update
    void Start()
    {
        Crystal = GameObject.Find("Crystal");
        crystalHP = (CrystalHP)Crystal.GetComponent("CrystalHP");
        speed = Random.Range(2, 10);
        baseSpeed = speed;
    }

    // Update is called once per frame
    void Update()
    {
        if (forward)
        {
            float vel = speed * Time.deltaTime;

            transform.position = Vector3.MoveTowards(transform.position, new Vector3(0, Random.Range(-10, 11), 0), vel);
        }
        if(isArrived)
        {
            
            timer -= Time.deltaTime;
            if (timer <= 0)
            {
                crystalHP.HP(1);
                timer = 2f;
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        
        if (other.gameObject.name == "Red Stop")
        {
            forward = false;
            isArrived = true;
        }
        else if(other.gameObject.tag == "Breakable")
        {
            Wall wallScript = (Wall)other.GetComponent("Wall");
            int hits = wallScript.hitsTaken;
            if(hits < 10)
            {
                speed = speed * 0.75f;
            }
            else if(hits < 20 && hits > 10)
            {
                speed = speed * 0.5f;
            }
            else if (hits > 20)
            {
                speed = speed * 0.25f;
            }
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag == "Breakable")
        {
            speed = baseSpeed;
        }
    }
}
