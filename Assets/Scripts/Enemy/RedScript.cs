using FMODUnity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedScript : MonoBehaviour
{
    public float speed;
    public float baseSpeed;
    public int stopSpot;
    bool forward = true;
    GameObject Crystal;
    CrystalHP crystalHP;
    bool isArrived = false;
    public bool isFrozen = false;
    float timer = 4f;
    public string sfxEnemyAttack;
    SpriteRenderer sprite;
    bool inRift;

    float ogPos;
    float lastY;

    bool goingUp;
    Rigidbody2D rb;
    // Start is called before the first frame update
    void Start()
    {
        ogPos = transform.position.y;
        rb = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
        Crystal = GameObject.Find("Crystal");
        crystalHP = (CrystalHP)Crystal.GetComponent("CrystalHP");
        speed = Random.Range(2, 10);
        baseSpeed = speed;
    }

    // Update is called once per frame
    void Update()
    {
        if (forward && !isFrozen && !inRift)
        {
            float vel = speed * Time.deltaTime;

            transform.position = Vector3.MoveTowards(transform.position, new Vector3(0, Random.Range(-10, 11), 0), vel);

            if (ogPos < lastY)
            {
                goingUp = true;
            }
            else
            {
                goingUp = false;
            }

            lastY = transform.position.y;
        }
        else if (!isFrozen && inRift && forward)
        {
            float vel = speed * Time.deltaTime;

            if (goingUp)
            {
                transform.position += new Vector3(0, Vector2.up.y, 0) * vel;
                //rb.velocity = vel * (Vector2.up * 100);
            }
            else
            {
                transform.position += new Vector3(0, Vector2.down.y, 0) * vel;
                //rb.velocity = vel * (Vector2.down * 100);
            }
        }
    }

    IEnumerator Attack()
    {
        while(isArrived)
        {
            crystalHP.HP(1);
            RuntimeManager.PlayOneShot(sfxEnemyAttack);
            yield return new WaitForSeconds(timer);
        }
    }

    public void Freeze()
    {
        isFrozen = true;
        sprite.color = new Color(152 / 255f, 208 / 255f, 250 / 255f);
        StopCoroutine(Attack());
        Invoke("Thaw", 5f);
    }

    void Thaw()
    {
        isFrozen = false;
        sprite.color = Color.white;
        StartCoroutine(Attack());
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Rift"))
        {
            inRift = true;
        }
        if (other.gameObject.name == "Red Stop")
        {
            forward = false;
            isArrived = true;
            inRift = false;
            StartCoroutine(Attack());
        }
        else if(other.CompareTag("Breakable"))
        {
            Wall wallScript = other.GetComponent<Wall>();
            int hits = wallScript.hitsTaken;
            if(hits / 100 < 0.33)
            {
                speed = speed * 0.75f;
            }
            else if(hits / 100 > 0.33 && hits / 100 < 0.66)
            {
                speed = speed * 0.5f;
            }
            else if (hits / 100 > 0.66)
            {
                speed = speed * 0.25f;
            }
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Breakable"))
        {
            speed = baseSpeed;
        }
    }
}
