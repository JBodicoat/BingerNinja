// Jack 02/11/2020 added m_hungerRestoreAmount
// Mario 08/11/2020 - Add strength modifier 

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

    public int m_strengthModifier = 0;

    public float m_speedModifier = 1;
    public int dmg;

    public float m_hungerRestoreAmount;

    public bool IsRanged()
    {
        return m_ranged;
    }
}

