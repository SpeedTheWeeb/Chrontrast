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
        if (collision.gameObject.CompareTag("Projectile")) //Skal ændre til entity hvis vi vælger dette system
        {
            crystalhealth = crystalhealth - 1;
            TextUI.text = "Crystal HP: " + crystalhealth;
            Destroy(collision.gameObject);
            StartCoroutine(cameraShake.Shake(.15f, .2f));
        }
        if (collision.gameObject.CompareTag("Bullet"))
        {
            Destroy(collision.gameObject);
        }
    }

}
