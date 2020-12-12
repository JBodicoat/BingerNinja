//Joao Beijinho 11/12/2020 - Created script
//                           Created reference to ventPath GameObject
//                           Created OnTriggerEnter2D which toggles ventPath ON/OFF
//                           Changed reference on start to reference on awake

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This script is to be used on the vents triggers, which will activate the VentPath
/// </summary>
public class VentTrigger_JoaoBeijinho : MonoBehaviour
{
    private GameObject m_ventPath;

    // Start is called before the first frame update
    void Awake()
    {
        m_ventPath = GameObject.Find("VentPath");
    }

    private void OnTriggerEnter2D(Collider2D collision)//Enter and Exit vent
    {
        if (collision.gameObject.CompareTag(Tags_JoaoBeijinho.m_playerTag))
        {
            m_ventPath.SetActive(!m_ventPath.activeInHierarchy);
        }
    }
}
