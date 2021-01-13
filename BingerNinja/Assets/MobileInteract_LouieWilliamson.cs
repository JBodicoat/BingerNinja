using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobileInteract_LouieWilliamson : MonoBehaviour
{
    // Start is called before the first frame update
    public bool isPressed;

    void Start()
    {
        isPressed = false;
    }
    // Update is called once per frame
    private void LateUpdate()
    {
        ResetInteract();
    }
    public void Interact()
    {
        isPressed = true;
        print("USE");
    }
    public void ResetInteract()
    {
        isPressed = false;
        print("STOP");
    }
}
