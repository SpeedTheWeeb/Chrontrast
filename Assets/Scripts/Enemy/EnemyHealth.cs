using FMODUnity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealth : MonoBehaviour
{
    public float maxHealth = 100f;
    Text text;
    SpawningScript Spawning;
    public float currentHealth;
    int modifier = 0;
    Color ogColor;
    public SpriteRenderer render;
    public string sfxEnemyDeath;
    public string sfxEnemyHurt;

    private void Start()
    {
        GameObject crystal = GameObject.Find("Crystal");
        SpawningScript wave = (SpawningScript)crystal.GetComponent("SpawningScript");
        //if (wave.currentWave % 2 == 0)
        //{
        //    modifier += 10;
        //}
        //currentHealth = maxHealth + ((modifier/maxHealth));
        render = GetComponent<SpriteRenderer>();
        ogColor = render.color;
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        if(currentHealth > 1f)
        {
            RuntimeManager.PlayOneShot(sfxEnemyHurt); // issue with playing along with the Die() method
        }
        else if (currentHealth <= 0)
            Die();
        Flash();
        Debug.Log("Was hit for " + damage + " damage");
    }

    void Flash()
    {
        Color col = new Color(255/255f, 125/255f, 125/255f);
        render.color = col;
        StartCoroutine(ResetFlash());
    }
    IEnumerator ResetFlash()
    {
        float i = 0.4f;
        while(i < 1.1)
        {
            render.color = new Color(1,i,i);
            i += 0.2f;
            yield return new WaitForSeconds(0.1f);
        }
    }

    //private void ResetFlash()
    //{
    //    render.color = ogColor;
    //}

    void Die()
    {
        Debug.Log("Enemy died!");
        RuntimeManager.PlayOneShot(sfxEnemyDeath);
        Destroy(gameObject);
    }
    
}
