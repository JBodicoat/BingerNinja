// Jann

/// Swaps the pixels with a given color to the desired color

// Jann 29/10/20 - Proof of concept implementation
// Jann 01/11/20 - Inspector UI, finalizing implementation
// Jann 03/11/20 - Added TilemapRenderer and Image (UI) support

using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UI;

public class ColorChanger_Jann : MonoBehaviour
{
    private const int Grey60 = 60;
    private const int Grey122 = 122;
    private const int Grey174 = 174;

    public Color m_colorOutGrey60 = new Color(0, 0, 0, 1);
    public Color m_colorOutGrey122 = new Color(0, 0, 0, 1);
    public Color m_colorOutGrey174 = new Color(0, 0, 0, 1);

    private SpriteRenderer[] m_spriteRenderers;
    private TilemapRenderer[] m_tilemapRenderers;
    private Image[] m_images;

    private Texture2D m_colorSwapTexture;
    private Color[] m_spriteColors;
    
    private void Awake()
    {
        m_spriteRenderers = FindObjectsOfType<SpriteRenderer>();
        m_tilemapRenderers = FindObjectsOfType<TilemapRenderer>();
        m_images = FindObjectsOfType<Image>();
    }

    private void Start()
    {
        InitializeColorChanger();

        SwapColor(Grey60, m_colorOutGrey60);
        SwapColor(Grey122, m_colorOutGrey122);
        SwapColor(Grey174, m_colorOutGrey174);
        m_colorSwapTexture.Apply();
    }

    private void InitializeColorChanger()
    {
        Texture2D colorSwapTex = new Texture2D(256, 1, TextureFormat.RGBA32, false, false);
        colorSwapTex.filterMode = FilterMode.Point;

        for (int i = 0; i < colorSwapTex.width; ++i)
            colorSwapTex.SetPixel(i, 0, new Color(0.0f, 0.0f, 0.0f, 0.0f));

        colorSwapTex.Apply();

        foreach (SpriteRenderer spriteRenderer in m_spriteRenderers)
        {
            spriteRenderer.material.SetTexture("_SwapTex", colorSwapTex);
        }
        
        foreach (TilemapRenderer tilemapRenderer in m_tilemapRenderers)
        {
            tilemapRenderer.material.SetTexture("_SwapTex", colorSwapTex);
        }

        foreach (Image image in m_images)
        {
            image.material.SetTexture("_SwapTex", colorSwapTex);
        }
        
        m_spriteColors = new Color[colorSwapTex.width];
        m_colorSwapTexture = colorSwapTex;
    }

    private void SwapColor(int index, Color color)
    {
        m_spriteColors[index] = color;
        m_colorSwapTexture.SetPixel(index, 0, color);
    }
}
