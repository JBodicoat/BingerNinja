using System;
using UnityEngine;

[ExecuteInEditMode]
[RequireComponent(typeof(Camera))]
public class ColorChangerCameraEffect_Jann : MonoBehaviour
{
    public Shader m_replacementShader;

    private Camera m_camera;

    private void Start()
    {
        GetComponent<Camera>().SetReplacementShader(m_replacementShader, "RenderType");
    }

    private void OnEnable()
    {
        m_camera = GetComponent<Camera>();
        
        if (m_replacementShader != null)
        {
            m_camera.SetReplacementShader(m_replacementShader, "RenderType");
        }
    }

    private void OnDisable()
    {
        m_camera.ResetReplacementShader();
    }
}
