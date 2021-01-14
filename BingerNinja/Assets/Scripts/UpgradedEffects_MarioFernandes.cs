// Mário Fernandes

// Mário 28/10/2020 - Add useble effects and substitute of the weapons to the upgraded ones
//Louie  11/12/2020 - Added pickup ui

using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// My class takes care implementing the useble effects and substitute the itmes when the the upgrade is eneble
/// </summary>
public class UpgradedEffects_MarioFernandes : MonoBehaviour
{
    int q = 0;

    public float m_sakeStrengthModifier = 2;
    public float m_TempuraStrengthModifier = 1.5f;
    public float m_TempuraSpeedhModifier = 0.5f;
    public GameObject m_Dango = null; 
    public GameObject m_Noodle = null;

     WeaponUI_LouieWilliamson w;

    //Would hapen whene you press the button to apply the sake effect
    public void SakeEffect()
    {
        if(GetComponent<Inventory_JoaoBeijinho>().HasItem(ItemType.Sake, 1))
        {
            GetComponent<EffectManager_MarioFernandes>().AddEffect(new StrengthEffect_MarioFernandes(30, m_sakeStrengthModifier));
            GetComponent<Inventory_JoaoBeijinho>().RemoveItem(ItemType.Sake, 1);
            w.setPickupImage(FoodType.SAKE);
            w.setPickupAnim(false);
        }
    }

    //Would hapen whene you press the button to apply the tempura effect
    public void TempuraEffect()
    {
        if(GetComponent<Inventory_JoaoBeijinho>().HasItem(ItemType.Tempura, 1))
        {
            GetComponent<EffectManager_MarioFernandes>().AddEffect(new StrengthEffect_MarioFernandes(30, m_TempuraStrengthModifier));
            GetComponent<EffectManager_MarioFernandes>().AddEffect(new SpeedEffect_MarioFernandes(30, m_TempuraSpeedhModifier));  
            GetComponent<Inventory_JoaoBeijinho>().RemoveItem(ItemType.Tempura, 1);
            w.setPickupImage(FoodType.TEMPURA);
            w.setPickupAnim(false);
        }
    }

    //Switch the Riceballs to dango on the level
    public void DangoEffect()
    {
        if(GetComponent<Inventory_JoaoBeijinho>().HasItem(ItemType.Dango, 1))
        {
           GameObject[] r = GameObject.FindGameObjectsWithTag("PlayerWeapon");     
           foreach (var e in r)
           {
               if(e.GetComponent<WeaponsTemplate_MarioFernandes>().m_foodType == FoodType.RICEBALL)
               {
                   Instantiate(m_Dango,e.transform.position, e.transform.rotation);
                   e.SetActive(false);
               }
           }
        }
    }

    //Switch the Squids to Noodles on the level
    public void NoodlesEffect()
    {
        if(GetComponent<Inventory_JoaoBeijinho>().HasItem(ItemType.Noodles, 1))
        {
           GameObject[] r = GameObject.FindGameObjectsWithTag("PlayerWeapon");    
           foreach (var e in r)
           {
               if(e.GetComponent<WeaponsTemplate_MarioFernandes>().m_foodType == FoodType.SQUID)
               {
                   Instantiate(m_Noodle,e.transform.position, e.transform.rotation);
                   e.SetActive(false);
               }
           }
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        w = GameObject.Find("WeaponsUI").GetComponent<WeaponUI_LouieWilliamson>();
    }

    // Update is called once per frame
    void Update()
    {
        if(q < SceneManager.GetActiveScene().buildIndex)
        {
            DangoEffect();
            NoodlesEffect();
            q = SceneManager.GetActiveScene().buildIndex;
        }
    }
}
