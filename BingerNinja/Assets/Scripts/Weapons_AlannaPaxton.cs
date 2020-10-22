using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using System.Security.AccessControl;
using UnityEngine;


public class Weapons_AlannaPaxton : MonoBehaviour
{
    
    protected PlayerHealthHunger_MarioFernandes player;
    protected EffectManager_MarioFernandes poison_debuff;

    // Start is called before the first frame update
    void Start()
    {
        player = GetComponent<PlayerHealthHunger_MarioFernandes>();
        poison_debuff = GetComponent<EffectManager_MarioFernandes>();
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void Damage(int damage)
    {

       
    }

    
}
class Fugu : Weapons_AlannaPaxton
{

     public GameObject foodFugu;
   
    void Poison()
    {
   //having to call effect manager to set up a new status effect which will be the poison debuff mario has created.
       
    }
}
class RiceBalls : Weapons_AlannaPaxton
{
    // need to get the player hunger so if eat is true to update hunger 
    bool IsProjectile() // this is pretty much the isweapon or isfood
    {
        return true;
    }

    void AmountOfRice()
    {
        // in here perhaps have a random chance of how many projectiles can be made?
    }
}

class KobeBeef : Weapons_AlannaPaxton
{
    //need the base speed from character code
    // either creating a variable for boost speed or referencing another part of code
    // how long the boost speed lasts for
    
    // void Newspeed() if there is no referencing 
}

class Sashimi : Weapons_AlannaPaxton
{
    //amount of time the strength stays on for 
    // void StrengthTimer() if there is no timer code already

    bool IsStrength()
    {
        return true;
    }
}

class Natto: Weapons_AlannaPaxton
{
    //linking sound to natto specific game object so the player and enemy can hear it being thrown as a distraction
    // void Radius() to set up the distance that the enemy will hear it depending where it lands
    // void PlaySound() to play the specific sound, maybe one noise if its been noticed and another if its too far away from the enemys pickup radius.

    bool IsEnemyThere()
    {
        // to check the radius of the natto and the enemy and see if its overlapped 
        //wether the enemy can hear it and go investigate.
        return false;
    }

}