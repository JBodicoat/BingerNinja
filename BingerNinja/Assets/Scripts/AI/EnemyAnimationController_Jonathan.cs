using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimationController_Jonathan : MonoBehaviour
{

    private Animator m_animator;
    private Vector2 m_lastPosition;
    private float m_xMovement;
    private float m_yMovement;
    private SpriteRenderer m_spriteRenderer;

    private void Start()
    {
        m_animator = GetComponentInChildren<Animator>();
        m_spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        m_xMovement = m_lastPosition.x - transform.position.x;
        m_yMovement = m_lastPosition.y - transform.position.y;
        
        ////Flip sprite if moving lef/right
        //if(m_xMovement > 0)
        //{
        //    m_spriteRenderer.flipX = false;
        //} else
        //{
        //    m_spriteRenderer.flipX = true;
        //}

        //Set Idle animation
        if(m_xMovement != 0 || m_yMovement != 0)
        {
            m_animator.SetBool("Idle", false);
        } else
        {
            m_animator.SetBool("Idle", true);
        }

        //Set yMovement animation
        m_animator.SetFloat("yMovement", m_yMovement);

        m_lastPosition = transform.position;
    }

}
