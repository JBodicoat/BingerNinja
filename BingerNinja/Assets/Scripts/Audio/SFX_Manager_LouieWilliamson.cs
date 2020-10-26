using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFX_Manager_LouieWilliamson : MonoBehaviour
{
    public List<string> NameOfClips = new List<string>();
    public List<AudioClip> ListOfClips = new List<AudioClip>();
    private Dictionary<string, AudioClip> SFXDictionary = new Dictionary<string, AudioClip>();

    public GameObject SFXPrefab;
    void Start()
    {
        for (int i = 0; i < NameOfClips.Count; i++)
        {
            SFXDictionary.Add(NameOfClips[i], ListOfClips[i]);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void PlaySFX(string clipName)
    {
        if (SFXDictionary.ContainsKey(clipName))
        {
            AudioSource SoundEffect = Instantiate(SFXPrefab).GetComponent<AudioSource>();
            SoundEffect.PlayOneShot(SFXDictionary[clipName]);
            Destroy(SoundEffect.gameObject, SFXDictionary[clipName].length);
        }
    }
}
