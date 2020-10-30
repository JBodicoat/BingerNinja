//Joao Beijinho

//Joao Beijinho 26/10/2020 - Created this script and the trigger for pickUp

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This class allows to pick up an item on triggerEnter adn store it in the inventory
/// </summary>
public class PickupItem_JoaoBeijinho : MonoBehaviour
{
    protected Inventory_JoaoBeijinho m_inventory;
    private GameObject m_player;
    private string m_itemName;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            m_inventory.GiveItem(m_itemName);
            gameObject.transform.parent = m_player.transform;
            gameObject.GetComponent<SpriteRenderer>().enabled = false;
        }
    }

    void Awake()
    {
        m_player = GameObject.FindGameObjectWithTag("Player");
        m_inventory = m_player.GetComponent<Inventory_JoaoBeijinho>();
        m_itemName = gameObject.name;
    }
}
