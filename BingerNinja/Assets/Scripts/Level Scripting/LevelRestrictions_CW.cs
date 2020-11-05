using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelRestrictions_CW : MonoBehaviour
{
    public enum levelRestrictors{ HASHIDDEN, HASKEYONE };
    levelRestrictors restrictors;
    private Inventory_JoaoBeijinho playerInventory;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (playerInventory.HasItem(0))
        {
            gameObject.SetActive(false);
        }
        else
        {

        }
    }


}
