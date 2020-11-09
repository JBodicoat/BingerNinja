///Louie Williamson
///This scripts handles the UI values and animations for the weapons, pickups and keys.

//Louie 08/11/2020 - First created
//Louie 09/11/2020 - Added animations

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeaponUI_LouieWilliamson : MonoBehaviour
{
    // Start is called before the first frame update

    private string weaponName;
    private string rangedName;
    private string rangedAmmo;

    public Text weaponText;
    public Image weaponImage;

    public Text rangedText;
    public Image rangedImage;
    public Text ammoText;

    public Image pickupImage;

    private Animator weaponsAnim;
    public Animator pickupAnim;
    public Animator keyAnim;

    private float timer;
    void Start()
    {
        timer = 0;

        weaponsAnim = GetComponent<Animator>();
        weaponName = "";
        rangedName = "";
        rangedAmmo = "";
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;

        if (timer > 20)
        {
            SetWeaponsUIAnimation(false);
            setPickupAnim(false);
            setKey(false);

            timer = 0;
        }
        else if (timer > 15)
        {
            setPickupAnim(true);
        }
        else if (timer > 10)
        {
            setKey(true);
        }
        else if (timer > 5)
        {
            SetWeaponsUIAnimation(true);
        }
    }
    public void SetWeaponsUIAnimation(bool isShownIfTrue)
    {
        weaponsAnim.SetBool("isOnScreen", isShownIfTrue);
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
                //setImage(, isRanged);
                break;
            case FoodType.SQUID:
                setName("Squid", isRanged);
                //setImage(, isRanged);
                break;
            case FoodType.RICEBALL:
                setName("Riceball", isRanged);
                //setImage(, isRanged);
                break;
            case FoodType.KOBEBEEF:
                setName("Kobe Beef", isRanged);
                //setImage(, isRanged);
                break;
            case FoodType.SASHIMI:
                setName("Sashimi", isRanged);
                //setImage(, isRanged);
                break;
            case FoodType.PIZZA:
                setName("Pizza", isRanged);
                //setImage(, isRanged);
                break;
            case FoodType.SAKE:
                setName("Sake", isRanged);
                //setImage(, isRanged);
                break;
            case FoodType.NOODLES:
                setName("Noodles", isRanged);
                //setImage(, isRanged);
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

    public void setPickupImage()
    {
        //pickupImage = ;
    }

    public void setKey(bool hasKey)
    {
        keyAnim.SetBool("hasKey", hasKey);
    }
    
    public void setPickupAnim(bool hasPickup)
    {
        pickupAnim.SetBool("isPickupShown", hasPickup);
    }
}
