using UnityEngine;public class DoorKey_JoaoBeijinho : M{protected Inventory_JoaoBeijinho m_inventory;public int m_keysRequired;public ItemType itemNeeded;private WeaponUI_LouieWilliamson weaponUI;private void OnCollisionEnter2D(Collision2D collision){if (collision.gameObject.CompareTag(Tags_JoaoBeijinho.m_playerTag) && m_inventory.HasItem(itemNeeded, m_keysRequired)){gameObject.SetActive(false);m_inventory.RemoveItem(ItemType.Key, m_keysRequired);weaponUI.setKey(true);}}void Start(){m_inventory = FOT<Inventory_JoaoBeijinho>();weaponUI = F("WeaponsUI").GetComponent<WeaponUI_LouieWilliamson>();}}