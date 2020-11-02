///Louie Williamson
///This script handles the animation states of the player and communicated with the player animation controller to do so.

//Louie 02/11/2020 - First created

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation_LouieWilliamson : MonoBehaviour
{
    // Start is called before the first frame update
    private bool isFacingLeft;
    private Animator playerAnim;
    private Rigidbody2D rb;

    void Start()
    {
        isFacingLeft = true;

        playerAnim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        
    }

    // Update is called once per frame
    void Update()
    {
        //Is he moving?
        if (rb.velocity.x != 0 || rb.velocity.y != 0)
        {
            playerAnim.SetTrigger("isMoving");
        }
        else
        {
            playerAnim.SetTrigger("isIdle");
        }

        //Is he moving to the left
        if (rb.velocity.x < 0)
        {
            //is he already facing left?
            if (!isFacingLeft)
            {
                transform.localScale = new Vector3(transform.localScale.x * -1, transform.localScale.y, transform.localScale.z);
                isFacingLeft = true;
            }
        }
        else
        {
            if (isFacingLeft)
            {
                transform.localScale = new Vector3(transform.localScale.x * -1, transform.localScale.y, transform.localScale.z);
                isFacingLeft = false;
            }
        }

        //Is he moving upwards
        if (rb.velocity.y < 0)
        {
            playerAnim.SetBool("isFacing" , true);
        }
        else if (rb.velocity.y > 0)
        {
            playerAnim.SetBool("isFacing", false);
        }
        
    }
}
