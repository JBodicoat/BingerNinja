using UnityEngine;public class PickupItem_JoaoBeijinho : M{public Inventory_JoaoBeijinho m_inventory;public ItemType m_item;public int m_itemQuantity;WeaponUI_LouieWilliamson b;void OnTriggerEnter2D(Collider2D c){if (c.tag == "Player"){m_inventory.GiveItem(m_item, m_itemQuantity);gameObject.SetActive(false);if (m_item == ItemType.Key || m_item == ItemType.LiftKey){b.setKey(true);}}}void Start(){b = F("WeaponsUI").GetComponent<WeaponUI_LouieWilliamson>();}}