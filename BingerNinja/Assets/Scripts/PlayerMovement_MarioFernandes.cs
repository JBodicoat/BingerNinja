// Mário Fernandes
/// My class takes care of the movement of the character

///!!!!!!!!!!!!Needs Rigidbody2D to Work!!!!!!!!!!

// Mário 16/10/2020 - Abstract Movement code from old Character class
// Jamie 17/10/2020 - Removed some unneeded code and implemented function used by PlayerController class
// Mário 18/10/2020 - Add reset speed function
// Joao 23/10/2020 - Added reference to playerStealth script and stop movement while crouched in update
// Joao 26/10/2020 - Updated crouch restriction in update to check if player is not crouched

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement_MarioFernandes : MonoBehaviour
{
    protected Rigidbody2D m_rb;
    
    protected PlayerStealth_JoaoBeijinho m_playerStealthScript;

    //Direction the character is moving
    protected Vector2 m_direction = new Vector2(0, 0);

    public float m_speed = 5.0f;
 
    public float m_baseSpeed = 5.0f;

    public void ResetSpeed()
    {
        m_speed = m_baseSpeed;
    }
    
    //Recieves vector from the PlayerController script and is assigned to the m_direction vector
    public void RecieveVector(Vector2 vector)
    {
        m_direction = vector;
    }

    void Start()
    {
        ResetSpeed();
        Physics2D.gravity = Vector2.zero;
        m_rb = GetComponent<Rigidbody2D>();
        m_playerStealthScript = GetComponent<PlayerStealth_JoaoBeijinho>();
    }
  
    void Update()
    {
        if (!m_playerStealthScript.m_crouched)
        {
            m_direction.Normalize();
            m_direction *= m_speed;

            m_rb.velocity = m_direction;
        }
    }
}
