using UnityEngine;public class BulletMovment_SebastianMol : M{public float m_speed;internal Vector2 m_direction;public float m_damage;public bool m_dosePoisonDamage = false;public int m_poisonDamageChance;public float m_poisionDamage;public float m_posionDamageTime;public bool m_doseStunDamage = false;public float m_stunAmount = 0;public float m_stunDamageTime;public bool m_doseHeal = false;public float m_healDuration;public float m_HealAmount = 0;public float  m_rotateSpeed;void Start(){D(gameObject, 2);}void Update(){transform.position += (Vector3)m_direction * m_speed * Time.deltaTime ;transform.Rotate(new Vector3(0, 0, m_rotateSpeed * Time.deltaTime));}void OnTriggerEnter2D(Collider2D a){if (a.gameObject.name == "Walls1_map"){D(gameObject);}if (a.tag == "Player"){a.GetComponent<HitEffectElliott>().RT(false);if (m_dosePoisonDamage){int rand = Random.Range(0, m_poisonDamageChance);if(rand == m_poisonDamageChance) FOT<EffectManager_MarioFernandes>().AddEffect(new PoisionDefuff_MarioFernandes(m_poisionDamage, m_posionDamageTime));}if(m_doseStunDamage){FOT<EffectManager_MarioFernandes>().AddEffect(new SpeedEffect_MarioFernandes(m_stunDamageTime, 0));}if(m_doseHeal){FOT<EffectManager_MarioFernandes>().AddEffect(new HealBuff_MarioFernandes(m_healDuration,m_HealAmount));}FOT<PlayerHealthHunger_MarioFernandes>().Hit(m_damage);D(gameObject);}}void OnCollisionEnter2D(Collision2D b){D(gameObject);}}