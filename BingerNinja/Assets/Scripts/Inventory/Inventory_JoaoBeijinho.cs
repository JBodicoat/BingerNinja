//Joao Beijinho

//Joao Beijinho 26/10/2020 - Created this script along with GiveItem(), CheckForItem() and RemoveItem() functions

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This class contains a list, aka Inventory, along with functions to interact with the items
/// </summary>
public class Inventory_JoaoBeijinho : MonoBehaviour
{
    public List<Item_JoaoBeijinho> m_characterItems = new List<Item_JoaoBeijinho>();
    public ItemDatabase_JoaoBeijinho m_itemDatabase;

    /// <summary>
    /// Use this function to add an Item from the Item DataBase to the Inventory
    /// </summary>
    public void GiveItem(string itemName)
    {
        Item_JoaoBeijinho itemToAdd = m_itemDatabase.GetItem(itemName);
        m_characterItems.Add(itemToAdd);
        print("Added to Inventory: " + itemToAdd.m_name);
    }

    /// <summary>
    /// Use this function to check if an Item is in the Inventory
    /// </summary>
    public Item_JoaoBeijinho CheckForItem(int id)
    {
        return m_characterItems.Find(item => item.m_id == id);
    }

    /// <summary>
    /// Use this function to remove an Item from the Inventory
    /// </summary>
    public void RemoveItem(int id)
    {
        Item_JoaoBeijinho item = CheckForItem(id);
        if (item != null)
        {
            m_characterItems.Remove(item);
            print("Removed from Inventory: " + item.m_name);
        }
    }
}
