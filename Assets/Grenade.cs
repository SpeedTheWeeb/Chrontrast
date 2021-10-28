using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grenade : MonoBehaviour
{
    public float speed = 5f;            // Travel Speed
    public float explosionDamage = 15f; // Core damage
    public float explosionRadius = 4f;  // radius range for explosion
    public float directHit = 5f;        // added damage from the projectile itself
    public Rigidbody2D r2d;             // Reference to Rigidbody2D
    
    void Start()
    {
        r2d.velocity = transform.up * speed;
    }

    private void OnTriggerEnter2D(Collider2D hitInfo)
    {
        if (explosionRadius > 0)
        {
            Collider2D[] splashEnemies = Physics2D.OverlapCircleAll(transform.position, explosionRadius);

            foreach(Collider2D enemy in splashEnemies)
            {
                if (enemy.CompareTag("Enemy"))
                {
                    Vector2 closestPoint = enemy.ClosestPoint(transform.position);
                    float distance = Vector3.Distance(closestPoint, transform.position);

                    float damagePercent = Mathf.InverseLerp(explosionRadius, 0, distance);
                    enemy.GetComponent<EnemyHealth>().TakeDamage(directHit + (explosionDamage * damagePercent));
                }
            }
        }
        
        // Needs a limited travel range to not outperform Sniper, and a check to see if enemy is hit by a Direct Hit

        Destroy(gameObject);
    }   
}