// Jack 20/10 changed to support new PlayerHealthAndHunger script
// Jack 02/11/2020 added damage dealt as a variable replacing magic number
// Louie 03/11/2020 added player damage sfx

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// used to move the projectile used by enemies
/// </summary>
public class BulletMovment_SebastianMol : MonoBehaviour
{
    public float m_speed;
    internal Vector2 m_direction;
    public float m_damage;

    public float m_damageDealt;
    private AudioManager_LouieWilliamson m_audioManager;

    private void Start()
    {
        Destroy(gameObject, 2);
        m_audioManager = FindObjectOfType<AudioManager_LouieWilliamson>();
    }
    void Update()
    {
        transform.position += (Vector3)m_direction * m_speed * Time.deltaTime ;
    }

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if(collision.tag == "Player")
        {
           FindObjectOfType<PlayerHealthHunger_MarioFernandes>().Hit(m_damage);
            Destroy(gameObject);
		}
	}
}
