///Louie Williamson
///This script handles all of the enemy animation states by changing the parameters in their animators.

//Louie 18/11/2020 - First created

using UnityEngine;

public class Enemy_Animation_LouieWilliamson : MonoBehaviour
{
    private Animator m_animator;
    private Vector2 m_lastPosition;
    private float m_xMovement;
    private float m_yMovement;
    private bool isFacingLeft;

    public void AttackAnimation()
    {
        m_animator.SetTrigger("isAttacking");
    }
    private void Start()
    {
        m_animator = GetComponentInChildren<Animator>();
        m_lastPosition = transform.position;
        isFacingLeft = true;
    }

    private void Update()
    {
        m_xMovement = m_lastPosition.x - transform.position.x;
        m_yMovement = m_lastPosition.y - transform.position.y;
        
        //face enemy in the right direction
        if (m_yMovement > 0)
        {
            m_animator.SetBool("isFacing", true);
        }
        else if (m_yMovement < 0)
        {
            m_animator.SetBool("isFacing", false);
        }

        //Is he moving to the left
        if (m_xMovement < 0)
        {
            //is he already facing left?
            if (!isFacingLeft)
            {
                FlipSprite();
                isFacingLeft = true;
            }
        }
        else if (m_xMovement > 0)
        {
            if (isFacingLeft)
            {
                FlipSprite();
                isFacingLeft = false;
            }
        }

        //Set Idle/ Moving animation trigger
        if (m_xMovement == 0 && m_yMovement == 0)
        {
            m_animator.SetTrigger("isIdle");
        }
        else
        {
            m_animator.SetTrigger("isMoving");
        }

        

        m_lastPosition = transform.position;
    }
    private void FlipSprite()
    {
        transform.localScale = new Vector3(transform.localScale.x * -1, transform.localScale.y, transform.localScale.z);
    }
}
