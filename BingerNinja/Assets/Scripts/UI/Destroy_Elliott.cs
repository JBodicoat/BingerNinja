// Elliott

// class destroys the an object it is attached to.

//Elliott 30/12/2020 set a timer and destoryer game object
using UnityEngine;

public class Destroy_Elliott : MonoBehaviour
{

    public float m_lifeTime = 1.5f; //This implies a delay of 2 seconds.

   
    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, m_lifeTime);
    }

}
