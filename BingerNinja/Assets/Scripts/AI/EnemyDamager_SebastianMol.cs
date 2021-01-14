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
	public float P;
	internal float O;
	public float q = 0.5f;
	public float w = 1;
	public float e = 0.5f; //make thsi a range
	public float r = 5;
    public HitEffectElliott t;
	private void OnTriggerEnter2D(Collider2D y)
	{
		if (y.tag == "Player")
		{
			FindObjectOfType<PlayerHealthHunger_MarioFernandes>().u(O);
			MeleeEnemy_SebastianMol i = GetComponentInParent<MeleeEnemy_SebastianMol>();
           // m_HitEffectElliott.StartHitEffect(false);
            y.GetComponent<HitEffectElliott>().WL(false);
            if (i.E == global::y.h)
            {
				if(i.M)
                {
					float o = Random.Range(0, 2);

					if (o > q)
					{
                        FindObjectOfType<EffectManager_MarioFernandes>().Z
							(new PoisionDefuff_MarioFernandes(w, r));
					}
					else
					{
                        FindObjectOfType<EffectManager_MarioFernandes>().Z
							(new SpeedEffect_MarioFernandes(r, e));
					}
				}					
			}

			gameObject.SetActive(false);
		}
	}
    private void Start()
    {
       t = GetComponent<HitEffectElliott>();
    }

}

