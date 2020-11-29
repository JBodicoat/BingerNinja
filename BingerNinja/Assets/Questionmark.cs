using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Questionmark : MonoBehaviour
{
    public GameObject questionmark;
    public GameObject excalmationmark;

    public void Alert()
    {
        Instantiate(questionmark, gameObject.transform.position, Quaternion.identity);
    }

    public void LostSight()
    {
        Instantiate(excalmationmark, gameObject.transform.position, Quaternion.identity);
    }
}
