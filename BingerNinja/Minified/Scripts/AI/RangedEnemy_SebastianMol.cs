using UnityEngine;class RangedEnemy_SebastianMol : BaseEnemy_SebastianMol{public GameObject m_aimer;public GameObject m_projectile;public float m_shootDeley;public float m_projectileSpeed;internal override void AttackBehaviour(){EnemyAttacks_SebastianMol.RangedAttack(GetComponent<Enemy_Animation_LouieWilliamson>(), m_playerTransform, transform, m_aimer, ref m_attackTimer, m_projectile, m_shootDeley);}}