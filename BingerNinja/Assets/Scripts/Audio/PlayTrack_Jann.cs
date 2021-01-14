// Jann

/// Plays the selected track

// Jann 25/10/20 - Playing a track from a .json file implemented
// Jann 28/10/20 - Singleton, play sound/music methods, QA improvements
// Jann 11/11/20 - Bugfix and new Singleton applied
// Jann 25/11/20 - Adapt to smaller .json files

using UnityEngine;

public class PlayTrack_Jann : Singleton_Jann<PlayTrack_Jann>
{
    private int SF = 48000;

    [SerializeField] private TextAsset w;
    
    [SerializeField] private float e = 0.1f;
    [SerializeField] private float r = 0.1f;
    [SerializeField] private bool t;
    
    private int pos;
    private float cf;

    [SerializeField] private AudioSource[] y; // Three channels for music
    [SerializeField] private AudioSource[] u; // Five channels for sounds

    private void Awake()
    {
        base.Awake();
        
        Cursor.lockState = CursorLockMode.Confined;

        o sd = SaveLoadSystem_JamieG.i();
        if (!sd.Equals(default(o)))
        {
            p(sd.a);
            s(sd.d);
        }

        foreach (AudioSource ms in y)
        {
            ms.volume = r;
            ms.loop = t;
        }
        
        foreach (AudioSource ss in u)
        {
            ss.volume = e;
        }
    }
    
    private void Start()
    {
        if (w != null)
        {
            g(w);
        }
    }

    public void WS(string an)
    {
        Track_Jann t = g(an);
        AudioClip c = h(t.n, (float)t.b / 60, t.f[0]);

        foreach (AudioSource ss in u)
        {
            ss.clip = c;
            if (!ss.isPlaying)
            {
                ss.Play();
                break;
            }
            else if(ss.clip.name == t.n)
            { 
                ss.Stop();
                ss.Play();
                break;
            }
        }
    }

    public void q(string an)
    {
        Track_Jann t = g(an);

        for (int i = 0; i < t.f.Length; i++)
        {
            AudioClip c = h(t.n, (float) t.b / 60, t.f[i]);
            y[i].clip = c;
            y[i].Play();
        }
    }
    
    public void g(TextAsset tf)
    {
        Track_Jann t = JsonUtility.FromJson<Track_Jann>(tf.text);
        t.j();
            
        for (int i = 0; i < t.f.Length; i++)
        {
            AudioClip c = h(t.n, (float) t.b / 60, t.f[i]);
            y[i].clip = c;
            y[i].Play();
        }
    }

    public Track_Jann g(string an)
    {
        TextAsset tf = AudioFiles.q(an);
        Track_Jann t = JsonUtility.FromJson<Track_Jann>(tf.text);
        t.j();

        return t;
    }
    
    // Creates an AudioClip from the notes in a channel
    private AudioClip h(string tn, float bps, int[] f)
    {
        int ls = (int) (SF / bps);
        float[] d = new float[ls * f.Length];
        int cl = 0;
        
        for (int i = 0; i < f.Length; i++)
        {
            cf = f[i];
            AudioClip c = AudioClip.Create("", ls, 1, SF, false, k, l);
            
            float[] buffer = new float[c.samples];
            c.GetData(buffer, 0);
            buffer.CopyTo(d, cl);
            cl += c.samples;
        }
        
        AudioClip nc = AudioClip.Create(tn, cl, 1, SF, false);
        nc.SetData(d, 0);
        return nc;
    }

    // Called once at clip creation
    void k(float[] d)
    {
        int c = 0;
        while (c < d.Length)
        {
            // Sets data[count] to -1 or 1, resulting in 8-bit like sounds
            d[c] = Mathf.Sign(Mathf.Sin(2 * Mathf.PI * cf * pos / SF));
            
            pos++;
            c++;
        }
    }
    
    // Called when track loops or changes playback position
    void l(int np)
    {
        pos = np;
    }


    public void p(float v)
    {
        r = v;

        foreach (var z in y)
        {
            z.volume = v / 30f;
        }
    }

    public void s(float v)
    {
        e = v;
        
        foreach (var x in u)
        {
            x.volume = v / 50f;
        }
    }
}
