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
    Color ogColor;
    public SpriteRenderer render;
    private void Start()
    {
        currentHealth = maxHealth;
        render = GetComponent<SpriteRenderer>();
        ogColor = render.color;
    }

    public void TakeDamage(float damage)
    {
        
        currentHealth -= damage;
        Flash();
        if (currentHealth <= 0)
            Die();
    }

    void Flash()
    {
        Color col = new Color(255/255f, 125/255f, 125/255f);
        render.color = col;
        Invoke("ResetFlash", 0.3f);
    }
    private void ResetFlash()
    {
        render.color = ogColor;
    }

    void Die()
    {
        Debug.Log("Enemy died!");

        Destroy(gameObject);
    }

}
