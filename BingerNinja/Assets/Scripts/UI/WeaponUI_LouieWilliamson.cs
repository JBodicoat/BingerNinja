///Louie Williamson
///This scripts handles the UI values for the weapons, pickups and keys.

//Louie 08/11/2020 - First created

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeaponUI_LouieWilliamson : MonoBehaviour
{
    // Start is called before the first frame update

    private bool hasKey;
    private bool isKeyOnScreen;

    private string weaponName;
    private string rangedName;
    private string rangedAmmo;

    private bool isWeaponUIonScreen; // use this to animate in and out

    public Text weaponText;
    public Image weaponImage;

    public Text rangedText;
    public Image rangedImage;
    public Text ammoText;

    public Image pickupImage;

    private float timer;
    private float timer2;
    void Start()
    {
        hasKey = false;
        isKeyOnScreen = false;
        isWeaponUIonScreen = false;

        weaponName = "";
        rangedName = "";
        rangedAmmo = "";
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void WeaponChange(FoodType newWeapon, bool isRanged, int ammo)
    {
        if (isRanged)
        {
            setAmmo("" + ammo);
        }

        switch (newWeapon)
        {
            case FoodType.FUGU:
                setName("Fugu", isRanged);
                //setImage();
                break;
            case FoodType.SQUID:
                setName("Squid", isRanged);
                //setImage();
                break;
            case FoodType.RICEBALL:
                setName("Riceball", isRanged);
                //setImage();
                break;
            case FoodType.KOBEBEEF:
                setName("Kobe Beef", isRanged);
                //setImage();
                break;
            case FoodType.SASHIMI:
                setName("Sashimi", isRanged);
                //setImage();
                break;
            case FoodType.PIZZA:
                setName("Pizza", isRanged);
                //setImage();
                break;
            case FoodType.SAKE:
                setName("Sake", isRanged);
                //setImage();
                break;
            case FoodType.NOODLES:
                setName("Noodles", isRanged);
                //setImage();
                break;
            default:
                break;
        }
    }
    void setName(string name, bool isRanged)
    {
        if (isRanged)
        {
            rangedText.text = name;
        }
        else
        {
            weaponText.text = name;
        }
    }
    void setImage(FoodType weapon, bool isRanged)
    {
        if (isRanged)
        {
            //rangedImage = ;
        }
        else
        {
            //weaponImage = ;
        }
    }
    void setAmmo(string ammo)
    {
        ammoText.text = ammo;
    }

    void setPickup()
    {
        //pickupImage = ;
    }

    void setKey(bool hasKey)
    {
        if (hasKey)
        {
            //enable key ui - could animate in
        }
        else
        {
            //disable key ui - could animate out
        }
    }
}
