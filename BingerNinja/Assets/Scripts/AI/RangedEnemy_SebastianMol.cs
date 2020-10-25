using System.Collections;
using System.Collections.Generic;
using UnityEngine;

class RangedEnemy_SebastianMol : BaseEnemy_SebastianMol
{
    [Header("projectile prefabs")]
    public GameObject aimer;
    public GameObject bullet;
    [Header("projectile Variables")]
    [Tooltip("how fast the projectile moves")]
    public float shootDeley;
    [Tooltip("speed of the projectile")]
    public float projectileSpeed;
    [Tooltip("how far the player can be befor enemy shoots")]
    public float shootingRange;
    private float timer;

    internal override void EnemyBehaviour()
    {
        IsPlayerDetected();
        switch (currentState)
        {
            case state.WONDER:
                //move form one postion to another
                break;

            case state.CHASE:
                //move towards the player if he has been detected
                if (Vector2.Distance(transform.position, playerTransform.position) < shootingRange)
                {
                    currentState = state.ATTACK;
                }
                break;

            case state.ATTACK:
                //personalized attack
                RangedAttack();
                Debug.Log("attacking");
                break;

            case state.RETREAT:
                if (Vector2.Distance(transform.position, playerTransform.position) < shootingRange)
                {
                    currentState = state.ATTACK;
                }
                break;
        }     
    }

    private void RangedAttack()
    {
        if (Vector2.Distance(transform.position, playerTransform.position) < shootingRange)
        {
            Debug.Log("whitin range");
            if (playerTransform != null)
            {
                Debug.Log("see the player");
                Vector3 dir = Vector3.Normalize(playerTransform.position - transform.position);
                float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
                aimer.transform.eulerAngles = new Vector3(0, 0, angle);
                if (timer <= 0)
                {
                    GameObject projectile = Instantiate(bullet, transform.position, Quaternion.Euler(new Vector3(dir.x, dir.y, 0)));
                    projectile.GetComponent<Rigidbody2D>().velocity = new Vector2(dir.x * projectileSpeed, dir.y * projectileSpeed);
                    timer = shootDeley;
                }
                else
                {
                    timer -= Time.deltaTime;
                }
            }
            else
            {
                currentState = state.RETREAT;
            }
        }
        else 
        {
            currentState = state.CHASE;
        }
    }
    
   void OnDrawGizmosSelected()
   {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, shootingRange);
   }
}
