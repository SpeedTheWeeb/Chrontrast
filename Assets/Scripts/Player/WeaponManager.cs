using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using FMODUnity;
using FMOD.Studio;
using UnityEngine.UI;
using TMPro;

public class WeaponManager : MonoBehaviour
{
    //public TextMeshPro textMesh;
    public TextMeshProUGUI textMesh;
    private bool alreadyHolding = false; // Checks if player is or isn't holding a Weapon Power-Up
    public int weaponType = 0;          // initial "unarmed" state
    public Transform firePoint;         // Reference point for raycast and Projectile spawn     
    public int playerNumber;
    public LayerMask enemyMask;
    Vector2 direction;
    public Vector2 throwingDirection;
    public GameObject rotatePoint;
    bool puInteract;
    GameObject puHover;
    public string powerup;
    public bool havePowerup;

    public Text txtDmg1;
    public Text txtATS1;
    public Text txtDmg2;
    public Text txtATS2;

    int dmgP1 = 0;
    int atsP1 = 0;
    int dmgP2 = 0;
    int atsP2 = 0;

    public Image imageShow;

    public bool trigger;
    private GameObject weaponHover;

    public Transform meleeHurtbox;      // Reference point for Overlap Circle
    public float meleeRange = 3f;       // Radius for Overlap Circle
    public float meleeDamage = 100f;     // Damage applied to Tag: Enemies inside Overlap Circle
    public float meleeSpeed = 2f;       // Attacks pr. second
    public float meleeDmgMod = 0;
    public float meleeASMod = 0;
    // public Animator meleeAnim;

    public GameObject grenadePrefab;    // Holds projectile prefab
    public float grenadeRadius = 3f;    // Range before projectile explodes
    public float splashSpeed = .5f;     // Attacks pr. second
    public float splashDmgMod = 0;
    public float splashASMod = 0;
    // public Animator splashAnim;

    public LineRenderer sniperSight;    // Holds lasersight effect
    public float sniperDamage = 1f;     // Damage applied to Tag: Enemies on raycast path
    public float sniperSpeed = 1f;      // Attacks pr. second
    public float sniperDmgMod = 0;
    public float sniperASMod = 0;
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
    EventInstance sfxPowerup; // FMOD PowerUp SFX

    bool meleeBool = true;
    bool splashBool = true;
    bool sniperBool = true;

    //Particles
    GameObject meleeAttackParticleObjectP1 = null;
    GameObject meleeAttackParticleObjectP2 = null;
    ParticleSystem meleeAttackParticleEmitterP1;
    ParticleSystem meleeAttackParticleEmitterP2;

    private void Start()
    {
        //Defines Particles
        meleeAttackParticleObjectP1 = GameObject.Find("P1MeleeParticleSystem");
        meleeAttackParticleObjectP2 = GameObject.Find("P2MeleeParticleSystem");
        meleeAttackParticleEmitterP1 = GetComponentInChildren<ParticleSystem>();
        meleeAttackParticleEmitterP2 = GetComponentInChildren<ParticleSystem>();

        switch (playerNumber)
        {
            case 1:
                sfxPickup = RuntimeManager.CreateInstance("event:/sfx/player/future/pickup");
                RuntimeManager.AttachInstanceToGameObject(sfxPickup, transform, GetComponent<Rigidbody2D>());
                sfxThrow = RuntimeManager.CreateInstance("event:/sfx/player/future/throw");
                RuntimeManager.AttachInstanceToGameObject(sfxThrow, transform, GetComponent<Rigidbody2D>());
                sfxPowerup = RuntimeManager.CreateInstance("event:/sfx/player/future/powerup");
                RuntimeManager.AttachInstanceToGameObject(sfxPowerup, transform, GetComponent<Rigidbody2D>());
                break;

            case 2:
                sfxPickup = RuntimeManager.CreateInstance("event:/sfx/player/past/pickup");
                RuntimeManager.AttachInstanceToGameObject(sfxPickup, transform, GetComponent<Rigidbody2D>());
                sfxThrow = RuntimeManager.CreateInstance("event:/sfx/player/past/throw");
                RuntimeManager.AttachInstanceToGameObject(sfxThrow, transform, GetComponent<Rigidbody2D>());
                sfxPowerup = RuntimeManager.CreateInstance("event:/sfx/player/past/powerup");
                RuntimeManager.AttachInstanceToGameObject(sfxPowerup, transform, GetComponent<Rigidbody2D>());
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

                    if(meleeBool)
                    {
                        MeleeAttack();
                        meleeBool = false;
                        Invoke("ResetMelee", meleeSpeed - meleeASMod);
                    }
                    break;

                case 2:
                    if (splashBool)
                    {
                        SplashAttack();
                        splashBool = false;
                        Invoke("ResetSplash", splashSpeed - splashASMod);
                    }
                    break;
            }
        }
        else if(Input.GetButton("Fire" + playerNumber) && alreadyHolding && weaponType == 3)
        {
            if(sniperBool)
            {
                sniperSight.enabled = true;
            }
        }
        else if(Input.GetButtonUp("Fire" + playerNumber) && alreadyHolding && weaponType == 3)
        {
            if (sniperBool)
            {            
                SniperAttack();
                sniperSight.enabled = false;
                sniperBool = false;
                Invoke("ResetSniper", sniperSpeed - sniperASMod);
            }
        }

        //Drop
        if (Input.GetButtonDown("Drop" + playerNumber))
        {           
            if (alreadyHolding)
            {
                if (itemName.Contains("Chip"))
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
                else if(itemName.Contains("Tome"))
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
            }
            else if (trigger)
            {
                ChooseWeapon(weaponHover);
                sfxPickup.start();
            }
            else if (puInteract)
            {
                PickupPowerUp(puHover);
                sfxPowerup.start();
            }
        }
    }

    void ResetMelee()
    {
        meleeBool = true;
    }
    void ResetSplash()
    {
        splashBool = true;
    }
    void ResetSniper()
    {
        sniperBool = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Weapons"))
        {
            trigger = true;
            weaponHover = collision.gameObject;
        }
        if (collision.CompareTag("PowerUp"))
        {
            string powerup = "";
            puInteract = true;
            puHover = collision.gameObject;
            switch(puHover.name.Split('_')[0])
            {
                case "ATS":
                    powerup = "Attck Speed Up";
                    break;
                case "Stronger":
                    powerup = "Damage Up";
                    break;
                case "Heal":
                    powerup = "Heal Crystal";
                    break;
                case "Repair":
                    powerup = "Repair Walls";
                    break;
                case "Glue":
                    powerup = "Glue Floor";
                    break;
                case "Freeze":
                    powerup = "Freeze Enemies";
                    break;
            }

            textMesh.text = powerup;

        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Weapons"))
        {
            trigger = false;
            weaponHover = null;
        }
        if (collision.CompareTag("PowerUp"))
        {
            puInteract = false;
            puHover = null;
            textMesh.text = "";
        }
    }
    void PickupPowerUp(GameObject col)
    {
        powerup = col.gameObject.name;
        havePowerup = true;
        ShowPower(col);
        Powerup pu = col.GetComponent<Powerup>();
        pu.player = gameObject;
        pu.activatePU();
        
        puHover = null;
    }
    void ShowPower(GameObject obj)
    {
        if(!obj.name.Contains("Str") && !obj.name.Contains("Heal"))
        {
            GameObject showingPower = Instantiate(obj, imageShow.transform.position, Quaternion.identity);
            showingPower.transform.localScale = new Vector3(obj.transform.localScale.x * 10, obj.transform.localScale.y * 10);
        }
        else if(obj.name.Contains("Str"))
        {
            switch(obj.name.Split('_')[0])
            {
                case "ATS":
                    switch(playerNumber)
                    {
                        case 1:
                            atsP1++;
                            txtATS1.text = "ATS: " + atsP1;
                            break;

                        case 2:
                            atsP2++;
                            txtATS2.text = "ATS: " + atsP2;
                            break;
                    }
                    break;

                case "Stronger":
                    switch (playerNumber)
                    {
                        case 1:
                            dmgP1++;
                            txtDmg1.text = "DMG: " + dmgP1;
                            break;

                        case 2:
                            dmgP2++;
                            txtDmg2.text = "DMG: " + dmgP2;
                            break;
                    }
                    break;
            }
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
        // Debug.Log("I am swinging my sword!");
        // play animation
        // Animator.SetTrigger("MeleeAttack");
        switch (playerNumber)
        {
            case 1:
                RuntimeManager.PlayOneShot("event:/sfx/player/future/melee/miss");
                //FutureMeleeParticleEffect
                meleeAttackParticleEmitterP1.Play();
                break;
            case 2:
                RuntimeManager.PlayOneShot("event:/sfx/player/past/melee/miss");
                //MedievalMeleeParticleEffect
                meleeAttackParticleEmitterP2.Play();
                break;
        }
        // Detects enemies in range
        Collider2D[] meleeEnemies = Physics2D.OverlapCircleAll(meleeHurtbox.position, meleeRange, enemyMask);


        // Applies damage
        foreach (Collider2D enemy in meleeEnemies)
        {
            if (enemy.CompareTag("Enemy"))
            {
                switch (playerNumber)
                {
                    case 1:
                        RuntimeManager.PlayOneShot("event:/sfx/player/future/melee/hit");
                        if(enemy.gameObject.name.Contains("Med"))
                        {
                            enemy.GetComponent<EnemyHealth>().TakeDamage(meleeDamage + ((meleeASMod / 100) * meleeDamage));
                        }
                        break;
                    case 2:
                        RuntimeManager.PlayOneShot("event:/sfx/player/past/melee/hit");
                        if (enemy.gameObject.name.Contains("Fan"))
                        {
                            enemy.GetComponent<EnemyHealth>().TakeDamage(meleeDamage + ((meleeASMod / 100) * meleeDamage));
                        }
                        break;
                }                
            }            
        }
    }

    private void SplashAttack()
    {
        switch (playerNumber)
        {
            case 1:
                RuntimeManager.PlayOneShot("event:/sfx/player/future/explosive/shoot");
                break;
            case 2:
                RuntimeManager.PlayOneShot("event:/sfx/player/past/explosive/shoot");
                break;
        }
        GameObject grenade = Instantiate(grenadePrefab, firePoint.position, Quaternion.identity);
        Grenade g = grenade.GetComponent<Grenade>();
        g.dmgMod = splashDmgMod;
        g.Move(direction, playerNumber);
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
                switch (playerNumber)
                {
                    case 1:
                        RuntimeManager.PlayOneShot("event:/sfx/player/future/sniper/hit");
                        break;
                    case 2:
                        RuntimeManager.PlayOneShot("event:/sfx/player/past/sniper/hit");
                        break;
                }
                enemy.TakeDamage(sniperDamage + ((sniperDmgMod/100)*sniperDamage));
            }            
        }
        else
        {
            switch (playerNumber)
            {
                case 1:
                    RuntimeManager.PlayOneShot("event:/sfx/player/future/sniper/miss");
                    break;
                case 2:
                    RuntimeManager.PlayOneShot("event:/sfx/player/past/sniper/miss");
                    break;
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