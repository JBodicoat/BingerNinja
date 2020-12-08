// Jann

/// Swaps the pixels with a given color to the desired color

// Jann 29/10/20 - Proof of concept implementation
// Jann 01/11/20 - Inspector UI, finalizing implementation
// Jann 03/11/20 - Added TilemapRenderer and Image (UI) support
// Jann 07/11/20 - Materials are now assigned in code and don't need to be set for everything in Unity
// Jann 08/11/20 - Turned it into a singleton (used by HitEffect_Elliot)
// Elliott 20/11/2020 = made color arry public
// Jann 22/11/20 - Refactor and added particle system support

using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UI;

public class ColorChanger_Jann : Singleton_Jann<ColorChanger_Jann>
{
    public Material m_swapMaterial;
    
    public Color m_colorOutGrey60 = new Color(0, 0, 0, 1);
    public Color m_colorOutGrey122 = new Color(0, 0, 0, 1);
    public Color m_colorOutGrey174 = new Color(0, 0, 0, 1);

    public OriginalColor m_particleColor = OriginalColor.Grey60;
    
    public enum OriginalColor
    { 
        Grey60 = 60, Grey122 = 122, Grey174 = 174
    } 

    private SpriteRenderer[] m_spriteRenderers;
    private TilemapRenderer[] m_tilemapRenderers;
    private Image[] m_images;
    private ParticleSystem[] m_particleSystems;

    private Texture2D m_colorSwapTexture;
    public Color[] m_spriteColors;
    
    private void Awake()
    {
        m_spriteRenderers = FindObjectsOfType<SpriteRenderer>();
        m_tilemapRenderers = FindObjectsOfType<TilemapRenderer>();
        m_images = FindObjectsOfType<Image>();
        m_particleSystems = FindObjectsOfType<ParticleSystem>();
    }

    private void Start()
    {
        InitializeColorChanger();

        SwapColor((int) OriginalColor.Grey60, m_colorOutGrey60);
        SwapColor((int) OriginalColor.Grey122, m_colorOutGrey122);
        SwapColor((int) OriginalColor.Grey174, m_colorOutGrey174);
        m_colorSwapTexture.Apply();

        ApplyColorsToSceneObjects();
    }

    private void InitializeColorChanger()
    {
        Texture2D colorSwapTex = new Texture2D(256, 1, TextureFormat.RGBA32, false, false);
        colorSwapTex.filterMode = FilterMode.Point;

        for (int i = 0; i < colorSwapTex.width; ++i)
            colorSwapTex.SetPixel(i, 0, new Color(0.0f, 0.0f, 0.0f, 0.0f));

        colorSwapTex.Apply();

        m_swapMaterial.SetTexture("_SwapTex", colorSwapTex);
        
        m_spriteColors = new Color[colorSwapTex.width];
        m_colorSwapTexture = colorSwapTex;
    }

    private void ApplyColorsToSceneObjects()
    {
        foreach (SpriteRenderer spriteRenderer in m_spriteRenderers)
        {
            spriteRenderer.material = m_swapMaterial;
        }
        
        foreach (TilemapRenderer tilemapRenderer in m_tilemapRenderers)
        {
            tilemapRenderer.material = m_swapMaterial;
        }

        foreach (Image image in m_images)
        {
            image.material = m_swapMaterial;
        }
        
        foreach (ParticleSystem particleSystem in m_particleSystems)
        {
            var main = particleSystem.main;
            main.startColor = m_spriteColors[(int) m_particleColor];
        }
    }

    private void SwapColor(int index, Color color)
    {
        m_spriteColors[index] = color;
        m_colorSwapTexture.SetPixel(index, 0, color);
    }
}
