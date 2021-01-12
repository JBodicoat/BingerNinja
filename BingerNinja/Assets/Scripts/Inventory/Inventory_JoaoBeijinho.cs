//Joao Beijinho

//Joao Beijinho 26/10/2020 - Created this script along with GiveItem(), CheckForItem() and RemoveItem() functions
//Joao Beijinho 29/10/2020 - Started using struct and enum
//Joao Beijinho 30/10/2020 - Removed Struct and added dictionary
//Joao Beijinho 02/10/2020 - Updated HasItem to get the quantity too, created RemoveItem function
//Mario Fernandes 28/10/2020 - Update Pizza and cookies to Dango and Tempura
// Alanna & Elliott 07/12/20 - making a list for enemies to keep track of what enemies are left for ninja points CheckDeadEnemies()

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
    LiftKey,
    Tempura,
    Dango,
    Sake,
    Noodles
}

/// <summary>
/// This class contains a list, aka Inventory, along with functions to interact with the items
/// </summary>
public class Inventory_JoaoBeijinho : MonoBehaviour
{
   List<GameObject> m_EnemyList;
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
        //ItemType removeItem = HasItem();
        if (HasItem(itemToRemove, quantityToRemove))
        {
            m_inventoryItems[itemToRemove] -= quantityToRemove;
        }
    }

    public int ItemValue(ItemType item)
    {
        int itemval;
        m_inventoryItems.TryGetValue(item, out itemval);
        return itemval;
    }

    private void Start()
    {
         m_EnemyList = new List<GameObject>(GameObject.FindGameObjectsWithTag("Enemy"));
         
         // Load inventory
         InventoryData inventoryData = SaveLoadSystem_JamieG.LoadInventory();
         if (!inventoryData.Equals(default(InventoryData)))
         {
             foreach (ItemData itemData in inventoryData.m_items)
             {
                 GiveItem(itemData.m_type, itemData.m_amount);
             }
         }
    }
    
    public void CheckDeadEnemies()
    {
        int count = 0;
        for (int i = 0; i < m_EnemyList.Count; i++)
        {
            if (!m_EnemyList[i].activeSelf)
            {
                count++;
            }
            else break;
        }
        if(count == m_EnemyList.Count)
        {
            Debug.Log(ItemValue(ItemType.NinjaPoints));
            GiveItem(ItemType.NinjaPoints, 5);
            Debug.Log(ItemValue(ItemType.NinjaPoints));
        }
    }
}
