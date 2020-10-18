using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using System.Security.AccessControl;
using UnityEngine;


public class Weapons_AlannaPaxton : MonoBehaviour
{
    
    protected PlayerHealthHunger_MarioFernandes player;
    protected PoisionDefuff_MarioFernandes poison_debuff;

    // Start is called before the first frame update
    void Start()
    {
        player = GetComponent<PlayerHealthHunger_MarioFernandes>();
        poison_debuff = GetComponent<PoisionDefuff_MarioFernandes>();
        
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

    // put fugu gameobject here  public GameObject foodFugu;
   
    void Poison()
    {

  
       
    }
}
