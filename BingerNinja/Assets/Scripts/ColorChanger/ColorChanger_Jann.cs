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
// Jann 11/12/20 - Fixed bug where the HitEffect would collide with the colour changer

using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ColorChanger_Jann : Singleton_Jann<ColorChanger_Jann>
{
    public Material q;

    public Color w = new Color(0, 0, 0, 1);
    public Color e = new Color(0, 0, 0, 1);
    public Color r = new Color(0, 0, 0, 1);

    public y t = y.u;

    private List<Image> sb = new List<Image>();
    private List<Image> sf = new List<Image>();

    public y o = y.u;
    public y p = y.a;
    
    public y s = y.a;

    public enum y
    {
        u = 60,
        a = 122,
        d = 174
    }

    private List<Renderer> rs;
    private List<Image> imgs;
    private List<ParticleSystem> pss;
    private List<Text> txts;

    private Texture2D cst;
    public Color[] f;

    void Awake()
    {
        base.Awake();
        
        imgs = FOTA<Image>();
        pss = FOTA<ParticleSystem>();
        rs = FOTA<Renderer>();
        txts = FOTA<Text>();

        // Get slider reference
        GSI("HealthSlider");
        GSI("HungerSlider");
        
        ICC();
        
        ACSO();
    }

    private void ICC()
    {
        Texture2D cST = new Texture2D(256, 1, TextureFormat.RGBA32, false, false);
        cST.filterMode = FilterMode.Point;

        for (int i = 0; i < cST.width; ++i)
            cST.SetPixel(i, 0, new Color(0.0f, 0.0f, 0.0f, 0.0f));

        cST.Apply();

        q.SetTexture("_SwapTex", cST);

        f = new Color[cST.width];
        cst = cST;
        
        SC((int) y.u, w);
        SC((int) y.a, e);
        SC((int) y.d, r);
        cst.Apply();
    }

    public void g(SpriteRenderer sr)
    {
        sr.material = q;
    }

    private void SC(int i, Color c)
    {
        f[i] = c;
        cst.SetPixel(i, 0, c);
    }
    
    private void ACSO()
    {
        foreach (Renderer rr in rs)
        {
            rr.material = q;
        }

        foreach (Image img in sb)
        {
            img.color = f[(int) o];
        }

        // Set slider color
        sf.ForEach(i => i.color = f[(int) p]);
        sb.ForEach(i => i.color = f[(int) o]);

        foreach (Image img in imgs)
        {
            img.material = q;
        }

        foreach (ParticleSystem ps in pss)
        {
            var m = ps.main;
            m.startColor = f[(int) t];
        }

        foreach (Text t in txts)
        {
            t.color = f[(int) s];
        }
    }

    private void GSI(string h)
    {
        GameObject s = GameObject.Find(h);
        if (s != null)
        {
            sb.Add(s.transform.Find("Background").GetComponent<Image>());
            sf.Add(s.transform.Find("Fill Area").transform.Find("Fill").GetComponent<Image>());
        }
    }
    
    // This get all Components even of inactive GameObjects
    private List<T> FOTA<T>()
    {
        return SceneManager.GetActiveScene().GetRootGameObjects()
            .SelectMany(g => g.GetComponentsInChildren<T>(true))
            .ToList();
    }
}