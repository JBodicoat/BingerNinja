using UnityEngine;

public class CameraTransparencySort_LouieWilliamson : MonoBehaviour
{
    // Start is called before the first frame update
    private Camera gameCamera;
    private Vector3 YaxisSortMode = new Vector3(0.0f, 1.0f, 0.0f);
    private void Awake()
    {
        gameCamera = gameObject.GetComponent<Camera>();
        gameCamera.transparencySortMode = TransparencySortMode.Orthographic;
        gameCamera.transparencySortAxis = YaxisSortMode;
        
    }
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        print(gameCamera.transparencySortAxis + " : " + gameCamera.transparencySortMode);
    }
    
}
