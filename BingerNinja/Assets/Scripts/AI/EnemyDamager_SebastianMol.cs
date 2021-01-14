// Jack 02/11/2020 changed damage to be a public variable rather than magic number
// Louie 03/11/2020 added player damage sfx
// sebastian mol 20/11/2020 added randome chance of affect applied to player
// Elliott 20/11/2020 added hit effect
//sebastian mol 22/11/2020 commented things
using UnityEngine;

/// <summary>
/// used by the melee type enemies to damage the player.
/// </summary>
public class EnemyDamager_SebastianMol : MonoBehaviour
{
	public float m_baseDamage;
	internal float m_damage;
	public float m_percentageChanceOfAffect = 0.5f;
	public float m_poisionDamage = 1;
	public float m_slowDebuff = 0.5f; //make thsi a range
	public float m_affectTime = 5;
    public HitEffectElliott m_HitEffectElliott;
	 void OnTriggerEnter2D(Collider2D b)
	{
		if (b.tag == "Player")
		{
			FindObjectOfType<PlayerHealthHunger_MarioFernandes>().Hit(m_damage);
			MeleeEnemy_SebastianMol a = GetComponentInParent<MeleeEnemy_SebastianMol>();
           // m_HitEffectElliott.StartHitEffect(false);
            b.GetComponent<HitEffectElliott>().RT(false);
            if (a.W == WU.WJ)
            {
				if(a.QW)
                {
					float rand = Random.Range(0, 2);

					if (rand > m_percentageChanceOfAffect)
					{
						FindObjectOfType<EffectManager_MarioFernandes>().AddEffect
							(new PoisionDefuff_MarioFernandes(m_poisionDamage, m_affectTime));
					}
					else
					{
						FindObjectOfType<EffectManager_MarioFernandes>().AddEffect
							(new SpeedEffect_MarioFernandes(m_affectTime, m_slowDebuff));
					}
				}					
			}

			gameObject.SetActive(false);
		}
	}
    void Start()
    {
       m_HitEffectElliott = GetComponent<HitEffectElliott>();
    }

}

