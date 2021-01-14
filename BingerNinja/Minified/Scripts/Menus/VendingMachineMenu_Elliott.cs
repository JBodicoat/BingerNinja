using UnityEngine;using UnityEngine.InputSystem;using UnityEngine.UI;public class VendingMachineMenu_Elliott : M{public Text NoodleColor;public Text CookieColor;public GameObject SelectPizza;public GameObject SelectCookie;public GameObject SelectSake;public GameObject SelectNoodle;protected Inventory_JoaoBeijinho Inventory;private Color newcolor;private bool PurchasedCookie = false;private bool PurchasedNoodle = false;public ChangeLevels_CW levelLift;public Text Yen;public static bool m_gameIsPaused = false;public void DeSelectAll(){SelectPizza.SetActive(false);SelectCookie.SetActive(false);SelectSake.SetActive(false);SelectNoodle.SetActive(false);}public void Pizza(){DeSelectAll();SelectPizza.SetActive (true);}public void Cookie(){DeSelectAll();SelectCookie.SetActive(true);}public void Sake(){DeSelectAll();SelectSake.SetActive(true);}public void Noodle(){DeSelectAll();SelectNoodle.SetActive(true);}public void AdvanceLevel(){levelLift.ProgressToNextLevel();}public void PurchaseUpgrade(){if (SelectPizza.activeInHierarchy && Inventory.HasItem(ItemType.NinjaPoints, 15)){Inventory.RemoveItem(ItemType.NinjaPoints, 15);Inventory.GiveItem(ItemType.Tempura, 1);}else if (SelectCookie.activeInHierarchy && !PurchasedCookie && Inventory.HasItem(ItemType.NinjaPoints, 50)){Inventory.RemoveItem(ItemType.NinjaPoints, 50);Inventory.GiveItem(ItemType.Dango, 1);CookieColor.color = Color.red;PurchasedCookie = true;}else if (SelectSake.activeInHierarchy && Inventory.HasItem(ItemType.NinjaPoints,25)){Inventory.RemoveItem(ItemType.NinjaPoints, 25);Inventory.GiveItem(ItemType.Sake, 1);}else if (SelectNoodle.activeInHierarchy && !PurchasedNoodle && Inventory.HasItem(ItemType.NinjaPoints, 50)){Inventory.RemoveItem(ItemType.NinjaPoints, 50);Inventory.GiveItem(ItemType.Noodles, 1);NoodleColor.color = Color.red;PurchasedNoodle = true;}}void UIDisable(){GameObject[] m_UI = GameObject.FindGameObjectsWithTag(Tags_JoaoBeijinho.m_UI);if (gameObject.activeInHierarchy){foreach (var UI_object in m_UI){UI_object.SetActive(false);}}}void Start(){Inventory = FOT<Inventory_JoaoBeijinho>();Inventory.GiveItem(ItemType.NinjaPoints, 40);}void Update(){UIDisable();Yen.text = "Yen: " + Inventory.ItemValue(ItemType.NinjaPoints).ToString();var gamepad = Keyboard.current;if (gamepad == null)return;if (gamepad.digit1Key.wasPressedThisFrame){Pizza();}if (gamepad.digit2Key.wasPressedThisFrame){Cookie();}if (gamepad.digit3Key.wasPressedThisFrame){Sake();}if (gamepad.digit4Key.wasPressedThisFrame){Noodle();}if (gamepad.digit5Key.wasPressedThisFrame){PurchaseUpgrade();}if (gamepad.escapeKey.wasPressedThisFrame){AdvanceLevel();}}}