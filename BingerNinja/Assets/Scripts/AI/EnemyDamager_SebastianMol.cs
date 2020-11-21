// Jack 02/11/2020 changed damage to be a public variable rather than magic number
// Louie 03/11/2020 added player damage sfx
// sebastian mol 20/11/2020 added randome chance of affect applied to player
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// used by the melee type enemies to damage the player.
/// </summary>
public class EnemyDamager_SebastianMol : MonoBehaviour
{
	public float m_baseDamage;
	public float m_damage;
	[Range(0.0f, 1.0f)]
	public float m_percentageChanceOfAffect = 0.5f;

	public float m_poisionDamage = 1;
	public float m_slowDebuff = 0.5f; //make thsi a range
	public float m_affectTime = 5;
	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.tag == "Player")
		{
			FindObjectOfType<PlayerHealthHunger_MarioFernandes>().Hit(m_damage);
			MeleeEnemy_SebastianMol currentEnemyScript = GetComponentInParent<MeleeEnemy_SebastianMol>();
			if (currentEnemyScript.m_currentEnemyType == m_enemyType.SPACENINJABOSS)
            {
				if(currentEnemyScript.m_doseAffect)
                {
					float rand = Random.Range(0, 2);
					Debug.Log(rand);

					if (rand > m_percentageChanceOfAffect)
					{
						FindObjectOfType<EffectManager_MarioFernandes>().AddEffect
							(new PoisionDefuff_MarioFernandes(m_poisionDamage, m_affectTime));
						Debug.Log("poison");
					}
					else
					{
						FindObjectOfType<EffectManager_MarioFernandes>().AddEffect
							(new SpeedEffect_MarioFernandes(m_affectTime, m_slowDebuff));
						Debug.Log("slow");
					}
				}					
			}
		}
	}

}

