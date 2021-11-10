using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class CrystalHP : MonoBehaviour
{
    public Text TextUI;
    public int crystalhealth;
    public CameraShake cameraShake;
    private void Start()
    {
        crystalhealth = 100;
       
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Projectile")) //Skal �ndre til entity hvis vi v�lger dette system
        {
            HP(1);
            Destroy(collision.gameObject);
        }
        if (collision.gameObject.CompareTag("Bullet"))
        {
            Destroy(collision.gameObject);
        }
    }
    public void HP(int damage)
    {
            crystalhealth = crystalhealth - damage;
            TextUI.text = "Crystal HP: " + crystalhealth;
            StartCoroutine(cameraShake.Shake(.15f, .2f));
            
            if (crystalhealth == 0)
            {
                FindObjectOfType<Gamemanager>().Endgame();
            }
    }
}
