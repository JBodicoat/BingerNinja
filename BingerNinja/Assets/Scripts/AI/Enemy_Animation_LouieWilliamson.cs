///Louie Williamson
///This script handles all of the enemy animation states by changing the parameters in their animators.

//Louie 18/11/2020 - First created

using UnityEngine;

public class Enemy_Animation_LouieWilliamson : MonoBehaviour
{
    private Animator a;
    private Vector2 q;
    private float w;
    private float e;
    private bool r;

    public void t()
    {
        a.SetTrigger("isAttacking");
    }
    private void Start()
    {
        a = GetComponentInChildren<Animator>();
        q = transform.position;
        r = true;
    }

    private void Update()
    {
        w = q.x - transform.position.x;
        e = q.y - transform.position.y;
        
        //face enemy in the right direction
        if (e > 0)
        {
            a.SetBool("isFacing", true);
        }
        else if (e < 0)
        {
            a.SetBool("isFacing", false);
        }

        //Is he moving to the left
        if (w < 0)
        {
            //is he already facing left?
            if (!r)
            {
                y();
                r = true;
            }
        }
        else if (w > 0)
        {
            if (r)
            {
                y();
                r = false;
            }
        }

        //Set Idle/ Moving animation trigger
        if (w == 0 && e == 0)
        {
            a.SetTrigger("isIdle");
        }
        else
        {
            a.SetTrigger("isMoving");
        }

        

        q = transform.position;
    }
    private void y()
    {
        transform.localScale = new Vector3(transform.localScale.x * -1, transform.localScale.y, transform.localScale.z);
    }
}
