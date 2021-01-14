//Elliott Desouza
/// class holds values for the Vendingmachine menu

//Elliott 31/10/2020 - can now select an item which brings up a discription about said item, also adds item to inventory when item is brought
//Elliott 06/11/2020 - added a check for the one time buy items, made a DeselectAll fuction.
//Elliott 09/11/2020 - changed the color of the text when item is brought changes, give item now uses enum.
// Alanna & Elliott 07/12/20- Merged Alanna and Elliotts vending machine code, made the UI work with buttons and formatting, setting ninja points to yen.

using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class VendingMachineMenu_Elliott : MonoBehaviour
{
    public Text NoodleColor;
    public Text CookieColor;    
    public GameObject SelectPizza;
    public GameObject SelectCookie;
    public GameObject SelectSake;
    public GameObject SelectNoodle;
    protected Inventory_JoaoBeijinho Inventory;
     Color c;
     bool a = false;
     bool b = false;
    public ChangeLevels_CW levelLift;
    public Text Yen;
    public static bool m_gameIsPaused = false;

   



    /// <summary>
    /// shuts all decriptions and opens up the one selected
    /// </summary>
    public void DeSelectAll()
    {
        SelectPizza.SetActive(false);
        SelectCookie.SetActive(false);
        SelectSake.SetActive(false);
        SelectNoodle.SetActive(false);
    }
    public void Pizza()
    {
       DeSelectAll();
       SelectPizza.SetActive (true);
    }

    public void Cookie()
    {
        DeSelectAll();
        SelectCookie.SetActive(true);
    }

    public void Sake()
    {
        DeSelectAll();
        SelectSake.SetActive(true);
    }

    public void Noodle()
    {
        DeSelectAll();
        SelectNoodle.SetActive(true);
    }
    public void AdvanceLevel()
    {
        levelLift.ProgressToNextLevel();
    }
    public void PurchaseUpgrade()
    {
        if (SelectPizza.activeInHierarchy && Inventory.HasItem(ItemType.NinjaPoints, 15))
        {
            Inventory.RemoveItem(ItemType.NinjaPoints, 15);
            Inventory.RG(ItemType.Tempura, 1); 
        }
        else if (SelectCookie.activeInHierarchy && !a && Inventory.HasItem(ItemType.NinjaPoints, 50)) 
        {
            Inventory.RemoveItem(ItemType.NinjaPoints, 50);
            Inventory.RG(ItemType.Dango, 1);
            CookieColor.color = Color.red;
            a = true;
 
        }
        else if (SelectSake.activeInHierarchy && Inventory.HasItem(ItemType.NinjaPoints,25))
        {
            Inventory.RemoveItem(ItemType.NinjaPoints, 25);
            Inventory.RG(ItemType.Sake, 1);
        }
        else if (SelectNoodle.activeInHierarchy && !b && Inventory.HasItem(ItemType.NinjaPoints, 50)) /*&& ninja point >= 50*/
        {
            Inventory.RemoveItem(ItemType.NinjaPoints, 50);
            Inventory.RG(ItemType.Noodles, 1);
            NoodleColor.color = Color.red;
            b = true;

        }
    }
    void UIDisable()
    {
        GameObject[] m_UI = GameObject.FindGameObjectsWithTag(Tags_JoaoBeijinho.m_UI);
        if (gameObject.activeInHierarchy)
        {
            foreach (var UI_object in m_UI)
            {
                UI_object.SetActive(false);
                
            }
        }
    }
    //void UIEnable()
    //{
    //    if (!gameObject.activeInHierarchy)
    //    {
    //        foreach (var UI_object in m_UI)
    //        {
    //            UI_object.SetActive(true);

    //        }
    //    }
    //}

    void Start()
    {
        Inventory = GameObject.FindObjectOfType<Inventory_JoaoBeijinho>();
        Inventory.RG(ItemType.NinjaPoints, 40);
       
    }

    /// <summary>
    /// press 1 - 4 to select the item and press 5 to buy.
    /// </summary>
    void Update()
    {
        UIDisable();
        Yen.text = "Yen: " + Inventory.ItemValue(ItemType.NinjaPoints).ToString();

        var gamepad = Keyboard.current;

        if (gamepad == null)
            return;
        if (gamepad.digit1Key.wasPressedThisFrame)
        {
            Pizza();
        }
        if (gamepad.digit2Key.wasPressedThisFrame)
        {
            Cookie();
        }
        if (gamepad.digit3Key.wasPressedThisFrame)
        {
            Sake();
        }
        if (gamepad.digit4Key.wasPressedThisFrame)
        {
            Noodle();
        }
        if (gamepad.digit5Key.wasPressedThisFrame)
        {
            PurchaseUpgrade();
        }
        if (gamepad.escapeKey.wasPressedThisFrame)
        {
            //UIEnable();
            AdvanceLevel();
            
        }
    }
}