//Elliott Desouza
/// my class creates a check point

//Elliott 19/10/2020 - added collision which sets the current checkpoint
//Jann    06/11/2020 - Added saving the position of the checkpoint

using UnityEngine;

public class Checkpoint_ElliottDesouza : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            GameObject.FindGameObjectWithTag("SaveCheckpoint").GetComponent<SaveSystem_ElliottDesouza>().m_currentCheckpoint = gameObject.transform;
            // SaveLoadSystem_JamieG.SaveCheckpoint(gameObject.transform.position);
        }
    }
}
