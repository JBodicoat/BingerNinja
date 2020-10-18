// Mário Fernandes
/// My class takes care of the movement of the character

///!!!!!!!!!!!!Needs Rigidbody2D to Work!!!!!!!!!!

// Mário 16/10/2020 - Abstract Movement code from old Character class
// Mário 18/10/2020 - Add reset speed function
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement_MarioFernandes : MonoBehaviour
{
    protected Rigidbody2D m_rb;

    //Direction the character is moving
    protected Vector2 m_direction = new Vector2(0, 0);

    public float m_speed = 5.0f;

    public float m_baseSpeed = 5.0f;

    public void ResetSpeed()
    {
        m_speed = m_baseSpeed;
    }

#region Movement Directions
    public void MoveUp()
    {
        m_direction.y = 1;
    }

    public void MoveDown()
    {
        m_direction.y = -1;
    }

    public void MoveLeft()
    {
        m_direction.x = -1;
    }

    public void MoveRight()
    {
        m_direction.x = 1;
    }
#endregion

    void Start()
    {
        ResetSpeed();
        Physics2D.gravity = Vector2.zero;
        m_rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
#region Movement input
        var curKeyboard= Keyboard.current;

        // Movement
        m_direction = new Vector2(0, 0);

        if (curKeyboard.aKey.isPressed)
        {
            MoveLeft();
		}
        else if(curKeyboard.dKey.isPressed)
        {
            MoveRight();
		}

        if (curKeyboard.wKey.isPressed)
        {
            MoveUp();
        }
        else if(curKeyboard.sKey.isPressed)
        {
            MoveDown();
		}

        m_direction.Normalize();
		m_direction *= m_speed;

		m_rb.velocity = m_direction;
#endregion
    }
}
