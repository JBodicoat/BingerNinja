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
	public float XQ;
	internal float XW;
	public float XE = 0.5f;
	public float XR = 1;
	public float XT = 0.5f; //make thsi a range
	public float XY = 5;
    public HitEffectElliott XU;
	 void OnTriggerEnter2D(Collider2D b)
	{
		if (b.tag == "Player")
		{
			FindObjectOfType<PlayerHealthHunger_MarioFernandes>().j(XW);
			MeleeEnemy_SebastianMol a = GetComponentInParent<MeleeEnemy_SebastianMol>();
           // m_HitEffectElliott.StartHitEffect(false);
            b.GetComponent<HitEffectElliott>().RT(false);
            if (a.CA == WU.WJ)
            {
				if(a.QW)
                {
					float I = Random.Range(0, 2);

					if (I > XE)
					{
						FindObjectOfType<EffectManager_MarioFernandes>().h
							(new PoisionDefuff_MarioFernandes(XR, XY));
					}
					else
					{
						FindObjectOfType<EffectManager_MarioFernandes>().h
							(new SpeedEffect_MarioFernandes(XY, XT));
					}
				}					
			}

			gameObject.SetActive(false);
		}
	}
    void Start()
    {
       XU = GetComponent<HitEffectElliott>();
    }

}

