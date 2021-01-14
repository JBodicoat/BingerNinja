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

    public float Q;

    public GameObject WR;

    public float E;

    public bool R = false;

    public float T;

    public int Y;

    public float U = 3;

    public float I;

    public float O = 0.2f;

    public float P = 0.2f;
    public float S = 500;

     bool A = true;
    public bool D = false;
     Pathfinder_SebastianMol ab;
     List<Vector2Int> ac;

    protected bool F = false;
    protected bool G = false;

    /// <summary>
    /// activates the "enemy weapon" object that damages the player uses quick attack
    /// </summary>
    /// <returns></returns>
    protected IEnumerator H()
    {
        WR.GetComponent<EnemyDamager_SebastianMol>().XW
            = WR.GetComponent<EnemyDamager_SebastianMol>().XQ;
        WR.SetActive(true);
        yield return new WaitForSeconds(E);
       // m_attackCollider.SetActive(false);
    }

    /// <summary>
    /// activates the "enemy weapon" object that damages the player uses charge attack
    /// </summary>
    /// <returns></returns>
    protected IEnumerator J()
    {
        yield return new WaitForSeconds(T);
        EnemyDamager_SebastianMol K = WR.GetComponent<EnemyDamager_SebastianMol>();
        K.XW = K.XQ * U;
        WR.SetActive(true);
        yield return new WaitForSeconds(E);
        //m_attackCollider.SetActive(false);
    }

    protected void QuickAttack()
    {
        StartCoroutine(H());
    }

    protected void ChargeAttack()
    {
        StartCoroutine(J());
    }

    protected void StunAfterAttack()
    {
        RO(I);
    }

    /// <summary>
    /// ovveride class that holds logic for what the enemy shoudl do when in the attack state
    /// </summary>
    internal override void ET()
    {
        if(base.CA == WU.WF)
        {

            EnemyAttacks_SebastianMol.E(QY, ref QO,
                WR, Q, gameObject, S); //make this ibnto a public variable
        }
        else
        {
            EnemyAttacks_SebastianMol.Q(ref QO, R, Y, this.QuickAttack,
                                               this.ChargeAttack, this.StunAfterAttack, base.CA, Q, GetComponent<Enemy_Animation_LouieWilliamson>());
        }
       
    }

    /// <summary>
    /// make the attack range bigg again the reson it is small so that the enenmy walks toward the player befor attacking
    /// </summary>
    /// <param name="ER"> amnount of time befor the attack is big again</param>
    /// <returns></returns>
    protected IEnumerator EE(float ER, float ET = 0) //this is a very not clean way to do things but it doseent look bad in teh game 
    {
        yield return new WaitForSeconds(ER);
        if (V != QS )
        {
            if( ET == 0)
            {
                V = QS; //change teh attack range back to normal 
            }
            else
            {
                V = ET;
            }
            
        }
        G = false;
    }
    void LateUpdate()
    {
        if(base.CA == WU.WJ)
        {
            if ((base.CO / QR) > M)
                if (GameObject.FindObjectOfType<PlayerStealth_JoaoBeijinho>().F()) //confusuion when player stelths
                {
                    if (!F)
                    {
                        RO(QQ);
                        F = true;
                    }
                }
                else
                {
                    F = false;
                }
        }

        if(!QD)
        if(G)
        {
            StartCoroutine(EE(P));
        }
    }


    protected void OnTriggerEnter2D(Collider2D a)
    {
        if(a.gameObject.tag != Tags_JoaoBeijinho.m_enemyTag && a.gameObject.tag != "Untagged")
        {
            if (base.CA == WU.WF || base.CA == WU.WK)
            {
                Rigidbody2D rijy = GetComponent<Rigidbody2D>();
                if (rijy.bodyType == RigidbodyType2D.Dynamic)
                {

                    rijy.bodyType = RigidbodyType2D.Kinematic;
                    rijy.velocity = Vector2.zero;
                    WR.SetActive(false);
                    //if hit wall walk away one tile 
                    //if hit wall stunn
                    if (a.gameObject.name == "Walls1_map")
                    {
                        V = 0.01f;
                        G = true;
                        RO(I);
                    }

                    if (a.gameObject.CompareTag(Tags_JoaoBeijinho.m_playerTag))
                    {
                        if (base.CA == WU.WF || QF == 1)
                            FindObjectOfType<EffectManager_MarioFernandes>().h
                                    (new SpeedEffect_MarioFernandes(1, 0)); //change thesey to not be magic numbers
                    }
                }
            }
        }      
    }
}
