using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ActiveatePauseMenu_Elliott : MonoBehaviour
{
    protected VendingMachineMenu_Elliott m_vendingMachineMenu_Elliott;
    public GameObject m_pauseMenu;

    public void OpenPauseMenu()
    {
       // if(m_vendingMachineMenu_Elliott.)
        m_pauseMenu.SetActive(true);
    }

    private void Start()
    {
        m_vendingMachineMenu_Elliott = GetComponent<VendingMachineMenu_Elliott>();
        m_pauseMenu.SetActive(false);
    }
    // Update is called once per frame
    void Update()
    {
        var gamepad = Keyboard.current;
        if (gamepad == null)
            return;

        if (gamepad.pKey.wasPressedThisFrame)
        {
            OpenPauseMenu();
        }
    }
}