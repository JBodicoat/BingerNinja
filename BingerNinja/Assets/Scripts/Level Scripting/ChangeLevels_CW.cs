using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeLevels_CW : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            SceneManager_JamieG.Instance.LoadNextLevel();
        }
    }
}
