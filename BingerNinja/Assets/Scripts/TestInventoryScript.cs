using System.Collections.Generic;
using UnityEngine;

public class TestInventoryScript : MonoBehaviour
{
    public bool giveCoins;
    public bool testCoins;
    public bool removeCoins;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(giveCoins)
        {
            FindObjectOfType<Inventory_JoaoBeijinho>().GiveItem(ItemType.NinjaPoints, 20);
            print("player given coins");
            giveCoins = false;
            foreach (KeyValuePair<ItemType, int> pair in FindObjectOfType<Inventory_JoaoBeijinho>().m_inventoryItems)
            {
                print("You have: " + pair.Value + " " + pair.Key);
            }
        }

        if(testCoins)
        {
            if (FindObjectOfType<Inventory_JoaoBeijinho>().HasItem(ItemType.NinjaPoints, 40))
                print("We have enough coins");
            testCoins = false;
            foreach (KeyValuePair<ItemType, int> pair in FindObjectOfType<Inventory_JoaoBeijinho>().m_inventoryItems)
            {
                print("You have: " + pair.Value + " " + pair.Key);
            }
        }

        if(removeCoins)
        {
            FindObjectOfType<Inventory_JoaoBeijinho>().RemoveItem(ItemType.NinjaPoints, 10);
            print("coins taken away");
            removeCoins = false;
            foreach (KeyValuePair<ItemType, int> pair in FindObjectOfType<Inventory_JoaoBeijinho>().m_inventoryItems)
            {
                print("You have: " + pair.Value + " " + pair.Key);
            }
        }
    }
}
