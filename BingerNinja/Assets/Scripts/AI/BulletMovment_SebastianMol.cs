// Jack 20/10 changed to support new PlayerHealthAndHunger script

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

    private void Start()
    {
        Destroy(gameObject, 2);
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
