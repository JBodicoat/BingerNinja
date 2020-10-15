using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletMovment : MonoBehaviour
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
            FindObjectOfType<Character>().Hit(20);
            Destroy(gameObject);
		}
	}
}
