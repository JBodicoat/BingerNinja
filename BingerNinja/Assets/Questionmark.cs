using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Questionmark : MonoBehaviour
{
    public GameObject questionmark;
    public GameObject excalmationmark;

    public void Alert()
    {

    }

    public void LostSight()
    {
        //for (int i = 5; i < 6; i++)
        {
            Instantiate(questionmark, new Vector3(0, 0, 0), Quaternion.identity);
           // i = 1;
        }
       
        //intastioat  on this player
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        LostSight();
    }
}
