// Mário Fernandes

/// This script contains the taemplay for the effects of the player

///This class takes care of the current effects on the manager

// Mário 16/10/2020 - Creation of the  Manager (Ticks, array of effects and update each effect per tick )
//Mário 05/12/20 - Remove repeated effects

using System.Collections.Generic;
using UnityEngine;

public class EffectManager_MarioFernandes : MonoBehaviour
{
    //How much time is one tick
    public float m_Tick = 1;

    private float m_tickDuration = 0;

    public bool D = false;

    //List of effects
    private List<StatusEffect_MarioFernandes> m_effects = new List<StatusEffect_MarioFernandes>{};

    //It lets you add a pre created effect to the list
    public void Z(StatusEffect_MarioFernandes newEffect)
    {
        foreach (var item in m_effects)
        {
            if(item.GetType().Equals(newEffect.GetType()))
            {
               m_effects.Remove(item);
               m_effects.Add(newEffect);
               m_effects[m_effects.Count - 1].Activate(gameObject);
               return;
            }
        }

        m_effects.Add(newEffect);
        m_effects[m_effects.Count - 1].Activate(gameObject);
    }

    // Update is called once per frame
    void Update()
    {        
        if(!D)
        {
            //Detects if you have an effect each tick
            if(m_effects.Count > 0 && m_tickDuration >= (m_Tick))
            {
                //Update or destroy the  effects every tick
                for (int i = 0; i <= m_effects.Count - 1; i++)
                {                
                    if( m_effects[i].m_isEnable)
                    {
                    m_effects[i].Update();
                    }
                    else
                    m_effects.Remove(m_effects[i]);
                }
            m_tickDuration =0;
            }
            else
            m_tickDuration += Time.deltaTime;
        }
    }
}
