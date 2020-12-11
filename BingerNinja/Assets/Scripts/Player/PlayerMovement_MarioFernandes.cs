// Mário Fernandes
/// My class takes care of the movement of the character

///!!!!!!!!!!!!Needs Rigidbody2D to Work!!!!!!!!!!

// Mário 16/10/2020 - Abstract Movement code from old Character class
// Jamie 17/10/2020 - Removed some unneeded code and implemented function used by PlayerController class
// Mário 18/10/2020 - Add reset speed function
// Joao 23/10/2020 - Added reference to playerStealth script and stop movement while crouched in update
// Joao 26/10/2020 - *OUTDATED*Updated crouch restriction in update to check if player is not crouched*OUTDATED* Moved movement restriction to playerStealth Script
// Alanna 08/11/2020 - Added roll movement, changed the speed to 7.5 for one second. unable to get previous movement as a variable because of the way input is set up.
// Louie 30/11/2020 - Roll animation

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.WSA.Input;

public class PlayerMovement_MarioFernandes : MonoBehaviour
{
    protected Rigidbody2D m_rb;

    //Direction the character is moving
    protected Vector2 m_direction = new Vector2(0, 0);
    protected Vector2 m_old_direction = new Vector2(0, 0);

    public float m_speed = 5.0f;
    private float m_roll_speed = 7.5f;

    public float m_baseSpeed = 5.0f;
    public bool isRolling= false;
    private PlayerAnimation_LouieWilliamson m_pAnimation;

    public void ResetSpeed()
    {
        m_speed = m_baseSpeed;
    }
    public void RollMovement()
    {
        m_pAnimation.RollAnimation(true);

        if (m_old_direction.y < 0)
        {
            m_speed = m_roll_speed;
            //go down
            m_direction.y -= m_speed;
            StartCoroutine("RollTimer");
        }
        if (m_old_direction.y > 0)
        {
            m_speed = m_roll_speed;
            m_direction.y += m_speed;
            StartCoroutine("RollTimer");
            //roll up
        }
        if (m_old_direction.x < 0)
        {
            m_speed = m_roll_speed;
            m_direction.x -= m_speed;
            StartCoroutine("RollTimer");
            //goleft
        }
        if (m_old_direction.x > 0)
        {
            m_speed = m_roll_speed;
            m_direction.x += m_speed;
            StartCoroutine("RollTimer");
        }


    }
        //m_direction = m_speed * m_old_direction;
        //m_direction.Normalize();

        //m_rb.velocity = m_direction;

       
    
    //Recieves vector from the PlayerController script and is assigned to the m_direction vector
    public void RecieveVector(Vector2 vector)
    {
        if (!isRolling)
        {
            if (m_direction.x != 0 || m_direction.y != 0)
            {
                m_old_direction = m_direction;
            }
            m_direction = vector;
            
        }
    }

    void Start()
    {
        ResetSpeed();
        Physics2D.gravity = Vector2.zero;
        m_rb = GetComponent<Rigidbody2D>();
        m_pAnimation = GetComponentInChildren<PlayerAnimation_LouieWilliamson>();
    }

    void Update()
    {
        m_direction.Normalize();
        m_direction *= m_speed;

        m_rb.velocity = m_direction;
    }
    IEnumerator RollTimer()
    {
        yield return new WaitForSeconds(1.0f);
        isRolling = false;
        m_pAnimation.RollAnimation(false);
        m_speed = 5.0f;
    }
}
