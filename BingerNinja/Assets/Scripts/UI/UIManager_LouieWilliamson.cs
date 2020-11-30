///Louie Williamson
///This scripts shows the mobile ui if necessary

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager_LouieWilliamson : MonoBehaviour
{
    public GameObject mobileWpnUI;
    public GameObject mobileBtnUI;
    public GameObject normalUI;
    public GameObject mobileJoystickUI;
    void Start()
    {
        //SetMobileUI(false);
        
        //if (Application.platform == RuntimePlatform.Android || Application.platform == RuntimePlatform.IPhonePlayer)
        //{
        //    SetMobileUI(true);
        //}
        SetMobileUI(true);
    }
    private void SetMobileUI(bool ShownIfTrue)
    {
        mobileWpnUI.SetActive(ShownIfTrue);
        mobileBtnUI.SetActive(ShownIfTrue);
        mobileJoystickUI.SetActive(ShownIfTrue);
        normalUI.SetActive(!ShownIfTrue);
    }
}
