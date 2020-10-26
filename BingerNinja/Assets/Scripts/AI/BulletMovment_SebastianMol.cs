// Jack 20/10 changed to support new PlayerHealthAndHunger script

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletMovment_SebastianMol : MonoBehaviour
{
    public float speed;

    private void Start()
    {
        Destroy(gameObject, 2);
    }
    void Update()
    {
        //transform.position += Vector3.up * speed * Time.deltaTime ;
    }

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if(collision.tag == "Player")
        {
            FindObjectOfType<PlayerHealthHunger_MarioFernandes>().Hit(20);
            Destroy(gameObject);
		}
	}
}
