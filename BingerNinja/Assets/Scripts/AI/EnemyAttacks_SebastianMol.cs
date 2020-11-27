//sebastian mol 14/11/2020 script created
//sebastian mol 22/11/2020 charge attack created

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// used to decouple attack logic from enemie classes
/// </summary>
public class EnemyAttacks_SebastianMol : MonoBehaviour
{
    /// <summary>
    /// holds logic for melee attacks
    /// </summary>
    /// <param name="m_attackTimer"></param>
    /// <param name="m_hasChargeAttack"></param>
    /// <param name="m_chargAttackPosibility"></param>
    /// <param name="QuickAttack"></param>
    /// <param name="ChargeAttack"></param>
    /// <param name="StunAfterAttack"></param>
    /// <param name="m_petTigerDeley"></param>
    /// <param name="m_currentEnemyType"></param>
    /// <param name="m_hitSpeed"></param>
    /// <param name="hasRangedAttack"></param>
    /// <returns>weather the attack has been done</returns>
    public static bool MelleAttack(ref float m_attackTimer, bool m_hasChargeAttack, int m_chargAttackPosibility, 
        Action QuickAttack, Action ChargeAttack, Action StunAfterAttack,
        m_enemyType m_currentEnemyType, float m_hitSpeed, bool hasRangedAttack = false)
    {
        if (m_attackTimer <= 0)
        {
            if (m_hasChargeAttack)
            {
                int rand = UnityEngine.Random.Range(0, m_chargAttackPosibility);
                if(rand == m_chargAttackPosibility-1)
                {
                    ChargeAttack();
                }
                else
                {
                    QuickAttack();
                }
            }
            else
            {
                QuickAttack();
            }

            if (m_currentEnemyType == m_enemyType.PETTIGER)
            {
                StunAfterAttack();
            }

            m_attackTimer = m_hitSpeed;
            return true;
        }
        else
        {
            m_attackTimer -= Time.deltaTime;
            return false;
        }
    }

    /// <summary>
    /// holds logic for ranged attacks
    /// </summary>
    /// <param name="m_playerTransform"></param>
    /// <param name="transform"></param>
    /// <param name="m_aimer"></param>
    /// <param name="m_attackTimer"></param>
    /// <param name="m_projectile"></param>
    /// <param name="m_shootDeley"></param>
    /// <returns>weather the attack has been done</returns>
    public static bool RangedAttack(Transform m_playerTransform, Transform transform, GameObject m_aimer, 
        ref float m_attackTimer, GameObject m_projectile, float m_shootDeley)
    {
        if (m_playerTransform != null)
        {
            Vector3 dir = Vector3.Normalize(m_playerTransform.position - transform.position);
            float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
            m_aimer.transform.eulerAngles = new Vector3(0, 0, angle);

            if (m_attackTimer <= 0)
            {
                GameObject projectile = Instantiate(m_projectile, transform.position, Quaternion.Euler(new Vector3(dir.x, dir.y, 0)));
                projectile.GetComponent<BulletMovment_SebastianMol>().m_direction = (m_playerTransform.position - transform.position).normalized;
                m_attackTimer = m_shootDeley;
                return true;
            }
            else
            {
                m_attackTimer -= Time.deltaTime;
                return false;
            }
        }
        return false;
    }

    /// <summary>
    /// hold logic for a charge attack.
    /// </summary>
    /// <param name="m_playerTransform"></param>
    /// <param name="m_attackTimer"></param>
    /// <param name="m_attackCollider"></param>
    /// <param name="m_hitSpeed"></param>
    /// <param name="m_thisEnemy"></param>
    /// <param name="chargeForce"></param>
    /// <returns>weather the attack has been done</returns>
    public static bool ChargeAttack(Transform m_playerTransform, ref float m_attackTimer, 
        GameObject m_attackCollider, float m_hitSpeed, GameObject m_thisEnemy, float chargeForce)
    {
        if (m_attackTimer <= 0)
        {
            //shoot at enemy
            m_attackCollider.SetActive(true);
            Rigidbody2D rijy = m_thisEnemy.GetComponent<Rigidbody2D>();
            rijy.bodyType = RigidbodyType2D.Dynamic;
            rijy.gravityScale = 0;
            rijy.freezeRotation = true;
            rijy.AddForce((m_playerTransform.position - m_thisEnemy.transform.position).normalized * chargeForce );
            EnemyDamager_SebastianMol enemyDamager = m_attackCollider.GetComponent<EnemyDamager_SebastianMol>();
            enemyDamager.m_damage = enemyDamager.m_baseDamage;
            m_attackTimer = m_hitSpeed;

            return true;
        }
        else
        {
            m_attackTimer -= Time.deltaTime;
            return false;
        }
    }

}
