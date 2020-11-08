// Jack 02/11/2020 changed damage to be a public variable rather than magic number
// Louie 03/11/2020 added player damage sfx
// Elliott 08/11/2020 added hit effect
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// used by the melee type enemies to damage the player.
/// </summary>
public class EnemyDamager_SebastianMol : MonoBehaviour
{
   
    public float m_baseDamage;
	internal float m_damage;
	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.tag == "Player")
		{
			FindObjectOfType<PlayerHealthHunger_MarioFernandes>().Hit(m_damage);
            collision.GetComponent<HitEffectElliott>().StartHitEffect(false);
        }
    }
}

