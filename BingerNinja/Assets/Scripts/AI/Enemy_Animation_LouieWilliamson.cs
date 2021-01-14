///Louie Williamson
///This script handles all of the enemy animation states by changing the parameters in their animators.

//Louie 18/11/2020 - First created

using UnityEngine;

public class Enemy_Animation_LouieWilliamson : MonoBehaviour
{
     Animator q;
     Vector2 w;
     float e;
     float r;
     bool t;

    public void Q()
    {
        q.SetTrigger("isAttacking");
    }
     void Start()
    {
        q = GetComponentInChildren<Animator>();
        w = transform.position;
        t = true;
    }

     void Update()
    {
        e = w.x - transform.position.x;
        r = w.y - transform.position.y;
        
        //face enemy in the right direction
        if (r > 0)
        {
            q.SetBool("isFacing", true);
        }
        else if (r < 0)
        {
            q.SetBool("isFacing", false);
        }

        //Is he moving to the left
        if (e < 0)
        {
            //is he already facing left?
            if (!t)
            {
                u();
                t = true;
            }
        }
        else if (e > 0)
        {
            if (t)
            {
                u();
                t = false;
            }
        }

        //Set Idle/ Moving animation trigger
        if (e == 0 && r == 0)
        {
            q.SetTrigger("isIdle");
        }
        else
        {
            q.SetTrigger("isMoving");
        }

        

        w = transform.position;
    }
     void u()
    {
        transform.localScale = new Vector3(transform.localScale.x * -1, transform.localScale.y, transform.localScale.z);
    }
}
