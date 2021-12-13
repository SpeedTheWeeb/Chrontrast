using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMOD.Studio;
using FMODUnity;

public class Grenade : MonoBehaviour
{
    public LayerMask enemyMask;
    public float speed = 7.5f;              // Travel Speed
    public float maxDamage = 15f;           // Core damage
    public float detonationTimer = .75f;    // seconds before grenade explodes without direct hit
    public float explosionRadius = 4f;      // radius for explosion range
    public float directHit = 5f;            // added damage from the projectile itself (unused)
    public Rigidbody2D r2d;                 // Reference to Rigidbody2D
    public float dmgMod = 0;
    int player;
    public GameObject explosion;

    private void Update()
    {
        

    }

    void Detonate()
    {
        Explosive();
        Destroy(gameObject);
    }

    public void Move(Vector2 dir, int currentPlayer)
    {
        r2d.velocity = dir * speed;
        Invoke("Detonate", 0.5f);
        player = currentPlayer;
    }
    public void Explosive()
    {           
        Collider2D[] splashEnemies = Physics2D.OverlapCircleAll(transform.position, explosionRadius, enemyMask);
        Instantiate(explosion, transform.position, transform.rotation);
        foreach (Collider2D enemy in splashEnemies)
        {
            if (enemy.CompareTag("Enemy"))
            {
                Vector2 closestPoint = enemy.ClosestPoint(transform.position);
                float distance = Vector3.Distance(closestPoint, transform.position);

                float damagePercent = Mathf.InverseLerp(explosionRadius, 0, distance);
                if (player == 1)
                {
                    if (enemy.name.Contains("Med"))
                    {
                        enemy.GetComponent<EnemyHealth>().TakeDamage((maxDamage * damagePercent) + ((dmgMod / 100) * (maxDamage * damagePercent)));
                    }
                }
                else if (player == 2)
                {
                    if (enemy.name.Contains("Fan"))
                    {
                        enemy.GetComponent<EnemyHealth>().TakeDamage((maxDamage * damagePercent) + ((dmgMod / 100) * (maxDamage * damagePercent)));
                    }
                }
            }            
        }
    }

    private void OnTriggerEnter2D(Collider2D hitInfo)
    {
        switch (gameObject.name)
        {
            case "Rocket":
                RuntimeManager.PlayOneShot("event:/sfx/player/future/explosive/explode");
                break;

            case "Fireball":
                RuntimeManager.PlayOneShot("event:/sfx/player/past/explosive/explode");
                break;
        }
        if (hitInfo.CompareTag("Enemy"))
        {
            if (explosionRadius > 0)
            {            
                Explosive();
            }
            Destroy(gameObject);
        }
        // Needs a limited travel range to not outperform Sniper, and a check to see if enemy is hit by a Direct Hit        
    }   
}