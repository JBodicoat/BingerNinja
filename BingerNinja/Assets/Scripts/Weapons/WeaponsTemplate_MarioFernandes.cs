using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class WeaponsTemplate_MarioFernandes : MonoBehaviour
{
    [SerializeField]
    protected  bool m_ranged;

    public FoodType m_foodType;
    public int m_instaHeal = 0;
    public int m_healOverTime = 0;
    public int m_duration = 5;
    public int m_poisonDmg = 0;

    public float m_speedModifier = 1;
    public int dmg;

    public bool IsRanged()
    {
        return m_ranged;
    }
}

