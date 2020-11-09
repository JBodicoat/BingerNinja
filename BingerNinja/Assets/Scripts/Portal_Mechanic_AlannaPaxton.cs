using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// alanna 09/11/20, needs to be added onto player object, then create portals and tag them appropriately. Also add animation if wanted.

public class Portal_Mechanic_AlannaPaxton : MonoBehaviour
{
    protected GameObject m_portal1;
    protected GameObject m_portal2;
    // Start is called before the first frame update
    void Start()
    {
        m_portal1 = GameObject.FindGameObjectWithTag("Portal1");
        m_portal2 = GameObject.FindGameObjectWithTag("Portal2");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.collider.tag == "Portal1")
        {
            this.transform.position = m_portal2.transform.position;
        }
        if(collision.collider.tag == "Portal2")
        {
            this.transform.position = m_portal1.transform.position;
        }
    }
}
