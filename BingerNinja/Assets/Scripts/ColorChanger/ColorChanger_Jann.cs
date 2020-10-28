using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorChanger_Jann : MonoBehaviour
{
    private Camera m_camera;
    
    public Color[] greys = new[]
    {
        new Color(47.8f, 47.8f, 47.8f),
        new Color(23.5f, 23.5f, 23.5f),
        new Color(68.2f, 68.2f, 68.2f)
    };

    private void Awake()
    {
        m_camera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
    }

    void Start()
    {
        
    }

    void Update()
    {
        
    }
}
