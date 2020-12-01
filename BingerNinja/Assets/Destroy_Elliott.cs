using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destroy_Elliott : MonoBehaviour
{

    public float m_lifeTime= .1f; //This implies a delay of 2 seconds.

   
    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, m_lifeTime);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
