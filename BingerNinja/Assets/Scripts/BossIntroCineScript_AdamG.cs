using UnityEngine;
using Cinemachine;
using UnityEngine.Playables;

public class BossIntroCineScript_AdamG : MonoBehaviour
{
    [SerializeField]
    private PlayableDirector ZoomIn;
    [SerializeField]
    private PlayableDirector ZoomOut;
    [SerializeField]
    private CinemachineVirtualCamera playerCam;
    [SerializeField]
    private CinemachineVirtualCamera bossCam;

    //public Playable zoomIn;
    //public Playable zoomOut;

    // Start is called before the first frame update
    void Awake()
    {
        ZoomIn = GameObject.Find("TimelineIn").GetComponent<PlayableDirector>();
        ZoomOut = GameObject.Find("TimelineOut").GetComponent<PlayableDirector>();
        //zoomIn = ZoomIn.GetComponent<Playable>();
        //zoomOut = ZoomOut.GetComponent<Playable>();
    }

    public void PlayZoomIn()
    {
        ZoomIn.Play();
        bossCam.Priority = 11;
    }

    public void StopZoomIn()
    {

        ZoomIn.Stop();
    }

    public void PlayZoomOut()
    {
        ZoomOut.Play();
        bossCam.Priority = 9;
    }

    public void StopZoomOut()
    {
        ZoomOut.Stop();
    }
}
