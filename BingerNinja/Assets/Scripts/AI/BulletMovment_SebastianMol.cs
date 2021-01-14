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
    public float q;
    internal Vector2 w;
    public float e;

    public bool r = false;
    public int t;
    public float y;
    public float u;  
    
    public bool i = false;
    public float o = 0;
    public float p; 
    
    public bool a = false;
    public float s;
    public float d = 0;

    public float  f;

     void Start()
    {
        Destroy(gameObject, 2);
    }
    void Update()
    {
        transform.position += (Vector3)w * q * Time.deltaTime ;
        transform.Rotate(new Vector3(0, 0, f * Time.deltaTime));
    }

	 void OnTriggerEnter2D(Collider2D a)
	{

        if (a.gameObject.name == "Walls1_map")
        {
           Destroy(gameObject);
        }

        if (a.tag == "Player")
        {
            if (r)
            {
                int g = Random.Range(0, t);
                if(g == t) FindObjectOfType<EffectManager_MarioFernandes>().h(new PoisionDefuff_MarioFernandes(y, u));
            }
            
            if(i)
            {
                FindObjectOfType<EffectManager_MarioFernandes>().h(new SpeedEffect_MarioFernandes(p, 0));
            } 
            
            if(this.a)
            {
                FindObjectOfType<EffectManager_MarioFernandes>().h(new HealBuff_MarioFernandes(s, d));
            }

            FindObjectOfType<PlayerHealthHunger_MarioFernandes>().j(e);
            //TODO collision.GetComponent<HitEffectElliott>().StartHitEffect(false);

         

            Destroy(gameObject);
        }  

    }

     void OnCollisionEnter2D(Collision2D b)
    {
      
        Destroy(gameObject);
    }
}
