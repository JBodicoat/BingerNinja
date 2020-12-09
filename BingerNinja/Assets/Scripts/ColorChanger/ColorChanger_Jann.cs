// Jann

/// Swaps the pixels with a given color to the desired color

// Jann 29/10/20 - Proof of concept implementation
// Jann 01/11/20 - Inspector UI, finalizing implementation
// Jann 03/11/20 - Added TilemapRenderer and Image (UI) support
// Jann 07/11/20 - Materials are now assigned in code and don't need to be set for everything in Unity
// Jann 08/11/20 - Turned it into a singleton (used by HitEffect_Elliot)
// Elliott 20/11/2020 = made color arry public
// Jann 22/11/20 - Refactor and added particle system support
// Jann 08/12/20 - Single SpriteRenderers can now be updated

using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ColorChanger_Jann : Singleton_Jann<ColorChanger_Jann>
{
    public Material m_swapMaterial;

    public Color m_colorOutGrey60 = new Color(0, 0, 0, 1);
    public Color m_colorOutGrey122 = new Color(0, 0, 0, 1);
    public Color m_colorOutGrey174 = new Color(0, 0, 0, 1);

    public OriginalColor m_particleColor = OriginalColor.Grey60;

    private List<Image> m_sliderBackgrounds = new List<Image>();
    private List<Image> m_sliderFills = new List<Image>();

    public OriginalColor m_sliderBackgroundColor = OriginalColor.Grey60;
    public OriginalColor m_sliderFillColor = OriginalColor.Grey122;
    
    public OriginalColor m_dialogueTextColor = OriginalColor.Grey122;

    public enum OriginalColor
    {
        Grey60 = 60,
        Grey122 = 122,
        Grey174 = 174
    }

    private List<Renderer> m_renderers;
    private List<Image> m_images;
    private List<ParticleSystem> m_particleSystems;
    private Text[] m_dialogueTexts;

    private Texture2D m_colorSwapTexture;
    public Color[] m_spriteColors;

    private void Awake()
    {
        base.Awake();
        
        m_images = FindObjectsOfTypeAll<Image>();
        m_particleSystems = FindObjectsOfTypeAll<ParticleSystem>();
        m_renderers = FindObjectsOfTypeAll<Renderer>();

        GameObject dialogueManager = GameObject.Find("DialogManager");
        if (dialogueManager != null)
        {
            m_dialogueTexts = dialogueManager.GetComponentsInChildren<Text>(true);
            m_images.AddRange(dialogueManager.GetComponentsInChildren<Image>(true));
        }

        // Get slider reference
        GetSliderImages("HealthSlider");
        GetSliderImages("HungerSlider");
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

    public void UpdateColor(SpriteRenderer spriteRenderer)
    {
        spriteRenderer.material = m_swapMaterial;
    }
    
    private void ApplyColorsToSceneObjects()
    {
        foreach (Renderer renderer in m_renderers)
        {
            renderer.material = m_swapMaterial;
        }
        
        foreach (Image image in m_images)
        { 
            image.material = m_swapMaterial;
        }

        foreach (Image image in m_sliderBackgrounds)
        {
            image.color = m_spriteColors[(int) m_sliderBackgroundColor];
        }

        // Set slider color
        m_sliderFills.ForEach(i => i.color = m_spriteColors[(int) m_sliderFillColor]);
        m_sliderBackgrounds.ForEach(i => i.color = m_spriteColors[(int) m_sliderBackgroundColor]);

        foreach (Image image in m_images)
        {
            image.material = m_swapMaterial;
        }

        foreach (ParticleSystem particleSystem in m_particleSystems)
        {
            var main = particleSystem.main;
            main.startColor = m_spriteColors[(int) m_particleColor];
        }

        foreach (Text text in m_dialogueTexts)
        {
            text.color = m_spriteColors[(int) m_dialogueTextColor];
        }
    }

    private void SwapColor(int index, Color color)
    {
        m_spriteColors[index] = color;
        m_colorSwapTexture.SetPixel(index, 0, color);
    }

    private void GetSliderImages(string gameObjectName)
    {
        GameObject slider = GameObject.Find(gameObjectName);
        if (slider != null)
        {
            m_sliderBackgrounds.Add(slider.transform.Find("Background").GetComponent<Image>());
            m_sliderFills.Add(slider.transform.Find("Fill Area").transform.Find("Fill").GetComponent<Image>());
        }
    }
    
    // This get all Components even of inactive GameObjects
    private List<T> FindObjectsOfTypeAll<T>()
    {
        return SceneManager.GetActiveScene().GetRootGameObjects()
            .SelectMany(g => g.GetComponentsInChildren<T>(true))
            .ToList();
    }
}