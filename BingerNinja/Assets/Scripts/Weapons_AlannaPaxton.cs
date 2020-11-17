using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq.Expressions;
using System.Security.AccessControl;
using UnityEngine;

public enum WeaponType { Fugu, Squid, Rice_ball, Kobe_Beef, Sashimi };



public class Weapons_AlannaPaxton : MonoBehaviour
{

    public WeaponType CurrentWeapon;
    public GameObject Fugu;
    public GameObject Squid;
    public GameObject Rice_Ball;
    public GameObject Kobe_Beef;
    public GameObject Sashimi;
    public GameObject Intern;
    private GameObject Chef;
    private GameObject Barista;
    private GameObject SecurityGuard;
    private GameObject PetTiger;
    private GameObject Alien;
    private GameObject SpaceNinja;
    private GameObject YakuzaThug;
    private GameObject BusinessMan;
    private GameObject Janitor;
    
    protected int rand;
    protected PlayerHealthHunger_MarioFernandes player;
    protected EffectManager_MarioFernandes Buffs;
    protected PlayerCombat_MarioFernandes HoldingItem;
    protected bool Projectile;

    // Start is called before the first frame update
    void Start()
    {
        player = GetComponent<PlayerHealthHunger_MarioFernandes>();
        Buffs = GetComponent<EffectManager_MarioFernandes>();
        HoldingItem = GetComponent<PlayerCombat_MarioFernandes>();
        Projectile = false;
     
       rand = Random.Range(0, 2);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void Attack()
    {

    }

    void Eat()
    {

        if(HoldingItem.IsHoldingFood() == true && Input.GetKeyDown(KeyCode.E)) //if the player is holding an item and e for eat is being pressed 
        {
            switch (CurrentWeapon)
            {
                case WeaponType.Rice_ball:
                    {
                        HealBuff_MarioFernandes heal = new HealBuff_MarioFernandes(20.0f, 2.0f, 1.0f);
                        Buffs.AddEffect(heal);
                    }
                    break;
                case WeaponType.Sashimi:
                    {
                        HealBuff_MarioFernandes heal = new HealBuff_MarioFernandes(30.0f, 5.0f, 1.5f);
                        Buffs.AddEffect(heal);
                    }
                    break;
                case WeaponType.Squid:
                    {
                        HealBuff_MarioFernandes heal = new HealBuff_MarioFernandes(15.0f, 2.0f, 1.0f);
                        Buffs.AddEffect(heal);
                    }
                    break;
                case WeaponType.Fugu:
                    {
                               if(rand ==0 )
                                {
                                    PoisionDefuff_MarioFernandes poison = new PoisionDefuff_MarioFernandes(5.0f, 5.0f, 0.5f);
                                    Buffs.AddEffect(poison);
                                }
                                else
                                {
                                    HealBuff_MarioFernandes heal = new HealBuff_MarioFernandes(30.0f, 2.0f, 1.0f);
                                    Buffs.AddEffect(heal);
                                };
                    }
                    break;
                case WeaponType.Kobe_Beef:
                    {
                        HealBuff_MarioFernandes heal = new HealBuff_MarioFernandes(50.0f, 5.0f, 1.5f);
                        Buffs.AddEffect(heal);
                    }
                    break;
                default:
                    break;
            }
        }

    }
   
}

class RangedWeapon : Weapons_AlannaPaxton
{
    WeaponType CurrentWeapon;
    private void Update()
    {
        if (HoldingItem.IsHoldingFood() == true && Input.GetMouseButtonDown(0))
        {
            switch (CurrentWeapon)
            {
                case WeaponType.Rice_ball:
                    {
                        if(Input.GetMouseButton(0)) // code to be added to different enemies to see if player has clicked on them? 
                        {

                        }
                        else
                        {

                        }

                    }
                    break;
                case WeaponType.Sashimi:
                    {
                        if (Input.GetMouseButton(0))
                        {

                        }
                        else
                        {

                        }
                    }
                    break;
                case WeaponType.Squid:
                    {
                        if (Input.GetMouseButton(0))
                        {

                        }
                        else
                        {

                        }
                    }
                    break;

                default:
                    break;
            }
        }

    }

}
class MeleeWeapon: Weapons_AlannaPaxton
{
    WeaponType CurrentWeapon;
    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            switch (CurrentWeapon)
            {
                case WeaponType.Fugu:
                    {
                        
                    }
                    break;
                case WeaponType.Kobe_Beef:
                    {

                    }
                    break;

                default:
                    break;
            }
        }
    }
}
//class Fugu : Weapons_AlannaPaxton
//{   
//     public GameObject foodFugu;
    
//    void Poison()
//    {
//        
 
//    }
//}
//class RiceBalls : Weapons_AlannaPaxton
//{
//    public GameObject foodRiceBalls;
//    // need to get the player hunger so if eat is true to update hunger 
//    bool IsProjectile() // this is pretty much the isweapon or isfood
//    {
//       Projectile = true;
//        //get ranged weapon info
//        return false;
//    }
//    HealBuff_MarioFernandes heal1 = new HealBuff_MarioFernandes(15.0f, 2.0f, 1.0f);
    

  
//}

//class KobeBeef : Weapons_AlannaPaxton
//{

//    //need the base speed from character code
//    // either creating a variable for boost speed or referencing another part of code
//    // how long the boost speed lasts for

//    // void Newspeed() if there is no referencing 
//    HealBuff_MarioFernandes heal1 = new HealBuff_MarioFernandes(50.0f, 5.0f, 1.5f);
//}

//class Sashimi : Weapons_AlannaPaxton
//{
//    //amount of time the strength stays on for 
//    // void StrengthTimer() if there is no timer code already
//    HealBuff_MarioFernandes heal1 = new HealBuff_MarioFernandes(30.0f, 5.0f, 1.5f);

//}


