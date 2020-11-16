///Louie Williamson
///This scripts handles the UI values and animations for the weapons, pickups and keys.

//Louie 08/11/2020 - First created
//Louie 09/11/2020 - Added animations
//Louie 16/11/2020 - Added Weapon Sprite List

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

    public List<Sprite> WeaponSprites = new List<Sprite>();

    //---- LIST KEY ----   //
    //  0     =   Fugu     //
    //  1     =   Squid    //
    //  2     =   Riceball //
    //  3     =   Kobe     //
    //  4     =   Sashimi  //
    //  5     =   Tempura  //
    //  6     =   Sake     //
    //  7     =   Noodles   //

    public void SetWeaponsUIAnimation(bool isShownIfTrue)
    {
        weaponsAnim.SetBool("isOnScreen", isShownIfTrue);
    }
    public void setKey(bool hasKey)
    {
        keyAnim.SetBool("hasKey", hasKey);
    }
    
    public void setPickupAnim(bool hasPickup)
    {
        pickupAnim.SetBool("isPickupShown", hasPickup);
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
                setImage( 0, isRanged);
                break;
            case FoodType.SQUID:
                setName("Squid", isRanged);
                setImage(1, isRanged);
                break;
            case FoodType.RICEBALL:
                setName("Riceball", isRanged);
                setImage(2, isRanged);
                break;
            case FoodType.KOBEBEEF:
                setName("Kobe Beef", isRanged);
                setImage(3, isRanged);
                break;
            case FoodType.SASHIMI:
                setName("Sashimi", isRanged);
                setImage(4, isRanged);
                break;
            case FoodType.PIZZA: // This needs to be changed to tempura
                setName("Pizza", isRanged);
                setImage(5, isRanged);
                break;
            case FoodType.SAKE:
                setName("Sake", isRanged);
                setImage(6, isRanged);
                break;
            case FoodType.NOODLES:
                setName("Noodles", isRanged);
                setImage(7, isRanged);
                break;
            default:
                break;
        }
    }
    void Start()
    {
        weaponsAnim = GetComponent<Animator>();
        weaponName = "";
        rangedName = "";
        rangedAmmo = "";   
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
    void setImage(int weaponIndex, bool isRanged)
    {
        if (isRanged)
        {
            rangedImage.sprite = WeaponSprites[weaponIndex];
        }
        else
        {
            weaponImage.sprite = WeaponSprites[weaponIndex];
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

}
