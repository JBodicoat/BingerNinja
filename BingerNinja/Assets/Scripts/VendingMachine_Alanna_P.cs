using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VendingMachine_Alanna_P : MonoBehaviour
{
    public GameObject Noodles;
    public GameObject Sake;
    public GameObject Tempura;
    public GameObject Dango;
    protected Text NinjaP;

    Inventory_JoaoBeijinho m_InvScript;
    PlayerController_JamieG m_LeftClick;
    private WeaponUI_LouieWilliamson wpnUI;

    // Start is called before the first frame update
    void Start()
    {
        NinjaP = GameObject.Find("Ninja Points").GetComponent<Text>();
        NinjaP.text = "Ninja Points: ";
        m_InvScript = FindObjectOfType<Inventory_JoaoBeijinho>();
        m_LeftClick = FindObjectOfType<PlayerController_JamieG>();
        wpnUI = GameObject.Find("WeaponsUI").GetComponent<WeaponUI_LouieWilliamson>();
    }

    private void Image_Click()
    {

        if (m_LeftClick.m_attackTap.enabled)
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit, 100.0f))
            {
                if (hit.collider.gameObject.transform.position == Noodles.transform.position)
                {
                    if (m_InvScript.HasItem(ItemType.NinjaPoints, 50))
                    {
                        //{ remove squid food, replace with noodles 
                        m_InvScript.RemoveItem(ItemType.NinjaPoints, 50);
                    }

                }
                if (hit.collider.gameObject.transform.position == Sake.transform.position)
                {
                    if (m_InvScript.HasItem(ItemType.NinjaPoints, 25))
                    {
                        // call sake action
                        m_InvScript.RemoveItem(ItemType.NinjaPoints, 25);
                        wpnUI.setPickupImage(FoodType.SAKE);
                        wpnUI.setPickupAnim(true);
                    }

                }
                if (hit.collider.gameObject.transform.position == Tempura.transform.position)
                {
                    wpnUI.setPickupImage(FoodType.TEMPURA);
                    wpnUI.setPickupAnim(true);
                }
                if (hit.collider.gameObject.transform.position == Dango.transform.position)
                {
                    if (m_InvScript.HasItem(ItemType.NinjaPoints, 50))
                    {
                        //call dango action
                        m_InvScript.RemoveItem(ItemType.NinjaPoints, 50);
                    }
                }
            }
        }
    }
    // Update is called once per frame
    void Update()
    {
        //foreach (KeyValuePair<ItemType, int> pair in FindObjectOfType<Inventory_JoaoBeijinho>().m_inventoryItems)
        //{
        //    NinjaP.text = "Ninja Points: " + pair.Value;
        //}

        //  NinjaPoints.GetComponent<UnityEngine.UI.Text>().text = "Ninja Points: " + ItemType.NinjaPoints;
    }
    

   
}
