//Joao Beijinho

//Joao Beijinho 26/10/2020 - Created this script and the trigger for pickUp
//Joao Beijinho 29/10/2020 - update

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This class allows to pick up an item on triggerEnter and store it in the inventory
/// </summary>
public class PickupItem_JoaoBeijinho : MonoBehaviour
{
    public Inventory_JoaoBeijinho m_inventory;
    public ItemType m_item;
    private GameObject m_player;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            m_inventory.GiveItem(m_item, 1);
            gameObject.SetActive(false);
        }
    }
}
