// Mário Fernandes
/// My class takes care of Health and Hunger

// Mário 16/10/2020 - Abstract Health&Hunger code from old Character class
// Mário 18/10/2020 - Add max health increase/decrease functions
// Elliott 19/10/2020 - Added respawn on Death
// Elliott 21/11/2020 - Added death effect and pause input
//Alanna 10/12/2020 added sound effects for player death and damage to player

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

    public bool m_paused = false;
    public Slider m_healthSlider;
    public Slider m_hungerSlider;

    public Slider m_buffSlider;
    protected float m_currentTime =0;
    protected float m_startingTime =0;

    private PlayerDeathEffect_Elliott m_DeathEffect;
    private PlayerController_JamieG m_PauseInput;

    public void Buff (int weapon)
    {
        m_currentTime = m_startingTime;

        switch (weapon)
        {
            case 1: //fugu
                m_startingTime = 5;
                m_currentTime -= 1 * Time.deltaTime;
                m_buffSlider.value = m_currentTime;
                break;
            case 2: //noodles
                m_startingTime = 15;
                m_currentTime -= 1 * Time.deltaTime;
                m_buffSlider.value = m_currentTime;
                break;
            case 3: //tempura
                m_startingTime = 30;
                m_currentTime -= 1 * Time.deltaTime;
                m_buffSlider.value = m_currentTime;
                break;
            case 4: //sake
                m_startingTime = 30;
                m_currentTime -= 1 * Time.deltaTime;
                m_buffSlider.value = m_currentTime;
                break;
            case 5://sashimi
                m_startingTime = 5;
                m_currentTime -= 1 * Time.deltaTime;
                m_buffSlider.value = m_currentTime;
                break;
            case 6://kobe beef
                m_startingTime = 5;
                m_currentTime -= 1 * Time.deltaTime;
                m_buffSlider.value = m_currentTime;
                break;
            default:
                break;
        }
        /// if (food is equal to noodles)
        /// {
        ///     starting time == blah blah
        ///     slider will move in relation to how long buff is active
        /// }
        /// if ( food is equal to sake) 
        /// {
        ///  do the same crap but with a shorter time ect.
        /// }

    }


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
            PlayTrack_Jann.Instance.PlaySound(AudioFiles.Sound_Death);
            m_DeathEffect.SpriteFlash();
            //Invoke("Die", 2f);
            Die();
        }
        
        //print(m_currentHealth);
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
        gameObject.transform.position = GameObject.FindGameObjectWithTag("SaveCheckpoint").GetComponent<SaveSystem_ElliottDesouza>().m_currentCheckpoint.position;
        m_currentHealth = m_maxHealth;
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

        m_DeathEffect = GetComponent<PlayerDeathEffect_Elliott>();
        m_PauseInput = GetComponent<PlayerController_JamieG>();

    }

    // Update is called once per frame
    void Update()
    {
        m_buffSlider.maxValue = m_startingTime;
        m_buffSlider.value = m_currentTime;

        if (!m_paused)
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

