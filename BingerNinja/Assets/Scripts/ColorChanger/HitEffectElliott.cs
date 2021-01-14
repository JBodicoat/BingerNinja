﻿//Elliott Desouza

//Elliott 07/10/2020 - made it so it applys a texture to the object.
//Elliott 30/12/2020  added  = transform.Find("EnemySprite") to the code in the start function.
// Jann   11/12/2020 - This class now uses its own colour changer which fixes two bugs 

using UnityEngine;

/// this script changes the color to an object that has been hit
public class HitEffectElliott : MonoBehaviour
{
    public int m_critical = 174;
    public int m_nonCritical = 122;
    internal SpriteRenderer mSpriteRenderer;
    internal Texture2D mColorSwapTex;
    internal Color[] mSpriteColors;
    float mHitEffectTimer = 0.0f;
    const float cHitEffectTime = 0.3f;
    Color[] a;

    //Sets every pixel’s color of the sprite to the passed color
    public void SwapAllSpritesColorsTemporarily(Color color)
    {
        for (int i = 0; i < mColorSwapTex.width; ++i)
        {
            mColorSwapTex.SetPixel(i, 0, color);
            mColorSwapTex.Apply();
        }
    }

    // Resets the sprites original color 
    public void ResetAllSpritesColors()
    {
        for (int i = 0; i < mColorSwapTex.width; ++i)
        {
            mColorSwapTex.SetPixel(i, 0, a[i]);
            mColorSwapTex.Apply();
        }
    }

    public void RT(bool b)
    {
        mHitEffectTimer = cHitEffectTime;
        SwapAllSpritesColorsTemporarily(a[b ? m_critical : m_nonCritical]);
    }
    
    public void InitColorSwapTex()
    {
        Texture2D colorSwapTex = new Texture2D(256, 1, TextureFormat.RGBA32, false, false);
        colorSwapTex.filterMode = FilterMode.Point;

        for (int i = 0; i < colorSwapTex.width; ++i)
        {
            colorSwapTex.SetPixel(i, 0, new Color(0.0f, 0.0f, 0.0f, 0.0f));
        }
        colorSwapTex.Apply();

        mSpriteRenderer.material.SetTexture("_SwapTex", colorSwapTex);

        mSpriteColors = new Color[colorSwapTex.width];
        mColorSwapTex = colorSwapTex;
    }
    
    void SwapColor(int c, Color d)
    {
        mSpriteColors[c] = d;
        mColorSwapTex.SetPixel(c, 0, d);
    }

    // Start is called before the first frame update
    void Start()
    {
        if (gameObject.CompareTag(Tags_JoaoBeijinho.m_playerTag))
        {
            mSpriteRenderer = GetComponent<SpriteRenderer>();
        }
        else
        {
            mSpriteRenderer = GetComponentInChildren<SpriteRenderer>();
        }
        a = ColorChanger_Jann.Instance.m_spriteColors;
        
        InitColorSwapTex();
        
        SwapColor(60, a[60]);
        SwapColor(122, a[122]);
        SwapColor(174, a[174]);
        mColorSwapTex.Apply();
    }

    // Update is called once per frame
    void Update()
    {
        if (mHitEffectTimer > 0.0f)
        {
            mHitEffectTimer -= Time.deltaTime;
            if (mHitEffectTimer <= 0.0f)
                ResetAllSpritesColors();
        }
    }
}
