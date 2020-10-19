//Elliott Desouza
//My class takes care of Player Death and Respwan

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDeathRespawn_ElliottDesouza : MonoBehaviour
{
    protected float m_currentHealth = 100.0f;
    protected float m_lives = 3;

    private void death()
    {
        if (m_currentHealth == 0)
        {
            if(m_lives == 0)
            {
               
            }
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Enemy"))
        {
            death();
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
