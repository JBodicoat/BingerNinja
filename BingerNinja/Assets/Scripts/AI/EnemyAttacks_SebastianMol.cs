//sebastian mol 14/11/2020 script created

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class EnemyAttacks_SebastianMol : MonoBehaviour
{
    public static bool MelleAttack(ref float m_attackTimer, bool m_hasChargeAttack, int m_chargAttackPosibility, Action QuickAttack, Action ChargeAttack, Action StunIfTiger, float m_petTigerDeley, m_enemyType m_currentEnemyType, float m_hitSpeed, bool hasRangedAttack = false)
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
                StunIfTiger();
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

    public static bool RangedAttack(Transform m_playerTransform, Transform transform, GameObject m_aimer, ref float m_attackTimer, GameObject m_projectile, float m_shootDeley)
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

}
