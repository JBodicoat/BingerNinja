// Jack 20/10 Updated to support new input system
//            Added support for the buff/debuff system

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public enum weaponType { Fugu, Squid, Rice_ball, Kobe_Beef, Sashimi }; //Added Extra Weapons

public class Weapon_Morgan : MonoBehaviour
{
    //scripts 
    OnigiriWeapon_Morgan onigiriScript;
    SquidWeapon_Morgan squidScript;
    //Character characterScript;

    PlayerHealthHunger_MarioFernandes playerHealthAndHungerScript;
    EffectManager_MarioFernandes statusEffectManager;
    PoisionDefuff_MarioFernandes squidStatusEffect = new PoisionDefuff_MarioFernandes(10, 5, 0.5f);
    HealBuff_MarioFernandes onigiriStatusEffect = new HealBuff_MarioFernandes(50, 3, 1.5f);

    GameObject clone = null;

    //enumerators
    weaponType currentWeapon;

    bool isWeaponHeld = false;

    //mouse position in world space
    Vector2 MousePos;
    //projectile velocity
    Vector2 velocity;
    //weapon cooldown
    float attackCooldown;
    float squidCooldown;

    //projectiles / weapons
    public GameObject Fugu;
    public GameObject Squid; 

    // Start is called before the first frame update
    void Start()
    {
        //characterScript = FindObjectOfType<Character>();
        playerHealthAndHungerScript = FindObjectOfType<PlayerHealthHunger_MarioFernandes>();
        statusEffectManager = FindObjectOfType<EffectManager_MarioFernandes>();
    }

    // Update is called once per frame
    void Update()
    {
        //cooldown
        if (attackCooldown >= 0)
        {
            attackCooldown -= Time.deltaTime;
        }

        if (isWeaponHeld)
        {
            var mouse = Mouse.current;
            MousePos = Camera.main.ScreenToWorldPoint(mouse.position.ReadValue());

            switch (currentWeapon)
            {
                case weaponType.Fugu:
                    /* Commented out for new input system
                    if (Input.GetMouseButtonDown(0))
                    {
                        LeftClickOnigiri();
                    }
                    */
                    break;

                case weaponType.Squid:
                    {
                        if(squidScript && squidScript.isAttacking)
                        {
                            squidCooldown -= Time.deltaTime;
                            if(squidCooldown <= 0)
                            {
                                Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, 2);
                                foreach (Collider2D collider in hits)
                                {
                                    if (collider.tag == "Enemy")
                                    {
                                        collider.GetComponent<EnemyAi>().Hit(60);
                                    }
                                }
                                Destroy(clone);
                                clone = null;
                                squidScript = null;
							}
						}
                        else
                        {
                            if (mouse.leftButton.wasPressedThisFrame)
                            {
                                SquidLeftClick();
                            }

                            if (mouse.leftButton.wasReleasedThisFrame)
                            {
                                attackCooldown = 1;
                                squidScript.isAttacking = true;
                                squidScript.isPreparing = false;
                                squidCooldown = 0.5f;
                            }

                        /* Commented out for new input system
                        if (Input.GetMouseButtonDown(0))
                        {
                            SquidLeftClick();
                        }

                        if (Input.GetMouseButtonUp(0))
                        {
                            attackCooldown = 1;
                            squidScript.isAttacking = true;
                            squidScript.isPreparing = false;
                            squidCooldown = 0.5f;
                        }
                        */
                    }
                    }
                    break;

                default:
                    break;
            }

            var keyboard = Keyboard.current;
            if(keyboard.eKey.wasPressedThisFrame)
            {
                EatWeapon();
			}
        }
    }

    void LeftClickOnigiri()
    {
        //MousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        if (attackCooldown <= 0)
        {
            //plan weapon to throw in direction of mouse
            MouseClickDirection();

            //Debug.Log("x" + velocity.x + "y" + velocity.y);

            //create instance of damaging projectile
            clone = Instantiate(Fugu, gameObject.transform.position, Quaternion.identity);
            onigiriScript = clone.GetComponent<OnigiriWeapon_Morgan>();
            onigiriScript.velocity = velocity;

            //put weapon on cooldown
            attackCooldown = 1;
        }
    }

    void SquidLeftClick()
    {
        if (attackCooldown <= 0)
        {
            MouseClickDirection();

            // actions

            clone = Instantiate(squid, gameObject.transform.position, Quaternion.identity, characterScript.gameObject.transform);
            squidScript = clone.GetComponent<SquidWeapon_Morgan>();
            squidScript.isPreparing = true;
            squidScript.mouseDirection = velocity;
            
        }
    }

    void MouseClickDirection()
    {
        velocity.x = MousePos.x - gameObject.transform.position.x;
        velocity.y = MousePos.y - gameObject.transform.position.y;
        velocity.Normalize();
    }

    public bool PickupWeapon(weaponType type)
    {
        if (!isWeaponHeld)
        {
            isWeaponHeld = true;
            currentWeapon = type;
            return true;
        }

        return false;
	}

    public void EatWeapon()
    {
        if(isWeaponHeld)
        {
            //characterScript.Eat(60);
            playerHealthAndHungerScript.Eat(60);
            isWeaponHeld = false;

            switch(currentWeapon)
            {
                case weaponType.squid:
                    statusEffectManager.AddEffect(squidStatusEffect);
                    break;

                case weaponType.onigiri:
                    statusEffectManager.AddEffect(onigiriStatusEffect);
                    break;
			}

            if(clone)
            {
                Destroy(clone);
                clone = null;
			}
        }
	}
}

