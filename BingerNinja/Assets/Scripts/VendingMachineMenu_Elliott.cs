//Elliott Desouza
/// class holds values for the Vendingmachine menu

//Elliott 31/10/2020 - can now select an item which brings up a discription about said item, also adds item to inventory when item is brought
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class VendingMachineMenu_Elliott : MonoBehaviour
{
    public GameObject PizzaInfoText;
    public GameObject CookieInfoText;
    public GameObject SakeInfoText;
    public GameObject NoodleInfoText;
    protected Inventory_JoaoBeijinho Inventory;
    
    /// <summary>
    /// shuts all decriptions and opens up the one selected
    /// </summary>
    public void Pizza()
    {
       PizzaInfoText.SetActive (true);
       CookieInfoText.SetActive(false);      
       SakeInfoText.SetActive(false);
       NoodleInfoText.SetActive(false);
    }

    public void Cookie()
    {
        PizzaInfoText.SetActive(false);
        CookieInfoText.SetActive(true);
        SakeInfoText.SetActive(false);
        NoodleInfoText.SetActive(false);
    }

    public void Sake()
    {
        PizzaInfoText.SetActive(false);
        CookieInfoText.SetActive(false);
        SakeInfoText.SetActive(true);
        NoodleInfoText.SetActive(false);
    }

    public void Noodle()
    {
        PizzaInfoText.SetActive(false);
        CookieInfoText.SetActive(false);
        SakeInfoText.SetActive(false);
        NoodleInfoText.SetActive(true);
    }

    public void Upgrade()
    {
        if(PizzaInfoText.activeInHierarchy /*&& ninja point >= 15*/)
        {
            Debug.Log("pizza");
            //take away ninja points      
            Inventory.GiveItem("Pizza");
        }
        else if (CookieInfoText.activeInHierarchy /*&& ninja point >= 50*/)
        {
            Debug.Log("cookies");
            //take away ninja points
            Inventory.GiveItem("Cookies");
        }
        else if (SakeInfoText.activeInHierarchy /*&& ninja point >= 25*/)
        {
            Debug.Log("sake");
            //take away ninja points
            Inventory.GiveItem("Sake");
        }
        else if (NoodleInfoText.activeInHierarchy /*&& ninja point >= 50*/)
        {
            Debug.Log("noodles");
            //take away ninja points
            Inventory.GiveItem("Noodles");
        }
    }

    void Start()
    {
        Inventory = GameObject.FindObjectOfType<Inventory_JoaoBeijinho>();
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
            Upgrade();
        }
    }
}