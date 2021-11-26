using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grenade : MonoBehaviour
{
    public LayerMask enemyMask;
    public float speed = 7.5f;           // Travel Speed
    public float maxDamage = 15f;       // Core damage
    public float detonationTimer = 1f;  // seconds before grenade explodes without direct hit
    public float explosionRadius = 4f;  // radius for explosion range
    public float directHit = 5f;        // added damage from the projectile itself
    public Rigidbody2D r2d;             // Reference to Rigidbody2D
    float timer = .75f;

    private void Update()
    {
        timer -= Time.deltaTime;
        if (timer <= 0f)
        {
            Explosive();
            Destroy(gameObject);
        }

    }

    public void Move(Vector2 dir)
    {
        r2d.velocity = dir * speed;
    }

    public void Explosive()
    {
        Collider2D[] splashEnemies = Physics2D.OverlapCircleAll(transform.position, explosionRadius, enemyMask);

        foreach (Collider2D enemy in splashEnemies)
        {
            if (enemy.CompareTag("Enemy"))
            {
                Vector2 closestPoint = enemy.ClosestPoint(transform.position);
                float distance = Vector3.Distance(closestPoint, transform.position);

                float damagePercent = Mathf.InverseLerp(explosionRadius, 0, distance);
                enemy.GetComponent<EnemyHealth>().TakeDamage(maxDamage * damagePercent);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D hitInfo)
    {
        if(hitInfo.CompareTag("Enemy"))
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