//Joao Beijinho

//Joao Beijinho 26/10/2020 - Created this script along with the BuildDataBase() and GetItem() functions

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This class contains all possible items capable of going into the inventory
/// </summary>
public class ItemDatabase_JoaoBeijinho : MonoBehaviour
{
    public List<Item_JoaoBeijinho> m_items = new List<Item_JoaoBeijinho>();

    void Awake()
    {
        BuildDatabase();
    }

    /// <summary>
    /// This function is called in the inventory script to add an element from the DataBase list to the inventory
    /// </summary>
    public Item_JoaoBeijinho GetItem(string itemName)
    {
        return m_items.Find(item => item.m_name == itemName);
    }

    /// <summary>
    /// List of all the Items that can go in the inventory and their definition
    /// </summary>
    void BuildDatabase()
    {
        m_items = new List<Item_JoaoBeijinho>()
        {
            new Item_JoaoBeijinho(0, "Key", "Opens Door")
        };
    }
}
