///Mário Fernandes

// Mario 02/11/2020 - Create the Class
// Jack 02/11/2020 Changed "other.GetComponent<EnemyAi>().Hit(m_dmg);" to
//                         "other.GetComponent<BaseEnemy_SebastianMol>().TakeDamage(m_dmg);" in OnTriggerEnter2d
//                         changed GetComponent in above to GetComponentInParent to support new EnemyCollider child on enemy prefabs
//                         EnemyCollider child needed because otherwise projectiles collide with enemy view cone triggers
//sebastian mol 05/11/20 changed enemy take damage function call
// Mario 13/11/2020 - Add Distraction time to progectile
// Mario 14/11/2020 - Solve distraction bugs

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

    Vector3 m_mousePos;
    Vector3 m_direction;
 

    // Start is called before the first frame update
    void Start()
    {
        m_mousePos = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
        
        m_mousePos = m_mousePos- transform.position;

        m_mousePos.z = 0; 

        m_mousePos.Normalize();       
        

        transform.rotation = Quaternion.Euler(new Vector3(m_mousePos.x, m_mousePos.y, 0));        
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.position += m_mousePos * m_speed * Time.deltaTime ;
    }

     void Update() {
        if(m_timeAlive <= 0)
        Destroy(gameObject);
        else
        m_timeAlive -= Time.deltaTime;
    }

    private void OnTriggerEnter2D(Collider2D other) {
        
        if(!other.isTrigger && other.tag == "Enemy")
        {
            other.GetComponentInParent<BaseEnemy_SebastianMol>().TakeDamage( m_damageType.RANGE ,m_dmg);

            if(m_distractTime >0)
            {
                other.GetComponentInParent<BaseEnemy_SebastianMol>().StunEnemyWithDeleyFunc(m_distractTime);
            other.GetComponentInParent<BaseEnemy_SebastianMol>().TakeDamage(m_damageType.RANGE ,m_dmg);
           
                StartCoroutine(other.GetComponentInParent<BaseEnemy_SebastianMol>().StunEnemyWithDeley(m_distractTime));
            }

            Destroy(gameObject);
        }
    }
}
