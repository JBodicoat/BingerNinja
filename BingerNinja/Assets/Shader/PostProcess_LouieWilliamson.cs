using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PostProcess_LouieWilliamson : MonoBehaviour
{
    // Start is called before the first frame update
    private Material material;

    private void Awake()
    {
        material = new Material(Shader.Find("Hidden/Pixelated"));
    }

    private void OnRenderImage(RenderTexture source, RenderTexture destination)
    {
        Graphics.Blit(source, destination, material);
        print("blit successful");
    }
    
}