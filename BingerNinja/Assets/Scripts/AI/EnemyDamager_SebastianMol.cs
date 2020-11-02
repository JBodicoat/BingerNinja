using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// used by the melee type enemies to damage the player.
/// </summary>
public class EnemyDamager_SebastianMol : MonoBehaviour
{
	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.tag == "Player")
		{
			 FindObjectOfType<PlayerHealthHunger_MarioFernandes>().Hit(20);
		}
	}
}
