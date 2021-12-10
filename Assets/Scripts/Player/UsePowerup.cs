using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class UsePowerup : MonoBehaviour
{
    public Transform meleeBox;
    public GameObject Glue;
    public int playerNumber;
    WeaponManager info;
    public Image freezeImg;
    // Start is called before the first frame update
    void Start()
    {
        info = gameObject.GetComponent<WeaponManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetButtonDown("PU"+playerNumber))
        {
            Use();
        }
    }

    void Use()
    {
        
        if(info.havePowerup)
        {
            string pu = info.powerup;
            switch(pu.Split('_')[0])
            {
                case "Repair":
                    RepairWall();
                    break;
                case "Glue":
                    SpawnGlue();
                    break;
                case "Freeze":
                    Freeze();
                    break;
            }
            
            info.powerup = null;
            info.havePowerup = false;
        }

        GameObject[] Pu = GameObject.FindGameObjectsWithTag("PowerUp");

        foreach(GameObject p in Pu)
        {
            if(p.name.Contains(info.playerNumber.ToString()))
            {
                Destroy(p);
            }
        }
    }

    void Freeze()
    {
        StartCoroutine(FadeIn());

        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        foreach(GameObject enemy in enemies)
        {
            if(enemy.name.Contains("Blue"))
            {
                BlueScript blue = enemy.GetComponent<BlueScript>();
                blue.Freeze();
            }
            else if(enemy.name.Contains("Green"))
            {
                GreenScript green = enemy.GetComponent<GreenScript>();
                green.Freeze();
            }
            else if(enemy.name.Contains("Red"))
            {
                RedScript red = enemy.GetComponent<RedScript>();
                red.Freeze();
            }                
        }
        Invoke("StopFreeze", 3f);
    }

    void StopFreeze()
    {
        StartCoroutine(FadeOut());
    }

    IEnumerator FadeIn()
    {
        var color = freezeImg.color;
        color.a = 0.5f;
        freezeImg.color = color;
        yield return null;
    }

    IEnumerator FadeOut()
    {
        var color = freezeImg.color;
        float i = 0.5f;
        while (i > 0)
        {
            color.a = i;
            freezeImg.color = color;
            i -= 0.1f;
            yield return new WaitForSeconds(0.1f);
        }
    }

    void SpawnGlue()
    {
        Instantiate(Glue, transform.position, Quaternion.identity);
    }

    void RepairWall()
    {
        Collider2D[] wall = Physics2D.OverlapCircleAll(meleeBox.transform.position, 2);

        if(wall[0].CompareTag("Breakable"))
        {
            Wall w = wall[0].GetComponent<Wall>();
            w.hitsTaken -= 3;
        }
    }
}
