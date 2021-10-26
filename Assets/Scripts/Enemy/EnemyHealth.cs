using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealth : MonoBehaviour
{
    public float maxHealth = 100f;
    Text text;
    SpawningScript Spawning;
    float currentHealth;

    private void Start()
    {
        currentHealth = maxHealth;
        GameObject crystal = GameObject.Find("Crystal");
        Spawning = (SpawningScript)crystal.GetComponent("SpawningScript");
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
            Invoke("StartNextWave", 10f);
        }
    }

    void StartNextWave()
    {

        int currentWave = Spawning.currentWave;
        Spawning.InitWave(currentWave + 1);
    }

}
