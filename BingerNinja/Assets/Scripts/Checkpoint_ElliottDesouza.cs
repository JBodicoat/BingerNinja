//Elliott Desouza
// my class saves the check point
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
