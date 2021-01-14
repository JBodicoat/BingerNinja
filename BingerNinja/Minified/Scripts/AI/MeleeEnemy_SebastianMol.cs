using System.Collections;using System.Collections.Generic;using UnityEngine;class MeleeEnemy_SebastianMol : BaseEnemy_SebastianMol{public float m_hitSpeed;public GameObject m_attackCollider;public float attackDeactivationSpeed;public bool m_hasChargeAttack = false;public float m_chargeAttackDeley;public int m_chargAttackPosibility;public float m_chargeAttackMultiplier = 3;public float m_afterAttackDeley;public float m_maxChargTimeBeforDamege = 0.2f;public float m_amountOfTimeToMoveAwayFromWall = 0.2f;public float m_chargeAttackSpeed = 500;private bool A = true;public bool showPathBeforAttackTigerBoss = false;private Pathfinder_SebastianMol ab;private List<Vector2Int> ac;protected bool m_doStunOnce = false;protected bool m_doMoveAwayFromWallOnce = false;protected IEnumerator QuickAttackCo(){m_attackCollider.GetComponent<EnemyDamager_SebastianMol>().m_damage= m_attackCollider.GetComponent<EnemyDamager_SebastianMol>().m_baseDamage;m_attackCollider.SetActive(true);yield return new WaitForSeconds(attackDeactivationSpeed);}protected IEnumerator ChargeAttackCo(){yield return new WaitForSeconds(m_chargeAttackDeley);EnemyDamager_SebastianMol dameger = m_attackCollider.GetComponent<EnemyDamager_SebastianMol>();dameger.m_damage = dameger.m_baseDamage * m_chargeAttackMultiplier;m_attackCollider.SetActive(true);yield return new WaitForSeconds(attackDeactivationSpeed);}protected void QuickAttack(){SC(QuickAttackCo());}protected void ChargeAttack(){SC(ChargeAttackCo());}protected void StunAfterAttack(){RO(m_afterAttackDeley);}internal override void ET(){if(W == WU.WF){EnemyAttacks_SebastianMol.ChargeAttack(QY, ref QO,m_attackCollider, m_hitSpeed, gameObject, m_chargeAttackSpeed);}else {EnemyAttacks_SebastianMol.MelleAttack(ref QO, m_hasChargeAttack, m_chargAttackPosibility, QuickAttack,ChargeAttack, StunAfterAttack, W, m_hitSpeed, GetComponent<Enemy_Animation_LouieWilliamson>());}}protected IEnumerator MoveAwayFromeWall(float amaountOfTime, float attackRange = 0){yield return new WaitForSeconds(amaountOfTime);if (V != QS ){if( attackRange == 0){V = QS;}else {V = attackRange;}}m_doMoveAwayFromWallOnce = false;}void LateUpdate(){if(W == WU.WJ){if ((I / QR) > M)if (FOT<PlayerStealth_JoaoBeijinho>().F()){if (!m_doStunOnce){RO(QQ);m_doStunOnce = true;}}else {m_doStunOnce = false;}}if(!QD)if(m_doMoveAwayFromWallOnce){SC(MoveAwayFromeWall(m_amountOfTimeToMoveAwayFromWall));}}protected void OnTriggerEnter2D(Collider2D a){if(a.gameObject.tag != Tags_JoaoBeijinho.m_enemyTag && a.gameObject.tag != "Untagged"){if (W == WU.WF || W == WU.WK){Rigidbody2D rijy = GetComponent<Rigidbody2D>();if (rijy.bodyType == RigidbodyType2D.Dynamic){rijy.bodyType = RigidbodyType2D.Kinematic;rijy.velocity = Vector2.zero;m_attackCollider.SetActive(false);if (a.gameObject.name == "Walls1_map"){V = 0.01f;m_doMoveAwayFromWallOnce = true;RO(m_afterAttackDeley);}if (a.gameObject.CompareTag(Tags_JoaoBeijinho.m_playerTag)){if (W == WU.WF || QF == 1)FOT<EffectManager_MarioFernandes>().AddEffect(new SpeedEffect_MarioFernandes(1, 0));}}}}}}