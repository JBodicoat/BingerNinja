using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
public class Timeline_Script : MonoBehaviour
{
    public PlayableDirector playableDirector;
    public GameObject nextButton;
    public float seconds;
    
    public void PlayTimeline()
    {
        playableDirector.Play();
        nextButton.SetActive(false);
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
        nextButton.SetActive(true);
    }
}
