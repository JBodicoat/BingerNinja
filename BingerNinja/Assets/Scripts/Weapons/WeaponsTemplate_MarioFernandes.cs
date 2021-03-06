﻿///Mário Fernandes

// Mario 02/11/2020 - Create the Class
// Jack 02/11/2020 added m_hungerRestoreAmount
// Mario 08/11/2020 - Add strength modifier 
// Mario 13/11/2020 - Add Distraction time to progectile
// Mario 20/11/2020 - Ammunition added
// Mario 28/11/2020 - sprite added
// Mario 13/12/2020 - Define Normal Foodtype as null

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

///<Summary>
///This class serves as the template for all the weapons
///<Summary>
public class WeaponsTemplate_MarioFernandes : MonoBehaviour
{
    [SerializeField]
    protected  bool m_ranged;
    public FoodType m_foodType = FoodType.NULL;
    public int m_instaHeal = 0;
    public int m_healOverTime = 0;
    public int m_duration = 5;
    public int m_poisonDmg = 0;
    public float m_strengthModifier = 0;
    public float m_speedModifier = 1;
    public float m_distractTime = 0;

    public int m_ammunition = 0;
    public int dmg;

    public float m_hungerRestoreAmount;

    public Sprite m_mySprite;

    private void Start() {
        m_mySprite = GetComponent<SpriteRenderer>().sprite;
    }
    public bool IsRanged()
    {
        return m_ranged;
    }
}

