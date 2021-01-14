//Joao Beijinho

//Joao Beijinho 26/10/2020 - Created this script and the trigger for pickUp
//Joao Beijinho 29/10/2020 - update trigger to use new Inventory function, GiveItem()
//Joao Beijinho 02/11/2020 - Added m_itemQuantity int

using UnityEngine;

/// <summary>
/// This class allows to pick up an item on triggerEnter and store it in the inventory
/// </summary>
public class PickupItem_JoaoBeijinho : MonoBehaviour
{
    public Inventory_JoaoBeijinho m_inventory;
    public ItemType m_item;
    public int m_itemQuantity;

    WeaponUI_LouieWilliamson b;
    void OnTriggerEnter2D(Collider2D c)
    {
        if (c.tag == "Player")
        {
            m_inventory.GiveItem(m_item, m_itemQuantity);
            gameObject.SetActive(false);

            if (m_item == ItemType.Key || m_item == ItemType.LiftKey)
            {
                b.setKey(true);
            }
        }
    }
    void Start()
    {
        b = GameObject.Find("WeaponsUI").GetComponent<WeaponUI_LouieWilliamson>();
    }
}

