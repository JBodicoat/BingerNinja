//Elliott Desouza
/// class holds values for the Vendingmachine menu

//Elliott 31/10/2020 - can now select an item which brings up a discription about said item, also adds item to inventory when item is brought
//Elliott 06/10/2020 - added a check for the one time buy items, made a DeselectAll fuction.
//Elliott 09/10/2020 - changed the color of the text when item is brought changes, give item now uses enum.
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
    private Color newcolor;
    private bool PurchasedCookie = false;
    private bool PurchasedNoodle = false;
    public ChangeLevels_CW levelLift;
    public Text Yen;
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
        if (SelectPizza.activeInHierarchy && Inventory.HasItem(ItemType.NinjaPoints, 15))
        {
            Debug.Log("Tempura");  
            Inventory.RemoveItem(ItemType.NinjaPoints, 15);
            Inventory.GiveItem(ItemType.Tempura, 1); 
        }
        else if (SelectCookie.activeInHierarchy && !PurchasedCookie && Inventory.HasItem(ItemType.NinjaPoints, 50)) 
        {
            Debug.Log("Dango");
            Inventory.RemoveItem(ItemType.NinjaPoints, 50);
            Inventory.GiveItem(ItemType.Dango, 1);
            CookieColor.color = Color.red;
            PurchasedCookie = true;
 
        }
        else if (SelectSake.activeInHierarchy && Inventory.HasItem(ItemType.NinjaPoints,25))
        {
            Debug.Log("sake");
            Inventory.RemoveItem(ItemType.NinjaPoints, 25);
            Inventory.GiveItem(ItemType.Sake, 1);
        }
        else if (SelectNoodle.activeInHierarchy && !PurchasedNoodle && Inventory.HasItem(ItemType.NinjaPoints, 50)) /*&& ninja point >= 50*/
        {
            Debug.Log("noodles");
            Inventory.RemoveItem(ItemType.NinjaPoints, 50);
            Inventory.GiveItem(ItemType.Noodles, 1);
            NoodleColor.color = Color.red;
            PurchasedNoodle = true;

        }
    }

    void Start()
    {
        Inventory = GameObject.FindObjectOfType<Inventory_JoaoBeijinho>();
        Inventory.GiveItem(ItemType.NinjaPoints, 40);
    }

    /// <summary>
    /// press 1 - 4 to select the item and press 5 to buy.
    /// </summary>
    void Update()
    {
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
        if (gamepad.digit6Key.wasPressedThisFrame)
        {
            levelLift.ProgressToNextLevel();
        }
    }
}