using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    public bool alreadyHolding = false;    
    public int weaponType = 4;
    public int playerNumber = 2;

    public Transform meleeHurtbox;
    public float meleeRange = 1f;
    public float meleeDamage = 10f;
    public float meleeSpeed = 2f;       // Adjusts time before next attack is ready
    float nextMeleeAttackTime = 0f;
    // public Animator meleeAnim;

    private void Update()
    {
        switch(weaponType)
        {
            case 1:
                Debug.Log("I am holding a Melee Weapon!");
                if(Time.time >=nextMeleeAttackTime)
                {
                    if (Input.GetButtonDown("Fire" + playerNumber) && alreadyHolding)
                    {
                        MeleeAttack();
                        nextMeleeAttackTime = Time.time + 1f / meleeSpeed; // Counts how many seconds until next attack is ready
                    }
                }
                break;

            case 2:
                Debug.Log("I am holding a Splash Weapon!");
                /*if (Input.GetButtonDown("Fire" + playerNumber) && alreadyHolding)
                {
                    SplashAttack();
                }*/
                break;

            case 3:
                Debug.Log("I am holding a Sniper Weapon!");
                /*if (Input.GetButtonDown("Fire" + playerNumber) && alreadyHolding)
                {
                    SniperAttack();
                }*/
                break;

            default:
                Debug.Log("I am not holding any Weapon!");
                break;
        }
    }

    private void MeleeAttack()
    {
        Debug.Log("I am swinging my sword!");
        // play animation
        // Animator.SetTrigger("MeleeAttack");

        // Detects enemies in range
        Collider2D[] meleeEnemies = Physics2D.OverlapCircleAll(meleeHurtbox.position, meleeRange);        

        // Applies damage
        foreach(Collider2D enemy in meleeEnemies)
        {
            if(enemy.CompareTag("Enemy"))
            {                
                enemy.GetComponent<EnemyHealth>().TakeDamage(meleeDamage);
            }
        }
    }

    private void OnDrawGizmos()
    {
        if (meleeHurtbox == null)
            return;

        Gizmos.DrawWireSphere(meleeHurtbox.position, meleeRange);
    }
}
