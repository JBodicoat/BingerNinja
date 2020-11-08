// Mário Fernandes
/// This script contains the taemplay for the effects of the player

///This script is compose of 3 classes, StatusEffect, Poison, Heal

// Mário 18/10/2020 - Creation of the StatusEffect, Poison, Heal. 
//For the tatus Effect i created the Base variables and funtions(m_isEnable, m_Target, ,m_duration, m_timeLeft, m_speedMultiplier, Effect, Activate, Update, DeactivateEffect)
//For the Poison i created the m_damagePerTick variable and overrided the the basic functions to make it do damge per tick
//For the Heal i created the m_healthIncreaseariable and overrided the the basic functions to make increace the max health and heal

// Jack 02/11/2020 Changed StatusEffect class to no longer inherit from MonoBehaviour as this was not needed (and caused a warning)
// Mario 08/11/2020 - Update Heal and poison, Create Speed and Strength modifier effects
// Jack 08/11/2020 - Changed StatusEffect functions from abstract to virtual to cut down on repeated code and empty functions. Some minor format changes


using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.Interactions;

/// <summary>
/// Template class used as base to any effet
/// </summary>
public abstract class StatusEffect_MarioFernandes
{
    public bool m_isEnable;
    protected GameObject m_target = null;

    //How many ticks(sec) will last
    protected float m_duration = 0;
    protected float m_speedMultiplier = 1;

    //Expecific to every effect
    protected virtual void Effect()
    { 
    }

    //Expecific to every effect
    public virtual void Activate(GameObject target) 
    {
        m_isEnable = true;
        m_target = target;
    }

    //Expecific to every effect
    public virtual void DeactivateEffect() 
    { 
        m_isEnable = false;
    }

    public void Update()
    {
        if(m_duration-- > 0)
            Effect();
        else
            DeactivateEffect();                
    }

}

//Basic poison effect
public class PoisionDefuff_MarioFernandes : StatusEffect_MarioFernandes
{
    protected float m_damagePerTick = 0;    

    ///<summary>
    ///To create you need to give:
    /// * Duration (How many seconds)
    /// * The Damage (per second)    
    ///</summary>
    public PoisionDefuff_MarioFernandes(float duration, float damagePerTick)

    {
        m_duration = duration;
        m_damagePerTick = damagePerTick;        
    }

    protected override void Effect()
    {        
        m_target.GetComponent<PlayerHealthHunger_MarioFernandes>().Hit(m_damagePerTick);
    }
}

//Basic Heal and Max hp increase
public class HealBuff_MarioFernandes : StatusEffect_MarioFernandes
{
    protected float m_healthIncrease = 0;

    ///<summary>
    ///To create you need to give:
    /// * Duration (How many seconds)
    /// * The Heal (Max hp and heal)    
    ///</summary>
    public HealBuff_MarioFernandes(float duration, float healthIncrease)
    {
        m_healthIncrease = healthIncrease;
        m_duration = duration;    
    }

    public override void Activate(GameObject target)
    {
        m_isEnable = true;
        m_target = target;
        m_target.GetComponent<PlayerHealthHunger_MarioFernandes>().Heal(m_healthIncrease);
    }
}

public class SpeedEffect_MarioFernandes : StatusEffect_MarioFernandes
{
    protected float m_healthIncrease = 0;

        ///<summary>
    ///To create you need to give:
    /// * Duration (How many seconds)
    /// * The speed, to increace (1-2), Default is 1 (Normal speed)
    ///</summary>
    public SpeedEffect_MarioFernandes(float duration, float speedMultiplier = 1)
    {
        m_duration = duration;
        m_speedMultiplier = speedMultiplier;        
    }

    public override void Activate(GameObject target)
    {
        m_isEnable = true;
        m_target = target;
        m_target.GetComponent<PlayerMovement_MarioFernandes>().m_speed =  (m_target.GetComponent<PlayerMovement_MarioFernandes>().m_baseSpeed * m_speedMultiplier);
    }
    public override void DeactivateEffect()
    {
        m_target.GetComponent<PlayerMovement_MarioFernandes>().ResetSpeed();
        m_isEnable = false; 
    } 
}

public class StrengthEffect_MarioFernandes : StatusEffect_MarioFernandes
{
    protected float m_strengthModifier = 0;

    ///<summary>
    ///To create you need to give:
    /// * Duration (How many seconds)
    /// * Strength Modifier
    ///</summary>
    public StrengthEffect_MarioFernandes(float duration, float strengthModifier = 1)
    {
        m_duration = duration;
        m_strengthModifier = strengthModifier;        
    }
}