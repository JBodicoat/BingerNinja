// Jack 20/10 changed to support new PlayerHealthAndHunger script
// Jack 02/11/2020 added damage dealt as a variable replacing magic number
// Louie 03/11/2020 added player damage sfx
// Elliott 20/11/2020 added hit effect

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// used to move the projectile used by enemies
/// </summary>
public class BulletMovment_SebastianMol : MonoBehaviour
{
    public float m_speed;
    internal Vector2 m_direction;
    public float m_damage;

    public bool m_dosePoisonDamage = false;
    public int m_poisonDamageChance;
    public float m_poisionDamage;
    public float m_posionDamageTime;  
    
    public bool m_doseStunDamage = false;
    public float m_stunAmount = 0;
    public float m_stunDamageTime; 
    
    public bool m_doseHeal = false;
    public float m_healDuration;
    public float m_HealAmount = 0;


    private AudioManager_LouieWilliamson m_audioManager;

    private void Start()
    {
        Destroy(gameObject, 2);
        m_audioManager = FindObjectOfType<AudioManager_LouieWilliamson>();
    }
    void Update()
    {
        transform.position += (Vector3)m_direction * m_speed * Time.deltaTime ;
    }

	private void OnTriggerEnter2D(Collider2D collision)
	{
        Debug.Log(collision.gameObject.name);

        if (collision.gameObject.name == "Walls1_map")
        {
           Destroy(gameObject);
        }

        if (collision.tag == "Player")
        {
            if(m_dosePoisonDamage)
            {
                int rand = Random.Range(0, m_poisonDamageChance);
                if(rand == m_poisonDamageChance) FindObjectOfType<EffectManager_MarioFernandes>().AddEffect(new PoisionDefuff_MarioFernandes(m_poisionDamage, m_posionDamageTime));
            }
            
            if(m_doseStunDamage)
            {
                FindObjectOfType<EffectManager_MarioFernandes>().AddEffect(new SpeedEffect_MarioFernandes(m_stunDamageTime, 0));
            } 
            
            if(m_doseHeal)
            {
                FindObjectOfType<EffectManager_MarioFernandes>().AddEffect(new HealBuff_MarioFernandes(m_healDuration,m_HealAmount));
            }

            FindObjectOfType<PlayerHealthHunger_MarioFernandes>().Hit(m_damage);
            //TODO collision.GetComponent<HitEffectElliott>().StartHitEffect(false);
            Destroy(gameObject);
        }  

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
      
        Destroy(gameObject);
    }
}
