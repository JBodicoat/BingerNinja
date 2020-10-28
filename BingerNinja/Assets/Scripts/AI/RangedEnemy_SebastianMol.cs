using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.UI;
using UnityEngine.Networking;
using UnityEngine.Tilemaps;

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
    public Vector3 pos;
    public bool button = false;
    public bool button2 = false;



    internal override void EnemyBehaviour()
    {
        if (button)
        {
            MoveToWorldPos(pos);
            button = false;
        }

        if(button2)
        {
            ClearPath();
            button2 = false;
        }


        switch (m_currentState)
        {
            case state.WONDER:
                //move form one postion to another
                if (m_playerDetected) m_currentState = state.CHASE;
                break;

            case state.CHASE:
                //move towards the player if he has been detected
                if (Vector2.Distance(transform.position, m_playerTransform.position) < shootingRange/2)
                {
                    ClearPath();
                    m_currentState = state.ATTACK;
                }
                else
                {
                    if(m_currentPath.Count == 0)
                    {
                        MoveToWorldPos(m_playerTransform.position);
                    }
                    FollowPath();
                }
                break;

            case state.ATTACK:
                //personalized attack
                RangedAttack();
                Debug.Log("attacking");
                break;

            case state.RETREAT:
                //if (Vector2.Distance(transform.position, m_playerTransform.position) < shootingRange)
                //{
                //    m_currentState = state.ATTACK;
                //}
                break;
        }     
    }
    private void RangedAttack()
    {
        if (Vector2.Distance(transform.position, m_playerTransform.position) < shootingRange)
        {
            Debug.Log("whitin range");
            if (m_playerTransform != null)
            {
                Debug.Log("see the player");
                Vector3 dir = Vector3.Normalize(m_playerTransform.position - transform.position);
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
            //else
            //{
            //    m_currentState = state.RETREAT;
            //}
        }
        else 
        {
            m_currentState = state.CHASE;
        }
    }

    void OnDrawGizmosSelected()
   {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, shootingRange);
   }
}
