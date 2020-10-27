using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class TestBossDialog_MarioFernandes : MonoBehaviour
{
    public bool dial = false;
    int a = 0;

    // Update is called once per frame
    void Update()
    {
        
        if (dial)
        {
            gameObject.GetComponent<BossDialog_MarioFernandes>().TriggerDialogue(a);
            dial = false;
            a++;
		}
    }
}
