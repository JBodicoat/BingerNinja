//sebastian mol
//sebastian mol 30/10/20 removed patrol function
//sebastian mol 02/11/20 removed player behaviour switch replaced it with abstract functions

using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem.UI;
using UnityEngine.Networking;
using UnityEngine.Tilemaps;

/// <summary>
/// class that ranged enemys use
/// </summary>
class RangedEnemy_SebastianMol : BaseEnemy_SebastianMol
{
    [Header("class based damage variables")]
    [Header("projectile prefabs")]
    public GameObject m_aimer;
    public GameObject m_projectile;
    [Header("projectile Variables")]
    [Tooltip("how fast the projectile moves")]
    public float m_shootDeley;
    [Tooltip("speed of the projectile")]
    public float m_projectileSpeed;

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, m_attackRange);
    }

    /// <summary>
    /// ovveride class that holds logic for what the enemy shoudl do when in the attack state
    /// </summary>
    internal override void AttackBehaviour()
    {
        EnemyAttacks_SebastianMol.RangedAttack(m_playerTransform, transform, m_aimer, ref m_attackTimer, m_projectile, m_shootDeley);
    }

}
