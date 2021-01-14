using UnityEngine;

public class PostProcess : MonoBehaviour
{
    public Shader q;
    public float w = 4f;
    public float e = 200;
    public float r = -10;
    public float t = 20;
    public float y = -3;
    public float u = 0.05f;
    public float i = 1.9f;
    public float o = 0.6f;
    public float p = 8f;
    public float a = 75f;
    public float s = 0.05f;

    // Chromatic aberration amounts
    public Vector2 d = new Vector2(0, 0);
    public Vector2 f = Vector2.zero;
    public Vector2 g = new Vector2(0, 0);

    private Material h;

    // Use this for initialization
    void Start()
    {
        h = new Material(q);
    }

    private void OnRenderImage(RenderTexture source, RenderTexture destination)
    {
        h.SetFloat("u_time", Time.fixedTime);
        h.SetFloat("u_bend", w);
        h.SetFloat("u_scanline_size_1", e);
        h.SetFloat("u_scanline_speed_1", r);
        h.SetFloat("u_scanline_size_2", t);
        h.SetFloat("u_scanline_speed_2", y);
        h.SetFloat("u_scanline_amount", u);
        h.SetFloat("u_vignette_size", i);
        h.SetFloat("u_vignette_smoothness", o);
        h.SetFloat("u_vignette_edge_round", p);
        h.SetFloat("u_noise_size", a);
        h.SetFloat("u_noise_amount", s);
        h.SetVector("u_red_offset", d);
        h.SetVector("u_blue_offset", f);
        h.SetVector("u_green_offset", g);
        Graphics.Blit(source, destination, h);
    }
}
