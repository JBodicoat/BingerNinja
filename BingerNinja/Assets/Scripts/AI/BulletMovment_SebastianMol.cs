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
		if(collision.tag == "Player")
        {
            if(m_dosePoisonDamage)
            {
                int rand = Random.Range(0, m_poisonDamageChance);
                if(rand == m_poisonDamageChance) FindObjectOfType<EffectManager_MarioFernandes>().AddEffect(new PoisionDefuff_MarioFernandes(m_poisionDamage, m_posionDamageTime));
            }
           
            FindObjectOfType<PlayerHealthHunger_MarioFernandes>().Hit(m_damage);
            collision.GetComponent<HitEffectElliott>().StartHitEffect(false);
            Destroy(gameObject);
		}
	}
}
