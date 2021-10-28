using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    public bool alreadyHolding = false;    
    public int weaponType = 0;
    public int playerNumber = 2;
    Vector2 direction;
    public Vector2 throwingDirection;
    public Transform meleeHurtbox;
    public float meleeRange = 1f;
    public float meleeDamage = 10f;
    public float meleeSpeed = 2f;       // Adjusts time before next attack is ready
    float nextMeleeAttackTime = 0f;
    // public Animator meleeAnim;
    public GameObject holdingWeapon;
    // public Transform splashSourSpot;
    public Transform splashSweetSpot;
    public float sweetSpotRadius = 1f;
    // public float sourSpotDamage = 5f;
    public float sweetSpotDamage = 15f;
    public float splashSpeed = 2f;       // Adjusts time before next attack is ready
    float nextSplashAttackTime = 0f;
    // public Animator splashAnim;

    public GameObject meleePrefab;
    public GameObject shotgubPrefab;
    ItemBehavior item;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            direction = Vector2.left * 100;
            throwingDirection = Vector2.left;
        }
        else if (Input.GetKeyDown(KeyCode.W))
        {
            direction = Vector2.up * 100;
            throwingDirection = Vector2.up;
        }
        else if (Input.GetKeyDown(KeyCode.D))
        {
            direction = Vector2.right * 100;
            throwingDirection = Vector2.right;
        }
        else if (Input.GetKeyDown(KeyCode.S))
        {
            direction = Vector2.down * 100;
            throwingDirection = Vector2.down;
        }
        switch (weaponType)
        {
            case 1:
                Debug.Log("I am holding a Melee Weapon!");
                if(Time.time >=nextMeleeAttackTime)
                {
                    if (Input.GetButtonDown("Fire" + playerNumber) && alreadyHolding)
                    {
                        MeleeAttack();
                        nextMeleeAttackTime = Time.time + 1f / meleeSpeed; // Counts how many times pr second you can swing.
                    }
                }
                break;

            case 2:
                Debug.Log("I am holding a Splash Weapon!");
                if (Input.GetButtonDown("Fire" + playerNumber) && alreadyHolding)
                {
                    SweetSpotSplashAttack();
                    nextSplashAttackTime = Time.time + 1f / splashSpeed;
                }
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

        if (Input.GetButtonDown("Drop" + playerNumber) && alreadyHolding)
        {                    
            

            switch(weaponType)
            {
                case 1:
                    weaponType = 0;
                    alreadyHolding = false;
                    GameObject mel = Instantiate(meleePrefab, transform.position, Quaternion.identity);
                    item = mel.GetComponent<ItemBehavior>();
                    item.Throw();
                    item.dirInit(throwingDirection);

                    break;

                case 2:
                    weaponType = 0;
                    alreadyHolding = false;
                    GameObject sho = Instantiate(shotgubPrefab, transform.position, Quaternion.identity);
                    item = sho.GetComponent<ItemBehavior>();
                    item.Throw();
                    weaponType = 0;
                    alreadyHolding = false;
                    item.dirInit(throwingDirection);
                    break;
            }


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

    private void SweetSpotSplashAttack()
    {
        Debug.Log("I am shooting Bawls of Fiyah!");
        // play animation
        // Animator.SetTrigger("MeleeAttack");

        // Detects enemies in range
        Collider2D[] sweetSpotEnemies = Physics2D.OverlapCircleAll(splashSweetSpot.position, sweetSpotRadius);

        // Applies damage
        foreach (Collider2D enemy in sweetSpotEnemies)
        {
            if (enemy.CompareTag("Enemy"))
            {
                enemy.GetComponent<EnemyHealth>().TakeDamage(sweetSpotDamage);
            }
        }
    }   

    private void OnDrawGizmos()
    {
        if (meleeHurtbox == null)
            return;
        Gizmos.DrawWireSphere(meleeHurtbox.position, meleeRange);


        if (splashSweetSpot == null)
            return;
        Gizmos.DrawWireSphere(splashSweetSpot.position, sweetSpotRadius);
    }
}
