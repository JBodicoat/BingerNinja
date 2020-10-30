using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorChanger_Jann : MonoBehaviour
{
    public enum SwapIndex
    {
        Outline = 25,
        SkinPrim = 254,
        SkinSec = 239,
        HandPrim = 235,
        HandSec = 204,
        ShirtPrim = 62,
        ShirtSec = 70,
        ShoePrim = 253,
        ShoeSec = 248,
        Pants = 72,
    }
    
    private const int Black = 0;
    private const int Grey1 = 11;
    private const int Grey2 = 49;
    private const int Grey3 = 108;

    // private SpriteRenderer[] m_spriteRenderers;
    private SpriteRenderer m_spriteRenderer;
    
    private Texture2D m_colorSwapTexture;
    private Color[] m_spriteColors;

    private void Awake()
    {
        // m_spriteRenderers = FindObjectsOfType<SpriteRenderer>();
        m_spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        InitializeColorChanger();
        
        // SwapColor(255, ColorFromInt(0x781100));
        // SwapColor(239, ColorFromInt(0x784a00));
        // SwapColor(62, ColorFromInt(0x784a00));
        // SwapColor(70, ColorFromInt(0x784a00));
        // SwapColor(72, ColorFromInt(0x784a00));
        //m_colorSwapTexture.Apply();

        for (int i = 109; i < 110; i++)
        {
            SwapColor(i, ColorFromInt(0x781100));
        }
        
        SwapColor(Grey1, ColorFromInt(0x784a00));
        SwapColor(Grey2, ColorFromInt(0x4c2d00));
        SwapColor(Grey3, ColorFromInt(0xc4ce00));
        SwapColor(Grey3, ColorFromIntRGB(255, 0, 0));
        m_colorSwapTexture.Apply();
    }

    private void InitializeColorChanger()
    {
        Texture2D colorSwapTex = new Texture2D(256, 1, TextureFormat.RGBA32, false, false);
        colorSwapTex.filterMode = FilterMode.Point;

        for (int i = 0; i < colorSwapTex.width; ++i)
            colorSwapTex.SetPixel(i, 0, new Color(0.0f, 0.0f, 0.0f, 0.0f));

        colorSwapTex.Apply();

        // foreach (SpriteRenderer spriteRenderer in m_spriteRenderers)
        // {
        //     spriteRenderer.material.SetTexture("_SwapTex", colorSwapTex);
        // }
        m_spriteRenderer.material.SetTexture("_SwapTex", colorSwapTex);

        m_spriteColors = new Color[colorSwapTex.width];
        m_colorSwapTexture = colorSwapTex;
    }

    public void SwapColor(int index, Color color)
    {
        m_spriteColors[index] = color;
        m_colorSwapTexture.SetPixel(index, 0, color);
    }
    
    public static Color ColorFromInt(int c, float alpha = 1.0f)
    {
        int r = (c >> 16) & 0x000000FF;
        int g = (c >> 8) & 0x000000FF;
        int b = c & 0x000000FF;

        Color ret = ColorFromIntRGB(r, g, b);
        ret.a = alpha;

        return ret;
    }

    public static Color ColorFromIntRGB(int r, int g, int b)
    {
        return new Color((float)r / 255.0f, (float)g / 255.0f, (float)b / 255.0f, 1.0f);
    }
    
    public void ClearColor(int index)
    {
        Color c = new Color(0.0f, 0.0f, 0.0f, 0.0f);
        m_spriteColors[(int)index] = c;
        m_colorSwapTexture.SetPixel((int)index, 0, c);
    }

    public void SwapAllSpritesColorsTemporarily(Color color)
    {
        for (int i = 0; i < m_colorSwapTexture.width; ++i)
            m_colorSwapTexture.SetPixel(i, 0, color);
        m_colorSwapTexture.Apply();
    }

    public void ResetAllSpritesColors()
    {
        for (int i = 0; i < m_colorSwapTexture.width; ++i)
            m_colorSwapTexture.SetPixel(i, 0, m_spriteColors[i]);
        m_colorSwapTexture.Apply();
    }

    public void ClearAllSpritesColors()
    {
        for (int i = 0; i < m_colorSwapTexture.width; ++i)
        {
            m_colorSwapTexture.SetPixel(i, 0, new Color(0.0f, 0.0f, 0.0f, 0.0f));
            m_spriteColors[i] = new Color(0.0f, 0.0f, 0.0f, 0.0f);
        }
        m_colorSwapTexture.Apply();
    }
}
