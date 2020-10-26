using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class TestBossDialog_MarioFernandes : MonoBehaviour
{
    int a = 0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        var curKeyboard= Keyboard.current;        
    

        if (curKeyboard.spaceKey.IsPressed())
        {
            gameObject.GetComponent<BossDialog_MarioFernandes>().TriggerDialogue(a);
            
            a++;
		}
    }
}
