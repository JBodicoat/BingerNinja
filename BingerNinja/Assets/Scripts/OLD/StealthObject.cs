using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StealthObject : MonoBehaviour
{
	Character characterScript;

	private void Start()
	{
		characterScript = FindObjectOfType<Character>();
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if(collision.tag == "Player")
        {
			characterScript.isStealthed = true;
		}
	}

	private void OnTriggerExit2D(Collider2D collision)
	{
		if (collision.tag == "Player")
		{
			characterScript.isStealthed = false;
		}
	}
}
