using System;using UnityEngine;public class EnemyAttacks_SebastianMol : M{public static bool MelleAttack(ref float m_attackTimer, bool m_hasChargeAttack, int m_chargAttackPosibility,Action QuickAttack, Action ChargeAttack, Action StunAfterAttack,m_enemyType m_currentEnemyType, float m_hitSpeed, Enemy_Animation_LouieWilliamson enemyAnim, bool hasRangedAttack = false){if (m_attackTimer <= 0){if (m_hasChargeAttack){int rand = UnityEngine.Random.Range(0, m_chargAttackPosibility);if(rand == m_chargAttackPosibility-1){ChargeAttack();}else {QuickAttack();}}else {QuickAttack();}if (m_currentEnemyType == m_enemyType.PETTIGER){StunAfterAttack();}m_attackTimer = m_hitSpeed;return true;}else {m_attackTimer -= Time.deltaTime;enemyAnim.AttackAnimation();return false;}}public static bool RangedAttack(Enemy_Animation_LouieWilliamson enemyAnim, Transform m_playerTransform, Transform transform, GameObject m_aimer,ref float m_attackTimer, GameObject m_projectile, float m_shootDeley,float sizeIncreaseX = 0, float sizeIncreaseY = 0){if (m_playerTransform != null){Vector3 dir = Vector3.Normalize(m_playerTransform.position - transform.position);float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;m_aimer.transform.eulerAngles = new Vector3(0, 0, angle);if (m_attackTimer <= 0){GameObject projectile = Instantiate(m_projectile, transform.position, Quaternion.Euler(new Vector3(dir.x, dir.y, 0)));projectile.transform.localScale += new Vector3(sizeIncreaseX, sizeIncreaseY, 0);projectile.GetComponent<BulletMovment_SebastianMol>().m_direction = (m_playerTransform.position - transform.position).normalized;ColorChanger_Jann.Instance.UpdateColor(projectile.GetComponent<SpriteRenderer>());m_attackTimer = m_shootDeley;enemyAnim.AttackAnimation();return true;}else {m_attackTimer -= Time.deltaTime;return false;}}return false;}public static bool ChargeAttack(Transform m_playerTransform, ref float m_attackTimer,GameObject m_attackCollider, float m_hitSpeed, GameObject m_thisEnemy, float chargeForce){if (m_attackTimer <= 0){m_attackCollider.SetActive(true);Rigidbody2D rijy = m_thisEnemy.GetComponent<Rigidbody2D>();rijy.bodyType = RigidbodyType2D.Dynamic;rijy.gravityScale = 0;rijy.freezeRotation = true;rijy.AddForce((m_playerTransform.position - m_thisEnemy.transform.position).normalized * chargeForce );EnemyDamager_SebastianMol enemyDamager = m_attackCollider.GetComponent<EnemyDamager_SebastianMol>();enemyDamager.m_damage = enemyDamager.m_baseDamage;m_attackTimer = m_hitSpeed;return true;}else {m_attackTimer -= Time.deltaTime;return false;}}}