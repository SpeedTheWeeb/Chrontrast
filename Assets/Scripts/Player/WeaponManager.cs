using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    public bool alreadyHolding = false; // Checks if player is or isn't holding a Weapon Power-Up
    public int weaponType = 0;          // initial "unarmed" state
    public Transform firePoint;         // Reference point for raycast and Projectile spawn     
    public int playerNumber;
    public LayerMask enemyMask;
    Vector2 direction;
    public Vector2 throwingDirection;

    public Transform meleeHurtbox;      // Reference point for Overlap Circle
    public float meleeRange = 1f;       // Radius for Overlap Circle
    public float meleeDamage = 10f;     // Damage applied to Tag: Enemies inside Overlap Circle
    public float meleeSpeed = 2f;       // Attacks pr. second
    float nextMeleeAttackTime = 0f;     // Initialized cooldown for melee attacks
    // public Animator meleeAnim;

    public GameObject grenadePrefab;    // Holds projectile prefab
    public float grenadeRadius = 3f;    // Range before projectile explodes
    public float splashSpeed = .5f;     // Attacks pr. second
    float nextSplashAttackTime = 0f;    // Initialized cooldown for splash attacks
    // public Animator splashAnim;

    public LineRenderer sniperSight;    // Holds lasersight effect
    public float sniperDamage = 5f;     // Damage applied to Tag: Enemies on raycast path
    public float sniperSpeed = 1f;      // Attacks pr. second
    float nextSniperAttackTime = 0f;    // Initialized cooldown for sniper attacks
    // public Animator sniperAnim;

    public GameObject holdingWeapon;

    public GameObject meleePrefab;
    public GameObject shotgunPrefab;
    public GameObject sniperPrefab;
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
                if (Time.time >= nextSplashAttackTime)
                {
                    if (Input.GetButtonDown("Fire" + playerNumber) && alreadyHolding)
                    {
                        SplashAttack();
                        nextSplashAttackTime = Time.time + 1f / splashSpeed;
                    }
                }
                break;

            case 3:
                Debug.Log("I am holding a Sniper Weapon!");
                if(Time.time >= nextSniperAttackTime)
                {
                    if(Input.GetButtonDown("Fire" + playerNumber) && alreadyHolding)
                    {
                        sniperSight.enabled = true;
                    }

                    if (Input.GetButtonUp("Fire" + playerNumber) && alreadyHolding)
                    {
                        SniperAttack();
                        nextSniperAttackTime = Time.time + 1f / sniperSpeed;
                        sniperSight.enabled = false;
                    }
                }
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
                    GameObject sho = Instantiate(shotgunPrefab, transform.position, Quaternion.identity);
                    item = sho.GetComponent<ItemBehavior>();
                    item.Throw();                    
                    item.dirInit(throwingDirection);
                    break;

                case 3:
                    weaponType = 0;
                    alreadyHolding = false;
                    GameObject sni = Instantiate(sniperPrefab, transform.position, Quaternion.identity);
                    item = sni.GetComponent<ItemBehavior>();
                    item.Throw();
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
        Collider2D[] meleeEnemies = Physics2D.OverlapCircleAll(meleeHurtbox.position, meleeRange, enemyMask);        

        // Applies damage
        foreach(Collider2D enemy in meleeEnemies)
        {
            if(enemy.CompareTag("Enemy"))
            {                
                enemy.GetComponent<EnemyHealth>().TakeDamage(meleeDamage);
            }
        }
    }

    private void SplashAttack()
    {
        Debug.Log("I am shooting Bawls of Fiyah!");
        Instantiate(grenadePrefab, firePoint.position, firePoint.rotation);
    }   

    void SniperAttack()
    {
        Debug.Log("I am no-scoping shit!");
        // play animation
        // Animator.SetTrigger("SniperAttack");

        // Detects enemies in range
        RaycastHit2D hitInfo = Physics2D.Raycast(firePoint.position, firePoint.up, enemyMask);
        if(hitInfo)
        {
            Debug.Log("I hit " + hitInfo);
            EnemyHealth enemy = hitInfo.transform.GetComponent<EnemyHealth>();
            if(enemy != null)
            {
                enemy.TakeDamage(sniperDamage);
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
