using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class EnemyAttacks_SebastianMol : MonoBehaviour
{
    public void MelleAttack(float m_attackTimer, bool m_hasChargeAttack, int m_chargAttackPosibility, Action QuickAttack, Action ChargeAttack, Action StunIfTiger, float m_petTigerDeley, m_enemyType m_currentEnemyType, float m_hitSpeed)
    {
        if (m_attackTimer <= 0)
        {
            if (m_hasChargeAttack)
            {
                int rand = UnityEngine.Random.Range(0, m_chargAttackPosibility);
                switch (rand)
                {
                    case 0:
                        QuickAttack();
                        break;

                    case 1:
                        ChargeAttack();
                        break;
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
        }
        else
        {
            m_attackTimer -= Time.deltaTime;
        }
    }


}
