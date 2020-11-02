// Jack 02/11/2020 Changed "other.GetComponent<EnemyAi>().Hit(m_dmg);" to
//                         "other.GetComponent<BaseEnemy_SebastianMol>().TakeDamage(m_dmg);"
//                         in OnTriggerEnter2d

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Projectile_MarioFernandes : MonoBehaviour
{
    public int m_dmg = 0;
    public float m_speed = 0;

    float timeAlive = 3;

    Vector3 mousePos;
    Vector3 direction;
 

    // Start is called before the first frame update
    void Start()
    {
        mousePos = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
        
        mousePos = mousePos- transform.position;

        mousePos.z = 0; 

        mousePos.Normalize();       
        

        transform.rotation = Quaternion.Euler(new Vector3(mousePos.x, mousePos.y, 0));        
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.position += mousePos * m_speed * Time.deltaTime ;
    }

     void Update() {
        if(timeAlive <= 0)
        Destroy(gameObject);
        else
        timeAlive -= Time.deltaTime;
    }

    private void OnTriggerEnter2D(Collider2D other) {
        
        if(other.tag == "Enemy")
        {
            other.GetComponent<BaseEnemy_SebastianMol>().TakeDamage(m_dmg);
            Destroy(gameObject);
        }
    }
}
