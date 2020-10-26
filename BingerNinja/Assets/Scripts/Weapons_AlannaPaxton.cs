using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using System.Security.AccessControl;
using UnityEngine;


public class Weapons_AlannaPaxton : MonoBehaviour
{

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
    void Damage(int damage)
    {
        if(HoldingItem.IsHoldingFood() == true)
        {

// turn to weapon.
        }
        else
        {
            // go to eating

        }
       
    }
   
}
class Fugu : Weapons_AlannaPaxton
{   
     public GameObject foodFugu;
    
    void Poison()
    {
        Projectile = false;
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
}
class RiceBalls : Weapons_AlannaPaxton
{
    public GameObject foodRiceBalls;
    // need to get the player hunger so if eat is true to update hunger 
    bool IsProjectile() // this is pretty much the isweapon or isfood
    {
       Projectile = true;
        //get ranged weapon info
        return false;
    }
    HealBuff_MarioFernandes heal1 = new HealBuff_MarioFernandes(15.0f, 2.0f, 1.0f);
    

  
}

class KobeBeef : Weapons_AlannaPaxton
{

    //need the base speed from character code
    // either creating a variable for boost speed or referencing another part of code
    // how long the boost speed lasts for

    // void Newspeed() if there is no referencing 
    HealBuff_MarioFernandes heal1 = new HealBuff_MarioFernandes(50.0f, 5.0f, 1.5f);
}

class Sashimi : Weapons_AlannaPaxton
{
    //amount of time the strength stays on for 
    // void StrengthTimer() if there is no timer code already
    HealBuff_MarioFernandes heal1 = new HealBuff_MarioFernandes(30.0f, 5.0f, 1.5f);

}


