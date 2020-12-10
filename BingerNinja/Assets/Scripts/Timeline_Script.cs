using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
public class Timeline_Script : MonoBehaviour
{
    public PlayableDirector playableDirector;
    public float seconds;
    
    public void PlayTimeline()
    {
        playableDirector.Play();
        StartCoroutine(pause());
    }
    public void PauseTimeline()
    {
        playableDirector.Pause();
    }
    IEnumerator pause()
    {
        yield return new WaitForSeconds(seconds);
        PauseTimeline();
    }
}
