using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile_MarioFernandes : MonoBehaviour
{
    public int m_dmg = 0;
    public float m_speed = 0;

    Vector3 mousePos;
    Vector3 direction;
 

    // Start is called before the first frame update
    void Start()
    {
         mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        
        mousePos.z = 0; 

        mousePos.Normalize();

        transform.rotation = Quaternion.Euler(new Vector3(mousePos.x, mousePos.y, 0));

        transform.position = GameObject.FindWithTag("Player").transform.position;
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += mousePos * m_speed * Time.deltaTime ;
    }

    private void OnTriggerEnter2D(Collider2D other) {
        
        if(other.tag == "Enemy")
        {
            GetComponent<BaseEnemy_SebastianMol>().TakeDamage(m_dmg);
            Destroy(gameObject);
        }
    }
}
