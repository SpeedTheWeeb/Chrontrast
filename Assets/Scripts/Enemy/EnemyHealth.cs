using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public float maxHealth = 100f;
    
    float currentHealth;

    private void Start()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;

        if (currentHealth <= 0)
            Die();
    }

    void Die()
    {
        Debug.Log("Enemy died!");

        // animator.SetBool("IsDead", true)

        Destroy(gameObject);

        //Check enemy amount after every kill, when there are no more left, run code
        if(GameObject.FindGameObjectsWithTag("Enemy").Length <= 1)
        {
            Debug.Log("No More Enemies");
        }
    }
}
