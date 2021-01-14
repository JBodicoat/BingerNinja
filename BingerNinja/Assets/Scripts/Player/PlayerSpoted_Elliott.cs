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
     bool a = false;
     bool b = true;

     GameObject[] c;

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
        a = true;
        b = false;
    }

    public void EnemyLostSight()
    {
        StartCoroutine(EyeNowShut());
        b = true;
        a = false;
    }

    /// <summary>
    /// is the player seen by any enemy
    /// </summary>
    /// <returns>weather it has been seen by any enemy</returns>
    public bool IsPlayerSeen()
    {
        for (int i = 0; i < c.Length; i++)
        {
           
            if (c[i].GetComponent<BaseEnemy_SebastianMol>().CW)
            {
                return true;
            }
        }
        return false;
    }

    // Start is called before the first frame update
    void Start()
    {
        c = GameObject.FindGameObjectsWithTag("Enemy");
        m_eyeCosed.SetActive(false);
        m_eyeOpen.SetActive(false);

    }

    // Update is called once per frame
    void Update()
    {
        if (IsPlayerSeen())
        {
            if (!a) SeenByEnemy();
        }
        else
        {
            if (!b) EnemyLostSight();
        }
        if(c.Length == 0)
        {
            if (!b) EnemyLostSight();
        }
    }
}
