//sebastian mol 14/11/2020 class created
//sebastian mol 29/11/2020 finished and commeneted all logic for final boss
// louie        11/12/2020 Attack animation
using UnityEngine;

/// <summary>
/// class for enemies with ranegd and melee attcks
/// </summary>
class RangedAndMeleeEnemy_SebastianMol :  MeleeEnemy_SebastianMol
 {

    public GameObject Q;
    public GameObject W;

    public float R;

    public float T;

    public int Y;

    public float U = 3;

    public float I = 1;
    private int O;
    private bool P = false;
    private int A;

    public float G = 10;
    public float H = 3;
    public float J = 1;

    public GameObject K;
    public GameObject L;
    public GameObject[] Z;
    public SpriteRenderer C;
    public GameObject V;

    private int B;
    private float N;
    private bool M = false;
    private GameObject qq = null;
    private int qw = 0;
    private int qe = 3;
    private float qr;
    private float qy;


    //i have to put this here i cba to find a cleaner way to do this future seb get your shit togther


    /// <summary>
    /// ovveride class that holds logic for what the enemy shoudl do when in the attack state
    /// </summary>
    internal override void QP()
    {                    
        if(E == global::y.f || E == global::y.u)
        {
            if (A == Y - 1)
            {
                if (EnemyAttacks_SebastianMol.d(GetComponent<Enemy_Animation_LouieWilliamson>(), qt, transform, Q,
                    ref qo, W, R))
                {
                    P = false;
                }
            }
            else
            {
                if (EnemyAttacks_SebastianMol.q(ref qo, r, y,
                    S, D, F,
                    E, q, GetComponent<Enemy_Animation_LouieWilliamson>()))
                {
                    P = false;
                }
            }
        }

        if (E == global::y.j)
        {
            //health above 60
            if(qx == 1)
            {
                if (B == 0)
                {
                    qu();
                }
                else
                {
                    qi();
                    //TODO cool down
                }
            }
            else if(qx == 2)//health above 30 change this later
            {
                GameObject[] qo = GameObject.FindGameObjectsWithTag(Tags_JoaoBeijinho.m_lightTag);
                foreach (var qp in qo)
                {
                    qp.SetActive(false);
                    //add effect for light goign off or on or whatever
                }

                if (N > 0.3f)
                {
                    qa();
                }
                else
                {
                    qs();
                }
            }
            else if (qx == 3)//health below 30
            {
                //destroy all plants on this lvl
                GameObject[] qd = GameObject.FindGameObjectsWithTag(Tags_JoaoBeijinho.qf);
                foreach (var qg in qd)
                {
                    qg.SetActive(false);
                    //do an effect for plants to dissapoear
                }

                if (qr < 0.33f)
                {
                    qi();
                }
                else if (qr < 0.66f)
                {
                    qu();
                }
                else if (qr >= 0.66f)
                {
                    qa();
                }
            }
            
            
        }
    }
    /// <summary>
    /// logic for tadashi tripple shot attack
    /// </summary>
    private void qs()
    {
        if (qw < qe)
        {
            C.sprite = null;
            qq = V;
            if (EnemyAttacks_SebastianMol.d(GetComponent<Enemy_Animation_LouieWilliamson>(), qt, transform, Q,
                ref qo, qq, 0.3f))
            {
                qw++;
                qo = 0.1f;
            }

        }
        else
        {
            qw = 0;
            qo = R;
            M = false;
            qq = null;
        }
    }
    /// <summary>
    /// logic for tadashi normal attack
    /// </summary>
    private void qi()
    {
        if (EnemyAttacks_SebastianMol.q(ref qo, r, y,
                   S, D, F,
                   E, q, GetComponent<Enemy_Animation_LouieWilliamson>()))
        {
            M = false;
        }
    }
    /// <summary>
    /// logic for tadashi charg attack
    /// </summary>       
    private void qu()
    {
        if (EnemyAttacks_SebastianMol.m(qt, ref qo,
                   w, q, gameObject, a))
            M = false; //make this ibnto a public variable
    }
    /// <summary>
    /// logic for tadashi ranged attack that uses multiple projectiles
    /// </summary>
    private void qa()
    {
        if (qq)
        {
            if (EnemyAttacks_SebastianMol.d(GetComponent<Enemy_Animation_LouieWilliamson>(), qt, transform, Q,
                             ref qo, qq, R))
            {
                M = false;
                qq = null;
            }
        }
        
    }

    /// <summary>
    /// set up for tadashi quick attack
    /// </summary>
    private void qk()
    {    
        X = J;
        w = K;
        if(L) L.SetActive(false);
        C.sprite = null;
        M = true;
        qo = q;
        qy = J;
    }
    /// <summary>
    /// set up for tadashi charged attack
    /// </summary>
    private void ql()
    {
        X = G;
        w = L;
        if (K) K.SetActive(false);
        C.sprite = null;
        M = true;
        qo = t;
        qy = G;
    }
    /// <summary>
    /// set up for tadashi ranged attack with multiple projectiles
    /// </summary>
    private void qz()
    {
        X = U;
        w = K;
        N = Random.Range(0.0f, 1.0f);
        M = true;
        qo = R;
        qy = U;

        if (!qq)
        {
            int rand = Random.Range(0, 4);
   
            switch (rand)
            {
                case 0:
                    W = Z[0];
                    break;

                case 1:
                    W = Z[1];
                    break;

                case 2:
                    W = Z[2];
                    break;

                case 3:
                    W = Z[3];
                    break;
            }

            C.sprite = W.GetComponent<SpriteRenderer>().sprite;
            C.color = W.GetComponent<SpriteRenderer>().color; //delete thsi line when yi get the art for projectiles

            qq = W;

        }
        
    }

    /// <summary>
    /// handles the set up for each fase of tadashi boss fight
    /// </summary>
    private void qc()
    {    if(E == global::y.j)
        switch (qx)
        {
            case 1:
                if (!M)
                {
                        B = Random.Range(0, 2);
                        M = true;
                    if (B == 0)
                    {
                            ql();
                  
                    }
                    else
                    {
                            qk();
              
                    }
                }
                break;

            case 2:
                if (!M)
                {
                        qz();
                }
                break;

            case 3:
              if(!M)
              {
                        qq = null;
                        C.sprite = null;
                        qr = Random.Range(0.0f, 1.0f);

                    if (qr < 0.33f)
                    {
                            qk();
           

                    }
                    else if (qr < 0.66f)
                    {
                            ql();
                
                    }
                    else if (qr > 0.66f)
                    {
                            qz();
                   
                    }

              }
               break;
        }


        qv(qy);

    }

    /// <summary>
    /// used to walk away from all after charge attack so tadashi dosent get stuck
    /// </summary>
    /// <param name="range"></param>
    private void qv(float range)
    {
        if (!qj)
            if (j)
                StartCoroutine(G(p, range));
    }


    /// <summary>
    /// updates teh attack ranged based on what attack is coming up e.g. more range for ranged attacks
    /// </summary>
    private void qb()
    {
        if(E == global::y.f || E == global::y.u)
        {
            if (!P)
            {
                A = Random.Range(0, Y);
                P = true;
            }

            if (A == Y - 1)
            {
                X = U;
                
            }
            else
            {
                X = I;
               
            }

            if (!qj)
                if (j)
                {
                    StartCoroutine(G(p));
                }
        }
        
    }

    private void LateUpdate()
    {
        qb();
        qc();      
    }
}
