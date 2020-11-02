using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//public class Fugu_MarioFernandes : WeaponsTemplate_MarioFernandes
//
//{
//    //public int m_instaHeal = 0;
//    //public int m_healOverTime = 0;
//    //public int m_duration = 5;
//    //public int m_poisonDmg = 0;
//    FuguEffect_MarioFernandes m_eatEffect;
//    private void Start() {
//       m_eatEffect = new FuguEffect_MarioFernandes(m_instaHeal, m_duration, m_poisonDmg);
//       
//    }
//    public override void eat()
//    {        
//        FindObjectOfType<EffectManager_MarioFernandes>().AddEffect(m_eatEffect);
//    }
//
//    
//}
//
//public class FuguEffect_MarioFernandes : StatusEffect_MarioFernandes
//{
//        protected float m_instaHeal = 0;    
//        protected int m_damagePerTick = 0;
//        
//    public FuguEffect_MarioFernandes( int instaHeal, int duration, int dmg)
//    {              
//       
//        m_instaHeal = instaHeal;
//        m_duration = duration;
//        if(Random.Range(0,101) >= 50)
//        m_damagePerTick = dmg;
//    }
//
//    public override void Activate(GameObject target)
//    {
//        m_isEnable = true;
//        m_target = target;       
//       
//    }
//    protected override void Effect()
//    {        
//        m_target.GetComponent<PlayerHealthHunger_MarioFernandes>().Hit(m_damagePerTick);
//    }
//
//    public override void DeactivateEffect()
//    {        
//        m_isEnable = false;
//    }
//}