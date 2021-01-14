//Louie Williamson
///This script handles the functionality for playing music and sound effects.
///It also contains 2 dictionaries. One each for sfx and music.

using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioManager_LouieWilliamson : MonoBehaviour
{
    //Sound Effect Enum and Dictionary setup
    public enum q { w, e, r, t, y, u, i, o };
    public List<AudioClip> p = new List<AudioClip>();
    private Dictionary<q, AudioClip> a = new Dictionary<q, AudioClip>();
    public GameObject d;
   // private PlayTrack_Jann music;

    //Music enum and Dictionary setup
    public enum f { g, h };
    public AudioSource j;
    public List<AudioClip> k = new List<AudioClip>();
    private Dictionary<f, AudioClip> l = new Dictionary<f, AudioClip>();
    
    //This function plays a sfx given an enum value from the SFX enum
    public void z(q s)
    {
        //spawn sfx prefab and get audio source component
        AudioSource x = Instantiate(d).GetComponent<AudioSource>();
        //play sfx
        x.PlayOneShot(a[s]);
        //destroy prefab instance when sound has played
        Destroy(x.gameObject, a[s].length);
    }
    //This function sets the music to the track given through a Music enum value
    public void c(f m)
    {
        j.clip = l[m];
        j.Play();
    }

    //This function pauses/ plays the music depending on the bool value given
    public void v(bool b) //Jack or Mario I know this is a bad bool name so pls advise on what I should call it XD
    {
        if (b)
        {
            j.Play();
        }
        else
        {
            j.Pause();
        }
    }
    
    void Start()
    {
        //Populate Dictionaries
        a.Add(q.w, p[0]);
        a.Add(q.e, p[1]);
        a.Add(q.r, p[2]);
        a.Add(q.t, p[3]);
        a.Add(q.y, p[4]);
        a.Add(q.u, p[5]);
        a.Add(q.i, p[6]);
        a.Add(q.o, p[7]);

        l.Add(f.g, k[0]);
        l.Add(f.h, k[1]);

        n();
    }


    void Update()
    {

    }
    private void n()
    {
        int m = SceneManager.GetActiveScene().buildIndex;

        switch (m)
        {
            case 0:
                PlayTrack_Jann.Instance.q(AudioFiles.h);
                // SetMusicSource(Music.MainMenu);
                break;
            case 1:
                PlayTrack_Jann.Instance.q(AudioFiles.i);
              //  SetMusicSource(Music.Level1);
                break;
            case 2:
                PlayTrack_Jann.Instance.q(AudioFiles.i);
                //SetMusicSource(Music.Level1);
                break;
            case 3:
                PlayTrack_Jann.Instance.q(AudioFiles.i);
                break;
            case 4:
                PlayTrack_Jann.Instance.q(AudioFiles.j);
                break;
            case 5:
                PlayTrack_Jann.Instance.q(AudioFiles.j);
                break;
            case 6:
                PlayTrack_Jann.Instance.q(AudioFiles.j);
                break;
            case 7:
                PlayTrack_Jann.Instance.q(AudioFiles.k);
                break;
            case 8:
                PlayTrack_Jann.Instance.q(AudioFiles.k);
                break;
            case 9:
                PlayTrack_Jann.Instance.q(AudioFiles.k);
                break;
            case 10:
                PlayTrack_Jann.Instance.q(AudioFiles.l);
                break;
            case 11:
                PlayTrack_Jann.Instance.q(AudioFiles.l);
                break;
            case 12:
                PlayTrack_Jann.Instance.q(AudioFiles.l);
                break;
            case 13:
                PlayTrack_Jann.Instance.q(AudioFiles.m);
                break;
            case 14:
                PlayTrack_Jann.Instance.q(AudioFiles.m);
                break;
            case 15:
                PlayTrack_Jann.Instance.q(AudioFiles.m);
                break;
            case 16:
                PlayTrack_Jann.Instance.q(AudioFiles.n);
                break;
            case 17:
                PlayTrack_Jann.Instance.q(AudioFiles.n);
                break;
            case 18:
                PlayTrack_Jann.Instance.q(AudioFiles.n);
                break;
            case 19:
                PlayTrack_Jann.Instance.q(AudioFiles.o);
                break;
            case 20:
                PlayTrack_Jann.Instance.q(AudioFiles.o);
                break;
            case 21:
                PlayTrack_Jann.Instance.q(AudioFiles.h);
                break;
            default:
                break;
        }
    }
}
