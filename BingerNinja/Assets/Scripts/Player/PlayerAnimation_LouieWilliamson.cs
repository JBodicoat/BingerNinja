///Louie Williamson
///This script handles the animation states of the player and communicated with the player animation controller to do so.

//Louie 02/11/2020 - First created
//Louie 30/11/2020 - Roll and crouch animations

using UnityEngine;

public class PlayerAnimation_LouieWilliamson : MonoBehaviour
{
    // Start is called before the first frame update
     bool q;
    //hey 
     Animator w;
     Rigidbody2D e;
    public void QE()
    {
        if (q)
        {
            if (transform.localScale.x < 0)
            {
                FlipSprite();
            }
        }
        w.SetTrigger("isAttacking");
    }
    public void RollAnimation(bool isRolling)
    {
        w.SetBool("isRolling", isRolling);
    }
    public void Crouching(bool isCrouching)
    {
        w.SetBool("isCrouching", isCrouching);
    }
    void Start()
    {
        q = true;

        w = GetComponent<Animator>(); 
        e = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        //Is he moving?
        if (e.velocity.x != 0 || e.velocity.y != 0)
        {
            w.SetTrigger("isMoving");
        }
        else
        {
            w.SetTrigger("isIdle");
        }

        //Is he moving to the left
        if (e.velocity.x < 0)
        {
            //is he already facing left?
            if (!q)
            {
                FlipSprite();
                q = true;
            }
        }
        else if (e.velocity.x > 0)
        {
            if (q)
            {
                FlipSprite();
                q = false;
            }
        }

        //Is he moving upwards
        if (e.velocity.y < 0)
        {
            w.SetBool("isFacing" , true);
        }
        else if (e.velocity.y > 0)
        {
            w.SetBool("isFacing", false);
        }
    }
     void FlipSprite()
    {
        transform.localScale = new Vector3(transform.localScale.x * -1, transform.localScale.y, transform.localScale.z);
    }
}
