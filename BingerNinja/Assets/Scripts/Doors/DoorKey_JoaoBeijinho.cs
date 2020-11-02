//Joao Beijinho

//Joao Beijinho 30/10/2020 - Created This script

//Chase Wilding 02/11/2020 - Added itemNeeded & disabled door when key used

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This class checks if there is a key in the inventory, if there is, it opens
/// </summary>
public class DoorKey_JoaoBeijinho : MonoBehaviour
{
    protected Inventory_JoaoBeijinho m_inventory;

    public ItemType itemNeeded;

    private string m_playerTag = "Player";

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name == m_playerTag && m_inventory.HasItem(itemNeeded))//Collision with Player
        {
            gameObject.GetComponent<BoxCollider2D>().isTrigger = true;
            gameObject.SetActive(false);
        }
    }

    void Start()
    {
        m_inventory = FindObjectOfType<Inventory_JoaoBeijinho>();    
    }
}
