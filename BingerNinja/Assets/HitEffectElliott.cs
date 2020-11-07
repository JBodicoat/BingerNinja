//Elliott Desouza
/// this script changes the color to an object that has been hit

//Elliott 07/10/2020 - made it so it applys a texture to the object
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitEffectElliott : MonoBehaviour
{
    public SpriteRenderer mSpriteRenderer;
    public Texture2D mColorSwapTex;
    public Color [] mSpriteColors;
    float mHitEffectTimer = 0.0f;
    const float cHitEffectTime = 0.3f;
    private Color[] mStoreColor;

    public void SwapAllSpritesColorsTemporarily(Color color)
    {       
        for (int i = 0; i < mColorSwapTex.width; ++i)
        mColorSwapTex.SetPixel(i, 0, color);
        mColorSwapTex.Apply();
    }

    public void ResetAllSpritesColors()
    {
        for (int i = 0; i < mColorSwapTex.width; ++i)
        mColorSwapTex.SetPixel(i, 0, mStoreColor[i]);
        mColorSwapTex.Apply();
    }

    public void StartHitEffect(bool isCritical)
    {
        mStoreColor = GameObject.Find("ColorChanger").GetComponent<ColorChanger_Jann>().m_spriteColors;
        InitColorSwapTex();
        mHitEffectTimer = cHitEffectTime;
        SwapAllSpritesColorsTemporarily(mStoreColor[isCritical ? 174 : 122]);
        //SwapAllSpritesColorsTemporarily(new Color(0.679f, 0f, 0f, 1f));

    }

    public void InitColorSwapTex()
    {
        Texture2D colorSwapTex = new Texture2D(256, 1, TextureFormat.RGBA32, false, false);
        colorSwapTex.filterMode = FilterMode.Point;

        for (int i = 0; i < colorSwapTex.width; ++i)
        colorSwapTex.SetPixel(i, 0, new Color(0.0f, 0.0f, 0.0f, 0.0f));

        colorSwapTex.Apply();

        mSpriteRenderer.material.SetTexture("_SwapTex", colorSwapTex);

        mSpriteColors = new Color[colorSwapTex.width];
        mColorSwapTex = colorSwapTex;
    }

    // Start is called before the first frame update
    void Start()
    {
        mSpriteRenderer = GetComponent<SpriteRenderer>();
        InitColorSwapTex();
       // StartHitEffect();
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
