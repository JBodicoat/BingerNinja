//Joao Beijinho

//Joao Beijinho 26/10/2020 - Created this script along with the contructors

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This is a contructor class containing the definition of the items that are to go into the inventory
/// </summary>
public class Item_JoaoBeijinho
{
    public int m_id;
    public string m_name;
    public string m_description;

    public Item_JoaoBeijinho(int id, string name, string description)
    {
        this.m_id = id;
        this.m_name = name;
        this.m_description = description;
    }

    public void Item(Item_JoaoBeijinho item)
    {
        this.m_id = item.m_id;
        this.m_name = item.m_name;
        this.m_description = item.m_description;
    }
}
