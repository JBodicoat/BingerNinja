//Elliott Desouza

//Elliott 07/11/2020 - implented the shake
//Elliott 09/11/2020 - made public variables so its easyer to edit
using System.Collections;
using UnityEngine;

/// this script makes the main camara shake
public class CameraShakeElliott : MonoBehaviour
{
    public float m_duration;
    public float m_magnitude;
    public float m_cameraLerp;
    public IEnumerator Shake(float duration, float magnitude)
    {
        duration = m_duration;
        magnitude = m_magnitude;

        Vector3 orgignalPos = transform.localPosition;

        float elapsed = 0.0f;

        while (elapsed < m_duration)
        {
            float x = Random.Range(-1f, 1f) * m_magnitude;
            float y = Random.Range(-1f, 1f) * m_magnitude;

            transform.localPosition = new Vector3(x, y, orgignalPos.z);

            elapsed += Time.deltaTime;

            yield return null;
        }
        transform.localPosition = orgignalPos;
    }

    //private void LateUpdate()
    //{       
    //    Vector2.Lerp(transform.position, playerTrans.position, m_cameraLerp);
    //}

    //private void Start()
    //{
    //    playerTrans = transform.parent;
    //    transform.parent = null;
    //}

    public void StartShake()
    {
        StartCoroutine(Shake(m_duration, m_magnitude));
    }


}
