//Joao Beijinho 11/12/2020 - Created script
//                           Created reference to ventPath GameObject
//                           Created OnTriggerEnter2D which toggles ventPath ON/OFF
//                           Changed reference on start to reference on awake
//Joao Beijinho 12/01/2021 - Changed OnTriggerEnter2D to only toggle the ventPath ON

using UnityEngine;

/// <summary>
/// This script is to be used on the vents triggers, which will activate the VentPath
/// </summary>
public class VentTrigger_JoaoBeijinho : MonoBehaviour
{
     GameObject a;

    // Start is called before the first frame update
    void Awake()
    {
        a = GameObject.Find("VentPath");
    }

     void OnTriggerEnter2D(Collider2D b)//Enter and Exit vent
    {
        if (b.gameObject.CompareTag(Tags_JoaoBeijinho.QK))
        {
            a.SetActive(true);
        }
    }
}
