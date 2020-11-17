//Joao Beijinho

//Joao Beijinho 30/10/2020 - Created This script
//Joao Beijinho 02/11/2020 - Remove required key from inventory after its use, added m_keysRequired variable, replaced gameObject.trigger to gameObject.collider.enabled

//Chase Wilding 02/11/2020 - Added itemNeeded & disabled door when key used
//Joao Beijinho 09/11/2020 - Replaced tags with the tags in the Tags_JoaoBeijinho script

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
    public ItemType itemNeeded;

    private void OnCollisionEnter2D(Collision2D collision)
    {

        if (collision.gameObject.CompareTag(Tags_JoaoBeijinho.m_playerTag) && m_inventory.HasItem(itemNeeded, m_keysRequired))//Collision with Player and key check in inventory
        {
            gameObject.SetActive(false);
            m_inventory.RemoveItem(ItemType.Key, m_keysRequired);
        }
    }

    void Start()
    {
        m_inventory = FindObjectOfType<Inventory_JoaoBeijinho>();    
    }
}
