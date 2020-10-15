using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum weaponType { onigiri, squid };

public class Weapon_Morgan : MonoBehaviour
{
    //scripts 
    OnigiriWeapon_Morgan onigiriScript;
    SquidWeapon_Morgan squidScript;
    Character characterScript;

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
    public GameObject onigiri;
    public GameObject squid; 

    // Start is called before the first frame update
    void Start()
    {
        characterScript = FindObjectOfType<Character>();
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
            switch (currentWeapon)
            {
                case weaponType.onigiri:
                    if (Input.GetMouseButtonDown(0))
                    {
                        LeftClickOnigiri();
                    }
                    break;

                case weaponType.squid:
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
                        }
                    }
                    break;

                default:
                    break;
            }
        }
    }

    void LeftClickOnigiri()
    {
        MousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        if (attackCooldown <= 0)
        {
            //plan weapon to throw in direction of mouse
            MouseClickDirection();

            //Debug.Log("x" + velocity.x + "y" + velocity.y);

            //create instance of damaging projectile
            clone = Instantiate(onigiri, gameObject.transform.position, Quaternion.identity);
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
            characterScript.Eat(60);
            isWeaponHeld = false;

            if(clone)
            {
                Destroy(clone);
                clone = null;
			}
        }
	}
}

