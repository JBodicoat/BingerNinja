//Joao Beijinho

//Joao Beijinho 26/10/2020 - Created this script along with GiveItem(), CheckForItem() and RemoveItem() functions
//Joao Beijinho 29/10/2020 - Started using struct and enum
//Joao Beijinho 30/10/2020 - Removed Struct and added dictionary

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// List of all pickuble items
/// </summary>
public enum ItemType
{
    Key,
    LiftKey,
    type2,
}

/// <summary>
/// This class contains a list, aka Inventory, along with functions to interact with the items
/// </summary>
public class Inventory_JoaoBeijinho : MonoBehaviour
{
    public Dictionary<ItemType, int> m_inventoryItems = new Dictionary<ItemType, int>();

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

        foreach (KeyValuePair<ItemType, int> pair in m_inventoryItems)
        {
           // print("You have: " + pair.Value + " " + pair.Key);
           
        }
            //print(howMany + " " + whatType + " added to Inventory!");
    }
    public bool HasItem(ItemType item)
    {
        //Debug.Log("yay");
        if (m_inventoryItems.ContainsKey(item))
        {
           // Debug.Log("woo");
            return true;
        }
        else
        {
           // Debug.Log("o no");
            return false;
        }

    }

    ///// <summary>
    ///// Use this function to check if an Item is in the Inventory
    ///// </summary>
    //public Item_JoaoBeijinho CheckForItem(int id)
    //{
    //    return m_characterItems.Find(item => item.m_id == id);
    //}
    //
    ///// <summary>
    ///// Use this function to remove an Item from the Inventory
    ///// </summary>
    //public void RemoveItem(int id)
    //{
    //    Item_JoaoBeijinho item = CheckForItem(id);
    //    if (item != null)
    //    {
    //        m_characterItems.Remove(item);
    //        print("Removed from Inventory: " + item.m_name);
    //    }
    //}
}
