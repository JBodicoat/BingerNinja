///Louie Williamson
///This script links to the crt shader to apply it to the camera as a post process
///
using UnityEngine;

public class PostProcess_LouieWilliamson : MonoBehaviour
{
    public Shader shader;
    public float bend = 4f;
    public float scanlineSize1 = 200;
    public float scanlineSpeed1 = -10;
    public float scanlineSize2 = 20;
    public float scanlineSpeed2 = -3;
    public float scanlineAmount = 0.05f;
    public float vignetteSize = 1.9f;
    public float vignetteSmoothness = 0.6f;
    public float vignetteEdgeRound = 8f;
    public float noiseSize = 75f;
    public float noiseAmount = 0.05f;

    // Chromatic aberration amounts
    public Vector2 redOffset = new Vector2(0, 0);
    public Vector2 blueOffset = Vector2.zero;
    public Vector2 greenOffset = new Vector2(0, 0);

    private Material m;
    void Start()
    {
        m = new Material(shader);
    }

    private void OnRenderImage(RenderTexture c, RenderTexture e)
    {
        m.SetFloat("u_time", Time.fixedTime);
        m.SetFloat("u_bend", bend);
        m.SetFloat("u_scanline_size_1", scanlineSize1);
        m.SetFloat("u_scanline_speed_1", scanlineSpeed1);
        m.SetFloat("u_scanline_size_2", scanlineSize2);
        m.SetFloat("u_scanline_speed_2", scanlineSpeed2);
        m.SetFloat("u_scanline_amount", scanlineAmount);
        m.SetFloat("u_vignette_size", vignetteSize);
        m.SetFloat("u_vignette_smoothness", vignetteSmoothness);
        m.SetFloat("u_vignette_edge_round", vignetteEdgeRound);
        m.SetFloat("u_noise_size", noiseSize);
        m.SetFloat("u_noise_amount", noiseAmount);
        m.SetVector("u_red_offset", redOffset);
        m.SetVector("u_blue_offset", blueOffset);
        m.SetVector("u_green_offset", greenOffset);
        Graphics.Blit(c, e, m);
    }
}
