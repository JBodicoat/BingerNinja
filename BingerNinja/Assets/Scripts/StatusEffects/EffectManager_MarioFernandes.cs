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

     float q = 0;

    public bool m_paused = false;

    //List of effects
     List<StatusEffect_MarioFernandes> w = new List<StatusEffect_MarioFernandes>{};

    //It lets you add a pre created effect to the list
    public void AddEffect(StatusEffect_MarioFernandes e)
    {
        foreach (var r in w)
        {
            if(r.GetType().Equals(e.GetType()))
            {
               w.Remove(r);
               w.Add(e);
               w[w.Count - 1].Activate(gameObject);
               return;
            }
        }

        w.Add(e);
        w[w.Count - 1].Activate(gameObject);
    }

    // Update is called once per frame
    void Update()
    {        
        if(!m_paused)
        {
            //Detects if you have an effect each tick
            if(w.Count > 0 && q >= (m_Tick))
            {
                //Update or destroy the  effects every tick
                for (int i = 0; i <= w.Count - 1; i++)
                {                
                    if( w[i].m_isEnable)
                    {
                    w[i].Update();
                    }
                    else
                    w.Remove(w[i]);
                }
            q =0;
            }
            else
            q += Time.deltaTime;
        }
    }
}
