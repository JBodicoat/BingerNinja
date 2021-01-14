// Jack 20/10 changed to support new PlayerHealthAndHunger script
// Jack 02/11/2020 added damage dealt as a variable replacing magic number
// Louie 03/11/2020 added player damage sfx
// Elliott 20/11/2020 added hit effect

using UnityEngine;

/// <summary>
/// used to move the projectile used by enemies
/// </summary>
public class BulletMovment_SebastianMol : MonoBehaviour
{
    public float m_speed;
    internal Vector2 m_direction;
    public float m_damage;

    public bool m_dosePoisonDamage = false;
    public int m_poisonDamageChance;
    public float m_poisionDamage;
    public float m_posionDamageTime;  
    
    public bool m_doseStunDamage = false;
    public float m_stunAmount = 0;
    public float m_stunDamageTime; 
    
    public bool m_doseHeal = false;
    public float m_healDuration;
    public float m_HealAmount = 0;

    public float  m_rotateSpeed;

     void Start()
    {
        Destroy(gameObject, 2);
    }
    void Update()
    {
        transform.position += (Vector3)m_direction * m_speed * Time.deltaTime ;
        transform.Rotate(new Vector3(0, 0, m_rotateSpeed * Time.deltaTime));
    }

	 void OnTriggerEnter2D(Collider2D a)
	{

        if (a.gameObject.name == "Walls1_map")
        {
           Destroy(gameObject);
        }

        if (a.tag == "Player")
        {
            a.GetComponent<HitEffectElliott>().RT(false);
            if (m_dosePoisonDamage)
            {
                int rand = Random.Range(0, m_poisonDamageChance);
                if(rand == m_poisonDamageChance) FindObjectOfType<EffectManager_MarioFernandes>().AddEffect(new PoisionDefuff_MarioFernandes(m_poisionDamage, m_posionDamageTime));
            }
            
            if(m_doseStunDamage)
            {
                FindObjectOfType<EffectManager_MarioFernandes>().AddEffect(new SpeedEffect_MarioFernandes(m_stunDamageTime, 0));
            } 
            
            if(m_doseHeal)
            {
                FindObjectOfType<EffectManager_MarioFernandes>().AddEffect(new HealBuff_MarioFernandes(m_healDuration,m_HealAmount));
            }

            FindObjectOfType<PlayerHealthHunger_MarioFernandes>().Hit(m_damage);
            //TODO collision.GetComponent<HitEffectElliott>().StartHitEffect(false);

         

            Destroy(gameObject);
        }  

    }

     void OnCollisionEnter2D(Collision2D b)
    {
      
        Destroy(gameObject);
    }
}
