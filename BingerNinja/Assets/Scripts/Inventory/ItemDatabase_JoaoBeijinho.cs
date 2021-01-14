//Joao Beijinho

//Joao Beijinho 26/10/2020 - Created this script along with the BuildDataBase() and GetItem() functions
//Elliott Desouza 31/ 10/2020 - add some items to the list

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
    public Item_JoaoBeijinho GetItem(string i)
    {
        return m_items.Find(item => item.m_name == i);
    }

    /// <summary>
    /// List of all the Items that can go in the inventory and their definition
    /// </summary>
    void BuildDatabase()
    {
        m_items = new List<Item_JoaoBeijinho>()
        {
            new Item_JoaoBeijinho(0, "Key", "Opens Door"),
            new Item_JoaoBeijinho(1, "Pizza", "Gives you strength boost (1.5x) but slows you down (0.5x) for thirty seconds"),
            new Item_JoaoBeijinho(2, "Cookie", "throws two instead of one"),
            new Item_JoaoBeijinho(3, "Sake", "for 30 seconds increase strength (2.0x)"),
            new Item_JoaoBeijinho(4, "Noodles", "decreases strength (0.5x) for 15 seconds / distracts enemy for five seconds")

        };
    }
}
