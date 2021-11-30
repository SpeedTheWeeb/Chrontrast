using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using FMODUnity;
using FMOD.Studio;

public class WeaponManager : MonoBehaviour
{
    private bool alreadyHolding = false; // Checks if player is or isn't holding a Weapon Power-Up
    public int weaponType = 0;          // initial "unarmed" state
    public Transform firePoint;         // Reference point for raycast and Projectile spawn     
    public int playerNumber;
    public LayerMask enemyMask;
    Vector2 direction;
    public Vector2 throwingDirection;
    public GameObject rotatePoint;

    public bool trigger;
    private GameObject weaponHover;

    public Transform meleeHurtbox;      // Reference point for Overlap Circle
    public float meleeRange = 3f;       // Radius for Overlap Circle
    public float meleeDamage = 100f;     // Damage applied to Tag: Enemies inside Overlap Circle
    public float meleeSpeed = 2f;       // Attacks pr. second
    float nextMeleeAttackTime = 0f;     // Initialized cooldown for melee attacks
    // public Animator meleeAnim;

    public GameObject grenadePrefab;    // Holds projectile prefab
    public float grenadeRadius = 3f;    // Range before projectile explodes
    public float splashSpeed = .5f;     // Attacks pr. second
    float nextSplashAttackTime = 0f;    // Initialized cooldown for splash attacks
    // public Animator splashAnim;

    public LineRenderer sniperSight;    // Holds lasersight effect
    public float sniperDamage = 1f;     // Damage applied to Tag: Enemies on raycast path
    public float sniperSpeed = 1f;      // Attacks pr. second
    float nextSniperAttackTime = 0f;    // Initialized cooldown for sniper attacks
    // public Animator sniperAnim;

    public GameObject holdingWeapon;

    public GameObject FmeleePrefab;
    public GameObject FshotgunPrefab;
    public GameObject FsniperPrefab;
    public GameObject MmeleePrefab;
    public GameObject MshotgunPrefab;
    public GameObject MsniperPrefab;
    ItemBehavior item;

    private string itemName;

    EventInstance sfxPickup; // FMOD Pickup SFX
    EventInstance sfxThrow; //FMOD Throw SFX

    private void Start()
    {
        switch (playerNumber)
        {
            case 1:
                sfxPickup = RuntimeManager.CreateInstance("event:/sfx/player/future/pickup");
                RuntimeManager.AttachInstanceToGameObject(sfxPickup, transform, GetComponent<Rigidbody2D>());
                sfxThrow = RuntimeManager.CreateInstance("event:/sfx/player/future/throw");
                RuntimeManager.AttachInstanceToGameObject(sfxThrow, transform, GetComponent<Rigidbody2D>());
                break;

            case 2:
                sfxPickup = RuntimeManager.CreateInstance("event:/sfx/player/past/pickup");
                RuntimeManager.AttachInstanceToGameObject(sfxPickup, transform, GetComponent<Rigidbody2D>());
                sfxThrow = RuntimeManager.CreateInstance("event:/sfx/player/past/throw");
                RuntimeManager.AttachInstanceToGameObject(sfxThrow, transform, GetComponent<Rigidbody2D>());
                break;
        }           
    }

    private void Update()
    {
        //Direction
        throwingDirection = rotatePoint.transform.up * 100;
        direction = rotatePoint.transform.up;

        //Fire
        if (Input.GetButtonDown("Fire" + playerNumber) && alreadyHolding && weaponType != 3)
        {
            switch (weaponType)
            {
                case 1:
                    if (Time.time >= nextMeleeAttackTime)
                    {
                        MeleeAttack();
                        nextMeleeAttackTime = Time.time + 1f / meleeSpeed; // Counts how many times pr second you can swing.
                    }
                    break;

                case 2:
                    if (Time.time >= nextSplashAttackTime)
                    {
                        SplashAttack();
                        nextSplashAttackTime = Time.time + 1f / splashSpeed;
                    }
                    break;
            }
        }
        else if(Input.GetButton("Fire" + playerNumber) && alreadyHolding && weaponType == 3)
        {
                sniperSight.enabled = true;
        }
        else if(Input.GetButtonUp("Fire" + playerNumber) && alreadyHolding && weaponType == 3)
        {
                SniperAttack();
                sniperSight.enabled = false;
                nextSniperAttackTime = Time.time + 1f / sniperSpeed;
        }

        //Drop
        if (Input.GetButtonDown("Drop" + playerNumber))
        {
            if (!alreadyHolding)
            {
                sfxPickup.start();
                sfxPickup.release();
            }
            
            if (alreadyHolding)
            {

                if (itemName.Contains("Future"))
                {
                    switch (weaponType)
                    {
                        case 1:
                            weaponType = 0;
                            alreadyHolding = false;
                            GameObject mel = Instantiate(FmeleePrefab, rotatePoint.transform.position, Quaternion.identity);
                            item = mel.GetComponent<ItemBehavior>();
                            item.Throw();
                            item.dirInit(throwingDirection);
                            break;

                        case 2:
                            weaponType = 0;
                            alreadyHolding = false;
                            GameObject sho = Instantiate(FshotgunPrefab, transform.position, Quaternion.identity);
                            item = sho.GetComponent<ItemBehavior>();
                            item.Throw();
                            item.dirInit(throwingDirection);
                            break;

                        case 3:
                            weaponType = 0;
                            alreadyHolding = false;
                            GameObject sni = Instantiate(FsniperPrefab, transform.position, Quaternion.identity);
                            item = sni.GetComponent<ItemBehavior>();
                            item.Throw();
                            item.dirInit(throwingDirection);
                            break;
                    }
                }
                else if(itemName.Contains("Med"))
                {
                    switch (weaponType)
                    {
                        case 1:
                            weaponType = 0;
                            alreadyHolding = false;
                            GameObject mel = Instantiate(MmeleePrefab, transform.position, Quaternion.identity);
                            item = mel.GetComponent<ItemBehavior>();
                            item.Throw();
                            item.dirInit(throwingDirection);
                            break;

                        case 2:
                            weaponType = 0;
                            alreadyHolding = false;
                            GameObject sho = Instantiate(MshotgunPrefab, transform.position, Quaternion.identity);
                            item = sho.GetComponent<ItemBehavior>();
                            item.Throw();
                            item.dirInit(throwingDirection);
                            break;

                        case 3:
                            weaponType = 0;
                            alreadyHolding = false;
                            GameObject sni = Instantiate(MsniperPrefab, transform.position, Quaternion.identity);
                            item = sni.GetComponent<ItemBehavior>();
                            item.Throw();
                            item.dirInit(throwingDirection);
                            break;
                    }
                }

                sfxThrow.start();
                sfxThrow.release();
            }
            else if (trigger)
            {
                ChooseWeapon(weaponHover);                
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Weapons"))
        {
            trigger = true;
            weaponHover = collision.gameObject;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Weapons"))
        {
            trigger = false;
            weaponHover = null;
        }
    }

    void ChooseWeapon(GameObject weapon)
    {
        itemName = weapon.GetComponent<SpriteRenderer>().sprite.name;
        weaponType = 0;

        if(weapon.name.Contains("Melee"))
        {
            weaponType = 1;
        }
        else if(weapon.name.Contains("Shotgun"))
        {
            weaponType = 2;
        }
        else if(weapon.name.Contains("Sniper"))
        {
            weaponType = 3;
        }
        
        alreadyHolding = true;
        Pickup(weapon);
        
    }

    void Pickup(GameObject pl)
    {
        Destroy(pl);
    }

    private void MeleeAttack()
    {
        Debug.Log("I am swinging my sword!");
        // play animation
        // Animator.SetTrigger("MeleeAttack");

        // Detects enemies in range
        Collider2D[] meleeEnemies = Physics2D.OverlapCircleAll(meleeHurtbox.position, meleeRange, enemyMask);

        // Applies damage
        foreach (Collider2D enemy in meleeEnemies)
        {
            if (enemy.CompareTag("Enemy"))
            {
                enemy.GetComponent<EnemyHealth>().TakeDamage(meleeDamage);
            }
        }
    }

    private void SplashAttack()
    {
        Debug.Log("I am shooting Bawls of Fiyah!");
        GameObject grenade = Instantiate(grenadePrefab, firePoint.position, Quaternion.identity);
        Grenade g = grenade.GetComponent<Grenade>();
        g.Move(direction);
    }

    void SniperAttack()
    {
        // play animation
        // Animator.SetTrigger("SniperAttack");

        // Detects enemies in range
        RaycastHit2D hitInfo = Physics2D.Raycast(firePoint.position, direction, enemyMask);
        Debug.DrawRay(firePoint.position, direction);
        //Debug.DrawRay(firePoint.position, throwingDirection);
        if (hitInfo)
        {
            EnemyHealth enemy = hitInfo.collider.transform.GetComponent<EnemyHealth>();

            if (enemy != null)
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