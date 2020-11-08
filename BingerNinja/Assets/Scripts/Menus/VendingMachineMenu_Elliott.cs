//Elliott Desouza
/// class holds values for the Vendingmachine menu

//Elliott 31/10/2020 - can now select an item which brings up a discription about said item, also adds item to inventory when item is brought
//Elliott 06/10/2020 - added a check for the one time buy items, made a DeselectAll fuction.
//Elliott 09/10/2020 - changed the color of the text when item is brought changes, give item now uses enum.
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class VendingMachineMenu_Elliott : MonoBehaviour
{
    public Text NoodleColor;
    public Text CookieColor;    
    public GameObject SelectPizza;
    public GameObject SelectCookie;
    public GameObject SelectSake;
    public GameObject SelectNoodle;
    protected Inventory_JoaoBeijinho Inventory;
    private Color newcolor;
    private bool PurchasedCookie = false;
    private bool PurchasedNoodle = false;

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

    public void PurchaseUpgrade()
    {
        if (SelectPizza.activeInHierarchy /*&& ninja point >= 15*/)
        {
            Debug.Log("pizza");
            //take away ninja points      
            Inventory.GiveItem(ItemType.Pizza, 1);
        }
        else if (SelectCookie.activeInHierarchy && !PurchasedCookie /*&& ninja point >= 50*/)
        {
            Debug.Log("cookies");
            //take away ninja points
            Inventory.GiveItem(ItemType.Cookie, 1);
            CookieColor.color = Color.red;
            PurchasedCookie = true;
            
        }
        else if (SelectSake.activeInHierarchy /*&& ninja point >= 25*/)
        {
            Debug.Log("sake");
            //take away ninja points
            Inventory.GiveItem(ItemType.Sake,1);
        }
        else if (SelectNoodle.activeInHierarchy && !PurchasedNoodle /*&& ninja point >= 50*/)
        {
            Debug.Log("noodles");
            //take away ninja points
            Inventory.GiveItem(ItemType.Noodles,1);
            NoodleColor.color = Color.red;
            PurchasedNoodle = true;
        }
    }

    void Start()
    {
        Inventory = GameObject.FindObjectOfType<Inventory_JoaoBeijinho>();
        //Color newcolor = Color.red;
        //CookieColor.color = Color.red;
    }

    /// <summary>
    /// press 1 - 4 to select the item and press 5 to buy.
    /// </summary>
    void Update()
    {
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
    }
}