// Mário Fernandes
/// This class stores the current weapon on the player and make im abel to use it 

// Mário 17/10/2020 - Create class and Attack, PickUpFood, IIsHoldingFood Funcions
// Joao 25/10/2020 - Stop weapon usage while crouched in update

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat_MarioFernandes : MonoBehaviour
{
    public GameObject m_projectile;

    public Collider2D EnemyDetection;
    
    public float m_playerMeleeRange;
    protected PlayerStealth_JoaoBeijinho m_playerStealthScript;

    protected WeaponsTemplate_MarioFernandes m_currentWeapon;

  public  bool IsHoldingFood()
         {
       
        //Temp, should go the current weapon
        return false;
         }

    void PickUpFood()
    {
        
    }

    void Attack()
    {
        if(m_currentWeapon.IsRanged())
        {
             GameObject projectile = Instantiate(m_projectile, transform.position, transform.rotation);
             projectile.GetComponent<Projectile_MarioFernandes>().m_dmg = m_currentWeapon.dmg;
        }
        else
        {
            Collider2D[] results = new Collider2D[10];
            ContactFilter2D contact = new ContactFilter2D();
            

            EnemyDetection.OverlapCollider(contact.NoFilter(), results);            

            GameObject CloseEnemy = null;
            float distance = 100;

            foreach (var enemy in results)
            {
                if(enemy.tag == "Enemy")
                {
                    if (distance > Vector3.Distance(transform.position, enemy.transform.position))
                    {
                    distance = Vector3.Distance(transform.position, enemy.transform.position);
                    CloseEnemy = enemy.gameObject;
                    }
                }
            }

            if(CloseEnemy)
            {
                GetComponent<BaseEnemy_SebastianMol>().TakeDamage(m_currentWeapon.dmg);
            }
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        m_playerStealthScript = FindObjectOfType<PlayerStealth_JoaoBeijinho>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!m_playerStealthScript.m_crouched)
        {
            if(Input.GetMouseButton(0))
            Attack();
        }


    }

    private void OnTriggerEnter2D(Collider2D collision)
	{
		if(collision.GetComponent<WeaponsTemplate_MarioFernandes>())
        {
            m_currentWeapon = collision.GetComponent<WeaponsTemplate_MarioFernandes>();
		}
	}
}


