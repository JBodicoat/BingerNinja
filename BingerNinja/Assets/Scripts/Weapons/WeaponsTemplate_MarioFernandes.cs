using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum FoodType
{
    FUGU,
    SQUID,
    RICEBALL,
    KOBEBEEF,
    SASHIMI,
    PIZZA,
    SAKE,
    NOODLES,
} 
public abstract class WeaponsTemplate_MarioFernandes : MonoBehaviour
{
    public  bool m_ranged;

    public FoodType m_foodType;

    public int dmg;

    public abstract void eat();

    public bool IsRanged()
    {
        return m_ranged;
    }
}

public abstract class MeleeWeapon_Mariofernandes : WeaponsTemplate_MarioFernandes
{
    private void Start() {
        m_ranged = false;
    }
}

public abstract class RangedWeapon_Mariofernandes : WeaponsTemplate_MarioFernandes
{
    private void Start() {
        m_ranged = true;
    }

}

