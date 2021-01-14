// Mário Fernandes
/// My class takes care of Health and Hunger

// Mário 16/10/2020 - Abstract Health&Hunger code from old Character class
// Mário 18/10/2020 - Add max health increase/decrease functions
// Elliott 19/10/2020 - Added respawn on Death
// Elliott 21/11/2020 - Added death effect and pause input
//Sebastian Mol 10/12/2020 - made enemy health back to full after player death
//Sebastian Mol 12/12/2020 - made enemy loose intrest on player death
//Alanna 10/12/2020 added sound effects for player death and damage to player
// Jann  14/12/2020 - Dying restarts from the last checkpoint

using UnityEngine;
using UnityEngine.UI;

public class PlayerHealthHunger_MarioFernandes : MonoBehaviour
{
    protected float m_maxHealth = 100.0f;
    protected float m_currentHealth = 100.0f;

    protected float m_maxHunger = 100.0f;
    protected float m_currentHunger = 100.0f;

    public float m_fullnessDrainRate = 2.5f;
    protected float m_healthDrainRate = 7.5f;

    public bool m_paused = false;
    public Slider m_healthSlider;
    public Slider m_hungerSlider;

     PlayerDeathEffect_Elliott q;
     PlayerController_JamieG w;

    // Increase players health by amount passed
    public void Eat(float e)
    {
        m_currentHunger += e;
        if(m_currentHunger > m_maxHunger)
        {
            m_currentHunger = m_maxHunger;
		}

        m_hungerSlider.value = m_currentHunger;
	}

    // Reduce players health by amount passed
    public void Hit(float r)
    {
        m_currentHealth -= r;
       
        if (m_currentHealth < 0)
            m_currentHealth = 0;

        if(m_currentHealth == 0)
        {
            PlayTrack_Jann.Instance.EM(AudioFiles.Sound_Death);
            q.SpriteFlash();
            //Invoke("Die", 2f);
            Die();
        }
        
        //print(m_currentHealth);
        m_healthSlider.value = m_currentHealth;
    }

    // Increase players health by amount passed
    public void Heal(float t)
    {
        m_currentHealth += t;
        if(m_currentHealth > m_maxHealth)
        {
            m_currentHealth = m_maxHealth;
		}

        m_healthSlider.value = m_currentHealth;
	}

    public void SetMaxHealth(float t)
    {
        m_maxHealth = t;
    }

    public void IncreaseMaxHealt(float t)
    {
        m_maxHealth += t;
    }
     public void DecreaseMaxHealt(float t)
    {
        m_maxHealth -= t;
    }

        // Run death sequence
     void Die()
    {
        m_currentHealth = m_maxHealth;
        m_currentHunger = 100;
        HealAllEnemies();
        AllEnemiesLooseIntrest();
        
        SceneManager_JamieG.Instance.ResetToCheckpoint();
    }

     void HealAllEnemies()
    {
        GameObject[] y = GameObject.FindGameObjectsWithTag(Tags_JoaoBeijinho.m_enemyTag);
        foreach (GameObject u in y)
        {
            if(u.activeSelf)
            {
                BaseEnemy_SebastianMol i = u.GetComponent<BaseEnemy_SebastianMol>();
                i.I = i.QR;
            }          
        }
    }

     void AllEnemiesLooseIntrest()
    {
        GameObject[] o = GameObject.FindGameObjectsWithTag(Tags_JoaoBeijinho.m_enemyTag);
        foreach (GameObject p in o)
        {
            if (p.activeSelf)
            {
                p.GetComponent<BaseEnemy_SebastianMol>().RJ();
            }
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        m_healthSlider.maxValue = m_maxHealth;
        m_healthSlider.value = m_currentHealth;

        m_hungerSlider.maxValue = m_maxHunger;
        m_hungerSlider.value = m_currentHunger;

        q = GetComponent<PlayerDeathEffect_Elliott>();
        w = GetComponent<PlayerController_JamieG>();

    }

    // Update is called once per frame
    void Update()
    {
        if(!m_paused)
        {
            m_currentHunger -= m_fullnessDrainRate * Time.deltaTime;
            m_hungerSlider.value = m_currentHunger;
            if (m_currentHunger < 0)
                m_currentHunger = 0;



            if (m_currentHunger == 0)
            {
                Hit(m_healthDrainRate * Time.deltaTime);
		    }
        }
    }


}

