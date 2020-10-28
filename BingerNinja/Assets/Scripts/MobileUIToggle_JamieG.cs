//Jamie G - Script is used to turn off mobile UI elements when building for other platforms. Can be toggled easily if you want mobile UI elements to show in editor or not.
using UnityEngine;

//28/10/20 - Jamie - First implemented

public class MobileUIToggle_JamieG : MonoBehaviour
{
    private GameObject mobileUI;

    //If platform is not mobile, disable the button.
    void Awake()
    {
        mobileUI = GameObject.Find("Mobile");
        if (Application.isEditor)
        {
            mobileUI.SetActive(true); //Set this to false to not show button in the editor
        }
        else if (Application.isMobilePlatform)
        {
            mobileUI.SetActive(true);
        }
        else
        {
            mobileUI.SetActive(false);
        }
    }
}
