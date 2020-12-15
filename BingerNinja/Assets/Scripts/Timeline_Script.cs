using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
public class Timeline_Script : MonoBehaviour
{
    public PlayableDirector[] timeline;
    public PlayableDirector playableDirector;
    public GameObject nextButton;
    void Awake()
    {
        playableDirector = timeline[0];
        playableDirector.playableGraph.GetRootPlayable(0).SetSpeed(1);
    }  
    public void PlayTimeline()
    {
        if (playableDirector != null)
        playableDirector.playableGraph.GetRootPlayable(0).SetSpeed(1);
        //nextButton.SetActive(false);
    }
    public void PauseTimeline()
    {
        playableDirector.playableGraph.GetRootPlayable(0).SetSpeed(0);
       // nextButton.SetActive(true);
    }
    public void ChangeDirector(string directorName = "Good Ending / Bad Ending")
    {
        if (directorName == "Good Ending")
        {
            playableDirector = timeline[1];
            playableDirector.Play();
        }
        if (directorName == "Bad Ending")
        {
            playableDirector = timeline[2];
            playableDirector.Play();
        }
    }
        
    
}
