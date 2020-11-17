using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour
{
    public weaponType type;

    Weapon_Morgan weaponScript;

    // Start is called before the first frame update
    void Start()
    {
        weaponScript = FindObjectOfType<Weapon_Morgan>();
    }

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if(collision.tag == "Player")
        {
            if(weaponScript.PickupWeapon(type))
            {
                Destroy(gameObject);
			}
		}
	}
}
