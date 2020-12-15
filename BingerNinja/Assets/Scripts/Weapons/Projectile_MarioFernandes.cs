///Mário Fernandes

// Mario 02/11/2020 - Create the Class
// Jack 02/11/2020 Changed "other.GetComponent<EnemyAi>().Hit(m_dmg);" to
//                         "other.GetComponent<BaseEnemy_SebastianMol>().TakeDamage(m_dmg);" in OnTriggerEnter2d
//                         changed GetComponent in above to GetComponentInParent to support new EnemyCollider child on enemy prefabs
//                         EnemyCollider child needed because otherwise projectiles collide with enemy view cone triggers
//sebastian mol 05/11/20 changed enemy take damage function call
// Mario 13/11/2020 - Add Distraction time to progectile
// Mario 14/11/2020 - Solve distraction bugs
// Mario 21/11/2020 - Dettect Wall 2

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

///<Summary>
///This class serves as the template for all the projectiles
///<Summary>
public class Projectile_MarioFernandes : MonoBehaviour
{
    public int m_dmg = 0;
    public float m_speed = 0;

    public float m_distractTime =0;

    float m_timeAlive = 3;

    public Vector3 m_direction;

    private PlayerController_JamieG Controller;
 

    // Start is called before the first frame update
    void Start()
    {
        transform.rotation = Quaternion.Euler(new Vector3(m_direction.x, m_direction.y, 0));         
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.position += m_direction * m_speed * Time.deltaTime ;
    }

     void Update() {
        if(m_timeAlive <= 0)
        Destroy(gameObject);
        else
        m_timeAlive -= Time.deltaTime;
    }

    private void OnTriggerEnter2D(Collider2D other) {
         print(other.gameObject.name);

        if(!other.isTrigger && other.tag == Tags_JoaoBeijinho.m_enemyTag)
        {
           
            other.GetComponentInParent<BaseEnemy_SebastianMol>().TakeDamage( m_damageType.RANGE ,m_dmg);

            if(m_distractTime >0)
            {
                other.GetComponentInParent<BaseEnemy_SebastianMol>().StunEnemyWithDeleyFunc(m_distractTime);
                //other.GetComponentInParent<BaseEnemy_SebastianMol>().TakeDamage(m_damageType.RANGE ,m_dmg);
           
                //StartCoroutine(other.GetComponentInParent<BaseEnemy_SebastianMol>().StunEnemyWithDeley(m_distractTime));
            }

            Destroy(gameObject);
        }else 
        if(other.isTrigger && other.GetComponent<Renderer>() && other.GetComponent<Renderer>().sortingLayerName == "Walls2")
        {
            Destroy(gameObject);
        }
    }
}
