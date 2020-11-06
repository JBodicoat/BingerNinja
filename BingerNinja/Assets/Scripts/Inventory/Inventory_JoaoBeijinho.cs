//Joao Beijinho

//Joao Beijinho 26/10/2020 - Created this script along with GiveItem(), CheckForItem() and RemoveItem() functions
//Joao Beijinho 29/10/2020 - Started using struct and enum
//Joao Beijinho 30/10/2020 - Removed Struct and added dictionary
//Joao Beijinho 02/10/2020 - Updated HasItem to get the quantity too, created RemoveItem function

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// List of all pickuble items
/// </summary>
public enum ItemType
{
    Key,
    NinjaPoints,
}

/// <summary>
/// This class contains a list, aka Inventory, along with functions to interact with the items
/// </summary>
public class Inventory_JoaoBeijinho : MonoBehaviour
{
    public Dictionary<ItemType, int> m_inventoryItems = new Dictionary<ItemType, int>();

    /// <summary>
    /// Use this function to add a type of item and its quantity to the inventory
    /// </summary>
    public void GiveItem(ItemType whatType, int howMany)
    {
        if (m_inventoryItems.ContainsKey(whatType))
        {

            m_inventoryItems[whatType] += howMany;
        }
        else
        {
            m_inventoryItems.Add(whatType, howMany);
        }
    }

    /// <summary>
    /// Check if the inventory contains a certain item and that item's quantity
    /// </summary>
    public bool HasItem(ItemType itemCheck, int amountNeeded)
    {
            return m_inventoryItems.ContainsKey(itemCheck) && m_inventoryItems[itemCheck] >= amountNeeded;
    }

    ///// <summary>
    /// Use this function to remove a specific Quantity of an Item from the Inventory
    /// </summary>
    public void RemoveItem(ItemType itemToRemove, int quantityToRemove)
    {
        if (HasItem(itemToRemove, quantityToRemove))
        {
            m_inventoryItems[itemToRemove] -= quantityToRemove;
        }
    }
}
