//Elliott Desouza

//Elliott 07/10/2020 - made it so it applys a texture to the object.
using System.Collections;
using System.Collections.Generic;
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
    private Color[] mStoreColor;

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
            mColorSwapTex.SetPixel(i, 0, mStoreColor[i]);
            mColorSwapTex.Apply();
        }
    }

    //public void StartHitEffect(bool isCritical)
    //{
    //    mStoreColor = ColorChanger_Jann.Instance.SpriteColors;
    //    InitColorSwapTex();
    //    mHitEffectTimer = cHitEffectTime;
    //    SwapAllSpritesColorsTemporarily(isCritical ? ColorChanger_Jann.Instance.ColorOutGrey174 :
    //           ColorChanger_Jann.Instance.ColorOutGrey122);
    //}
    public void StartHitEffect(bool isCritical)
    {
        //mStoreColor = GameObject.Find("ColorChanger").GetComponent<ColorChanger_Jann>().m_spriteColors;
        mStoreColor = FindObjectOfType<ColorChanger_Jann>().m_spriteColors;
        InitColorSwapTex();
        mHitEffectTimer = cHitEffectTime;
        SwapAllSpritesColorsTemporarily(mStoreColor[isCritical ? m_critical : m_nonCritical]);

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

    // Start is called before the first frame update
    void Start()
    {
        mSpriteRenderer =transform.Find("EnemySprite").GetComponent<SpriteRenderer>();
        InitColorSwapTex();

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
