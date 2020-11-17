using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SquidWeapon_Morgan : MonoBehaviour
{
    //weapon states
    internal bool isPreparing = false;
    internal bool isAttacking = false;

    public Sprite squidAttack;

    internal Vector2 mouseDirection = new Vector2(0, 0);
    float angle;

    // Update is called once per frame
    void Update()
    {
        if (isPreparing)
        {
            //transform.LookAt(new Vector3(mouseDirection.x, mouseDirection.y, transform.position.z));
        }

        if (isAttacking)
        {
            GetComponent<BoxCollider2D>().enabled = true;
            GetComponent<SpriteRenderer>().sprite = squidAttack;
        }
    }
}