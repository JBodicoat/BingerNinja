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

    public Text weaponText;
    public Image weaponImage;

    public Text rangedText;
    public Image rangedImage;
    public Text ammoText;

    public Image pickupImage;

    private Animator weaponsAnim;
    public Animator pickupAnim;
    public Animator keyAnim;
   //public GameObject MeleeHighlight;
   //public GameObject RangedHighlight;

    public List<Sprite> WeaponSprites = new List<Sprite>();
    private int rangedAmmo;

    public Texture2D rangedCursor;
    public Texture2D normalCursor;
    private Vector2 normalOffset;
    private Vector2 rangedOffset;

    //---- LIST KEY ----   //
    //  0     =   Fugu     //
    //  1     =   Squid    //
    //  2     =   Riceball //
    //  3     =   Kobe     //
    //  4     =   Sashimi  //
    //  5     =   Tempura  //
    //  6     =   Sake     //
    //  7     =   Noodles  //
    //  8     =   None     //
    public void SetActiveWeapon(bool isMelee)
    {
        //MeleeHighlight.SetActive(isMelee);
        //RangedHighlight.SetActive(!isMelee);
        SetCursor(!isMelee);
    }
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
    public void setPickupImage(FoodType newPickup)
    {
        pickupImage.sprite = WeaponSprites[(int)newPickup];
    }
    public void removeWeapon(bool isRanged)
    {
        if (isRanged)
        {
            rangedImage.sprite = WeaponSprites[8];
            setAmmo(-50);
        }
        else
        {
            weaponImage.sprite = WeaponSprites[8];
        }
        setName("None", isRanged);
    }

    public void WeaponChange(FoodType newWeapon, bool isRanged, int ammo)
    {
        if (isRanged)
        {
            setAmmo(ammo);
        }

        setName(newWeapon.ToString(), isRanged);
        setImage((int)newWeapon, isRanged);
    }
    public void setAmmo(int addToAmmo)
    {
        rangedAmmo = addToAmmo;
        if (rangedAmmo < 0) rangedAmmo = 0;
        ammoText.text = rangedAmmo.ToString();
    }
    void Start()
    {
        weaponsAnim = GetComponent<Animator>();
        removeWeapon(true);
        removeWeapon(false);
        normalOffset = new Vector2(4, 0);
        rangedOffset = new Vector2( 8, 8);
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

    void SetCursor(bool isRanged)
    {
        if (isRanged)
        {
            Cursor.SetCursor(rangedCursor, rangedOffset, CursorMode.Auto);
        }
        else
        {
            Cursor.SetCursor(normalCursor, normalOffset, CursorMode.Auto);
        }
    }

}
