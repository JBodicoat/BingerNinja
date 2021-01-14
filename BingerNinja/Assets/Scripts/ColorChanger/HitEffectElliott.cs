//Elliott Desouza

//Elliott 07/10/2020 - made it so it applys a texture to the object.
//Elliott 30/12/2020  added  = transform.Find("EnemySprite") to the code in the start function.
// Jann   11/12/2020 - This class now uses its own colour changer which fixes two bugs 

using UnityEngine;

/// this script changes the color to an object that has been hit
public class HitEffectElliott : MonoBehaviour
{
    public int q = 174;
    public int w = 122;
    internal SpriteRenderer e;
    internal Texture2D r;
    internal Color[] t;
    float y = 0.0f;
    const float u = 0.3f;
    private Color[] o;

    //Sets every pixel’s color of the sprite to the passed color
    public void p(Color a)
    {
        for (int i = 0; i < r.width; ++i)
        {
            r.SetPixel(i, 0, a);
            r.Apply();
        }
    }

    // Resets the sprites original color 
    public void s()
    {
        for (int i = 0; i < r.width; ++i)
        {
            r.SetPixel(i, 0, o[i]);
            r.Apply();
        }
    }

    public void WL(bool d)
    {
        y = u;
        p(o[d ? q : w]);
    }
    
    public void f()
    {
        Texture2D g = new Texture2D(256, 1, TextureFormat.RGBA32, false, false);
        g.filterMode = FilterMode.Point;

        for (int i = 0; i < g.width; ++i)
        {
            g.SetPixel(i, 0, new Color(0.0f, 0.0f, 0.0f, 0.0f));
        }
        g.Apply();

        e.material.SetTexture("_SwapTex", g);

        t = new Color[g.width];
        r = g;
    }
    
    private void h(int j, Color k)
    {
        t[j] = k;
        r.SetPixel(j, 0, k);
    }

    // Start is called before the first frame update
    void Start()
    {
        if (gameObject.CompareTag(Tags_JoaoBeijinho.QC))
        {
            e = GetComponent<SpriteRenderer>();
        }
        else
        {
            e = GetComponentInChildren<SpriteRenderer>();
        }
        o = ColorChanger_Jann.Instance.f;
        
        f();
        
        h(60, o[60]);
        h(122, o[122]);
        h(174, o[174]);
        r.Apply();
    }

    // Update is called once per frame
    void Update()
    {
        if (y > 0.0f)
        {
            y -= Time.deltaTime;
            if (y <= 0.0f)
                s();
        }
    }
}
