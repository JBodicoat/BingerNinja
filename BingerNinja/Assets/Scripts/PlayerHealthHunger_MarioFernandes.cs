// Mário Fernandes
/// My class takes care of Health and Hunger

// Mário 16/10/2020 - Abstract Health&Hunger code from old Character class
// Mário 18/10/2020 - Add max health increase/decrease functions
// Elliott 19/10/2020 - Added respawn on Death

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealthHunger_MarioFernandes : MonoBehaviour
{
    protected float m_maxHealth = 100.0f;
    protected float m_currentHealth = 100.0f;

    protected float m_maxHunger = 100.0f;
    protected float m_currentHunger = 100.0f;

    protected float m_fullnessDrainRate = 5.0f;
    protected float m_healthDrainRate = 7.5f;

    public Slider m_healthSlider;
    public Slider m_hungerSlider;

    // Increase players health by amount passed
    public void Eat(float amount)
    {
        m_currentHunger += amount;
        if(m_currentHunger > m_maxHunger)
        {
            m_currentHunger = m_maxHunger;
		}

        m_hungerSlider.value = m_currentHunger;
	}

    // Reduce players health by amount passed
    public void Hit(float amount)
    {
        m_currentHealth -= amount;
        if (m_currentHealth < 0)
            m_currentHealth = 0;

        if(m_currentHealth == 0)
        {
            Die();
		}
        
        print(m_currentHealth);
        m_healthSlider.value = m_currentHealth;
    }

    // Increase players health by amount passed
    public void Heal(float amount)
    {
        m_currentHealth += amount;
        if(m_currentHealth > m_maxHealth)
        {
            m_currentHealth = m_maxHealth;
		}

        m_healthSlider.value = m_currentHealth;
	}

    public void SetMaxHealth(float amount)
    {
        m_maxHealth = amount;
    }

    public void IncreaseMaxHealt(float amount)
    {
        m_maxHealth += amount;
    }
     public void DecreaseMaxHealt(float amount)
    {
        m_maxHealth -= amount;
    }

        // Run death sequence
    private void Die()
    {
        // gameObject.SetActive(false);
        gameObject.transform.position = GameObject.FindGameObjectWithTag("SaveCheckpoint").GetComponent<SaveSystem_ElliottDesouza>().m_currentCheckpoint.position;
        m_currentHealth = 100;
        m_currentHunger = 100;
        print("GAME OVER");
	}

    // Start is called before the first frame update
    void Start()
    {
        m_healthSlider.maxValue = m_maxHealth;
        m_healthSlider.value = m_currentHealth;

        m_hungerSlider.maxValue = m_maxHunger;
        m_hungerSlider.value = m_currentHunger;
    }

    // Update is called once per frame
    void Update()
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

