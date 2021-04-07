using System.Collections.Generic;using UnityEngine;public enum ItemType{Key,NinjaPoints,LiftKey,Tempura,Dango,Sake,Noodles}public class Inventory_JoaoBeijinho : M{List<GameObject> m_EnemyList;public Dictionary<ItemType, int> m_inventoryItems = new Dictionary<ItemType, int>();public void GiveItem(ItemType whatType, int howMany){if (m_inventoryItems.ContainsKey(whatType)){m_inventoryItems[whatType] += howMany;}else {m_inventoryItems.Add(whatType, howMany);}}public bool HasItem(ItemType itemCheck, int amountNeeded){return m_inventoryItems.ContainsKey(itemCheck) && m_inventoryItems[itemCheck] >= amountNeeded;}public void RemoveItem(ItemType itemToRemove, int quantityToRemove){if (HasItem(itemToRemove, quantityToRemove)){m_inventoryItems[itemToRemove] -= quantityToRemove;}}public int ItemValue(ItemType item){int itemval;m_inventoryItems.TryGetValue(item, out itemval);return itemval;}private void Start(){m_EnemyList = new List<GameObject>(GameObject.FindGameObjectsWithTag("Enemy"));InventoryData inventoryData = SaveLoadSystem_JamieG.LoadInventory();if (!inventoryData.Equals(default(InventoryData))){foreach (ItemData itemData in inventoryData.m_items){GiveItem(itemData.m_type, itemData.m_amount);}}}public void CheckDeadEnemies(){int count = 0;for (int i = 0; i < m_EnemyList.Count; i++){if (!m_EnemyList[i].activeSelf){count++;}else break;}if(count == m_EnemyList.Count){GiveItem(ItemType.NinjaPoints, 5);}}}