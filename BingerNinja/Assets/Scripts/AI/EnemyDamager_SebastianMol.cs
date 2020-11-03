// Jack 02/11/2020 changed damage to be a public variable rather than magic number

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// used by the melee type enemies to damage the player.
/// </summary>
public class EnemyDamager_SebastianMol : MonoBehaviour
{
	public float damageDealt;

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.tag == "Player")
		{
			 FindObjectOfType<PlayerHealthHunger_MarioFernandes>().Hit(damageDealt);
		}
	}
}
