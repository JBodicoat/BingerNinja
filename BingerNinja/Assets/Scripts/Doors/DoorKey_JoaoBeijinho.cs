//Joao Beijinho

//Joao Beijinho 30/10/2020 - Created This script
//Joao Beijinho 02/11/2020 - Remove required key from inventory after its use, added m_keysRequired variable, replaced gameObject.trigger to gameObject.collider.enabled

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This class checks if there is a key in the inventory, if there is, it opens
/// </summary>
public class DoorKey_JoaoBeijinho : MonoBehaviour
{
    protected Inventory_JoaoBeijinho m_inventory;

    public int m_keysRequired;
    private string m_playerTag = "Player";

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name == m_playerTag && m_inventory.HasItem(ItemType.Key, m_keysRequired))//Collision with Player and key check in inventory
        {
            gameObject.GetComponent<Collider2D>().enabled = false;
            m_inventory.RemoveItem(ItemType.Key, m_keysRequired);
        }
    }

    void Start()
    {
        m_inventory = FindObjectOfType<Inventory_JoaoBeijinho>();    
    }
}
