//sebastian mol 14/11/2020 class created
//sebastian mol 29/11/2020 finished and commeneted all logic for final boss
// louie        11/12/2020 Attack animation
using UnityEngine;

/// <summary>
/// class for enemies with ranegd and melee attcks
/// </summary>
class RangedAndMeleeEnemy_SebastianMol :  MeleeEnemy_SebastianMol
 {

    public GameObject EQ;
    public GameObject RQ;

    public float TQ;

    public float YQ;

    public int UQ;

    public float UW = 3;

    public float UE = 1;
    private int UR;
    private bool UT = false;
    private int UY;

    public float UU = 10;
    public float UI = 3;
    public float UO = 1;

    public GameObject UP;
    public GameObject UA;
    public GameObject[] US;
    public SpriteRenderer UD;
    public GameObject UF;

    private int q;
    private float w;
    private bool e = false;
    private GameObject r = null;
    private int t = 0;
    private int y = 3;
    private float u;
    private float i;


    //i have to put this here i cba to find a cleaner way to do this future seb get your shit togther


    /// <summary>
    /// ovveride class that holds logic for what the enemy shoudl do when in the attack state
    /// </summary>
    internal override void ET()
    {                    
        if(CA == WU.WG || CA == WU.WI)
        {
            if (UY == UQ - 1)
            {
                if (EnemyAttacks_SebastianMol.W(GetComponent<Enemy_Animation_LouieWilliamson>(), QY, transform, EQ,
                    ref QO, RQ, TQ))
                {
                    UT = false;
                }
            }
            else
            {
                if (EnemyAttacks_SebastianMol.Q(ref QO, R, Y,
                    QuickAttack, ChargeAttack, StunAfterAttack,
                    CA, Q, GetComponent<Enemy_Animation_LouieWilliamson>()))
                {
                    UT = false;
                }
            }
        }

        if (CA == WU.WK)
        {
            //health above 60
            if(QF == 1)
            {
                if (q == 0)
                {
                    UG();
                }
                else
                {
                    UH();
                    //TODO cool down
                }
            }
            else if(QF == 2)//health above 30 change this later
            {
                GameObject[] UJ = GameObject.FindGameObjectsWithTag(Tags_JoaoBeijinho.UL);
                foreach (var UK in UJ)
                {
                    UK.SetActive(false);
                    //add effect for light goign off or on or whatever
                }

                if (w > 0.3f)
                {
                    UZ();
                }
                else
                {
                    UX();
                }
            }
            else if (QF == 3)//health below 30
            {
                //destroy all plants on this lvl
                GameObject[] UC = GameObject.FindGameObjectsWithTag(Tags_JoaoBeijinho.UB);
                foreach (var UV in UC)
                {
                    UV.SetActive(false);
                    //do an effect for plants to dissapoear
                }

                if (u < 0.33f)
                {
                   UH();
                }
                else if (u < 0.66f)
                {
                    UG();
                }
                else if (u >= 0.66f)
                {
                    UZ();
                }
            }
            
            
        }
    }
    /// <summary>
    /// logic for tadashi tripple shot attack
    /// </summary>
    void UX()
    {
        if (t < y)
        {
            UD.sprite = null;
            r = UF;
            if (EnemyAttacks_SebastianMol.W(GetComponent<Enemy_Animation_LouieWilliamson>(), QY, transform, EQ,
                ref QO, r, 0.3f))
            {
                t++;
                QO = 0.1f;
            }

        }
        else
        {
            t = 0;
            QO = TQ;
            e = false;
            r = null;
        }
    }
    /// <summary>
    /// logic for tadashi normal attack
    /// </summary>
    void UH()
    {
        if (EnemyAttacks_SebastianMol.Q(ref QO, R, Y,
                   QuickAttack, ChargeAttack, StunAfterAttack,
                   CA, Q, GetComponent<Enemy_Animation_LouieWilliamson>()))
        {
            e = false;
        }
    }
    /// <summary>
    /// logic for tadashi charg attack
    /// </summary>       
    void UG()
    {
        if (EnemyAttacks_SebastianMol.E(QY, ref QO,
                   WR, Q, gameObject, S))
            e = false; //make this ibnto a public variable
    }
    /// <summary>
    /// logic for tadashi ranged attack that uses multiple projectiles
    /// </summary>
    void UZ()
    {
        if (r)
        {
            if (EnemyAttacks_SebastianMol.W(GetComponent<Enemy_Animation_LouieWilliamson>(), QY, transform, EQ,
                             ref QO, r, TQ))
            {
                e = false;
                r = null;
            }
        }
        
    }

    /// <summary>
    /// set up for tadashi quick attack
    /// </summary>
    void IQ()
    {    
        V = UO;
        WR = UP;
        if(UA) UA.SetActive(false);
        UD.sprite = null;
        e = true;
        QO = Q;
        i = UO;
    }
    /// <summary>
    /// set up for tadashi charged attack
    /// </summary>
    void IW()
    {
        V = UU;
        WR = UA;
        if (UP) UP.SetActive(false);
        UD.sprite = null;
        e = true;
        QO = T;
        i = UU;
    }
    /// <summary>
    /// set up for tadashi ranged attack with multiple projectiles
    /// </summary>
    void IE()
    {
        V = UW;
        WR = UP;
        w = Random.Range(0.0f, 1.0f);
        e = true;
        QO = TQ;
        i = UW;

        if (!r)
        {
            int rand = Random.Range(0, 4);
   
            switch (rand)
            {
                case 0:
                    RQ = US[0];
                    break;

                case 1:
                    RQ = US[1];
                    break;

                case 2:
                    RQ = US[2];
                    break;

                case 3:
                    RQ = US[3];
                    break;
            }

            UD.sprite = RQ.GetComponent<SpriteRenderer>().sprite;
            UD.color = RQ.GetComponent<SpriteRenderer>().color; //delete thsi line when yi get the art for projectiles

            r = RQ;

        }
        
    }

    /// <summary>
    /// handles the set up for each fase of tadashi boss fight
    /// </summary>
   void IR()
    {    if(CA == WU.WK)
        switch (QF)
        {
            case 1:
                if (!e)
                {
                    q = Random.Range(0, 2);
                    e = true;
                    if (q == 0)
                    {
                        IW();
                  
                    }
                    else
                    {
                        IQ();
              
                    }
                }
                break;

            case 2:
                if (!e)
                {
                    IE();
                }
                break;

            case 3:
              if(!e)
              {
                    r = null;
                    UD.sprite = null;
                    u = Random.Range(0.0f, 1.0f);

                    if (u < 0.33f)
                    {
                        IQ();
           

                    }
                    else if (u < 0.66f)
                    {
                        IW();
                
                    }
                    else if (u > 0.66f)
                    {
                        IE();
                   
                    }

              }
               break;
        }


        IT(i);

    }

    /// <summary>
    /// used to walk away from all after charge attack so tadashi dosent get stuck
    /// </summary>
    /// <param name="a"></param>
     void IT(float a)
    {
        if (!QD)
            if (G)
                StartCoroutine(EE(P, a));
    }


    /// <summary>
    /// updates teh attack ranged based on what attack is coming up e.g. more range for ranged attacks
    /// </summary>
    void IY()
    {
        if(CA == WU.WG || CA == WU.WI)
        {
            if (!UT)
            {
                UY = Random.Range(0, UQ);
                UT = true;
            }

            if (UY == UQ - 1)
            {
                V = UW;
                
            }
            else
            {
                V = UE;
               
            }

            if (!QD)
                if (G)
                {
                    StartCoroutine(EE(P));
                }
        }
        
    }

    void LateUpdate()
    {
        IY();
        IR();      
    }
}
