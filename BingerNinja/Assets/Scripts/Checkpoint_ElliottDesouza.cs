//Elliott Desouza
/// my class creates a check point

//Elliott 19/10/2020 - added collision which sets the current checkpoint
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint_ElliottDesouza : MonoBehaviour
{

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            GameObject.FindGameObjectWithTag("SaveCheckpoint").GetComponent<SaveSystem_ElliottDesouza>().m_currentCheckpoint = gameObject.transform;

        }
    }


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
