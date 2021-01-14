//sebastian mol
//sebastian mol 30/10/20 melee enemy shoudl be completed
//sebastian mol 02/11/20 removed player behaviour switch replaced it with abstract functions
//sebastian mol 09/11/20 chrage attack fixed 
//sebastian mol 20/11/2020 spce ninja enemy logic done
//sebastian mol 22/11/2020 tiuger logic in place
// louie        11/12/2020 Attack animation

using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// melee enemy class esed by any enemy that has melle attack
/// </summary>
class MeleeEnemy_SebastianMol : BaseEnemy_SebastianMol
{

    public float q;

    public GameObject w;

    public float e;

    public bool r = false;

    public float t;

    public int y;

    public float u = 3;

    public float i;

    public float o = 0.2f;

    public float p = 0.2f;
    public float a = 500;

    private bool s = true;
    public bool d = false;
    private Pathfinder_SebastianMol f;
    private List<Vector2Int> g;

    protected bool h = false;
    protected bool j = false;

    /// <summary>
    /// activates the "enemy weapon" object that damages the player uses quick attack
    /// </summary>
    /// <returns></returns>
    protected IEnumerator k()
    {
        w.GetComponent<EnemyDamager_SebastianMol>().O
            = w.GetComponent<EnemyDamager_SebastianMol>().P;
        w.SetActive(true);
        yield return new WaitForSeconds(e);
       // m_attackCollider.SetActive(false);
    }

    /// <summary>
    /// activates the "enemy weapon" object that damages the player uses charge attack
    /// </summary>
    /// <returns></returns>
    protected IEnumerator l()
    {
        yield return new WaitForSeconds(t);
        EnemyDamager_SebastianMol a = w.GetComponent<EnemyDamager_SebastianMol>();
        a.O = a.P * u;
        w.SetActive(true);
        yield return new WaitForSeconds(e);
        //m_attackCollider.SetActive(false);
    }

    protected void S()
    {
        StartCoroutine(k());
    }

    protected void D()
    {
        StartCoroutine(l());
    }

    protected void F()
    {
        WB(i);
    }

    /// <summary>
    /// ovveride class that holds logic for what the enemy shoudl do when in the attack state
    /// </summary>
    internal override void QP()
    {
        if(E == global::y.d)
        {

            EnemyAttacks_SebastianMol.m(qt, ref qo,
                w, q, gameObject, a); //make this ibnto a public variable
        }
        else
        {
            EnemyAttacks_SebastianMol.q(ref qo, r, y, this.S,
                                               this.D, this.F, E, q, GetComponent<Enemy_Animation_LouieWilliamson>());
        }
       
    }

    /// <summary>
    /// make the attack range bigg again the reson it is small so that the enenmy walks toward the player befor attacking
    /// </summary>
    /// <param name="H"> amnount of time befor the attack is big again</param>
    /// <returns></returns>
    protected IEnumerator G(float H, float J = 0) //this is a very not clean way to do things but it doseent look bad in teh game 
    {
        yield return new WaitForSeconds(H);
        if (X != qs )
        {
            if( J == 0)
            {
                X = qs; //change teh attack range back to normal 
            }
            else
            {
                X = J;
            }
            
        }
        j = false;
    }
    private void LateUpdate()
    {
        if(E == global::y.h)
        {
            if ((O / qw) > B)
                if (GameObject.FindObjectOfType<PlayerStealth_JoaoBeijinho>().QL()) //confusuion when player stelths
                {
                    if (!h)
                    {
                        WB(N);
                        h = true;
                    }
                }
                else
                {
                    h = false;
                }
        }

        if(!qj)
        if(j)
        {
            StartCoroutine(G(p));
        }
    }


    protected void OnTriggerEnter2D(Collider2D K)
    {
        if(K.gameObject.tag != Tags_JoaoBeijinho.m_enemyTag && K.gameObject.tag != "Untagged")
        {
            if (E == global::y.d || E == global::y.j)
            {
                Rigidbody2D L = GetComponent<Rigidbody2D>();
                if (L.bodyType == RigidbodyType2D.Dynamic)
                {

                    L.bodyType = RigidbodyType2D.Kinematic;
                    L.velocity = Vector2.zero;
                    w.SetActive(false);
                    //if hit wall walk away one tile 
                    //if hit wall stunn
                    if (K.gameObject.name == "Walls1_map")
                    {
                        X = 0.01f;
                        j = true;
                        WB(i);
                    }

                    if (K.gameObject.CompareTag(Tags_JoaoBeijinho.QC))
                    {
                        if (E == global::y.d || qx == 1)
                            FindObjectOfType<EffectManager_MarioFernandes>().Z
                                    (new SpeedEffect_MarioFernandes(1, 0)); //change thesey to not be magic numbers
                    }
                }
            }
        }      
    }
}
