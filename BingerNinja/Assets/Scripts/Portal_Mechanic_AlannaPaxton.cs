using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// alanna 09/11/20, needs to be added onto player object, then create portals and tag them appropriately. Also add animation if wanted.

public class Portal_Mechanic_AlannaPaxton : MonoBehaviour
{
    
    protected GameObject m_player;
    // Start is called before the first frame update
    void Start()
    {
        m_player= GameObject.FindGameObjectWithTag("Player");
       
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.childCount>0)
        {
          //  this.transform.position = collision.transform.GetChild(0);
        }
        if(collision.transform.parent != null)
        {
            this.transform.position = collision.transform.parent.position;
        }
       
    }
}
