//Elliott 

// this class is for the player to signal if he has been seen or not

//Elliott 30/12/2020 made it so when the player is seen and eye will apper on top of his head and when enemy loses sight, an closed eye appers 
using System.Collections;
using UnityEngine;

public class PlayerSpoted_Elliott : MonoBehaviour
{

    public GameObject m_eyeOpen;
    public GameObject m_eyeCosed;
    public float m_timer = 1;
    private bool m_doSeenOnce = false;
    private bool m_doLostOnce = true;

    private GameObject[] m_allEnemies;

    public void EyeNowOpened()
    {
        m_eyeOpen.SetActive(true);
    }

    public IEnumerator EyeNowShut()
    {
        m_eyeOpen.SetActive(false);
        m_eyeCosed.SetActive(true);
        yield return new WaitForSeconds(m_timer);
        m_eyeCosed.SetActive(false);
    }

    public void SeenByEnemy()
    {
        EyeNowOpened();
        m_doSeenOnce = true;
        m_doLostOnce = false;
    }

    public void EnemyLostSight()
    {
        StartCoroutine(EyeNowShut());
        m_doLostOnce = true;
        m_doSeenOnce = false;
    }

    /// <summary>
    /// is the player seen by any enemy
    /// </summary>
    /// <returns>weather it has been seen by any enemy</returns>
    public bool IsPlayerSeen()
    {
        for (int i = 0; i < m_allEnemies.Length; i++)
        {
           
            if (m_allEnemies[i].GetComponent<BaseEnemy_SebastianMol>().m_playerDetected)
            {
                return true;
            }
        }
        return false;
    }

    // Start is called before the first frame update
    void Start()
    {
        m_allEnemies = GameObject.FindGameObjectsWithTag("Enemy");
        m_eyeCosed.SetActive(false);
        m_eyeOpen.SetActive(false);

    }

    // Update is called once per frame
    void Update()
    {
        if(m_allEnemies == null)
        {
            if (!m_doLostOnce) EnemyLostSight();
        }

        if (IsPlayerSeen())
        {
            if (!m_doSeenOnce) SeenByEnemy();
        }
        else
        {
            if (!m_doLostOnce) EnemyLostSight();
        }
        if(m_allEnemies.Length == 0)
        {
            if (!m_doLostOnce) EnemyLostSight();
        }
    }
}
