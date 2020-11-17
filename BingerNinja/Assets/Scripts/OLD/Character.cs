using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class Character : MonoBehaviour
{
    private Rigidbody2D rb;

    private Vector2 direction = new Vector2(0, 0);
    private float speed = 5.0f;

    private const float maxHealth = 100.0f;
    private float currentHealth = maxHealth;

    private const float maxHunger = 100.0f;
    private float currentHunger = maxHunger;

    private const float fullnessDrainRate = 5.0f;
    private const float healthDrainRate = 7.5f;

    public Slider healthSlider;
    public Slider hungerSlider;

    private Weapon_Morgan weaponScript;

    internal bool isStealthed = false;

    // Start is called before the first frame update
    void Start()
    {
        Physics2D.gravity = Vector2.zero;
        rb = GetComponent<Rigidbody2D>();

        healthSlider.maxValue = maxHealth;
        healthSlider.value = currentHealth;

        hungerSlider.maxValue = maxHunger;
        hungerSlider.value = currentHunger;

        weaponScript = FindObjectOfType<Weapon_Morgan>();
    }

    // Update is called once per frame
    void Update()
    {
        /* Done in "PlayerMovement_MarioFernandes" script
        var curKeyboard= Keyboard.current;

        // Movement
        direction = new Vector2(0, 0);

        if (curKeyboard.aKey.isPressed)
        {
            direction.x = -1;
		}
        else if(curKeyboard.dKey.isPressed)
        {
            direction.x = 1;
		}

        if (curKeyboard.wKey.isPressed)
        {
            direction.y = 1;
        }
        else if(curKeyboard.sKey.isPressed)
        {
            direction.y = -1;
		}

        direction.Normalize();
		direction *= speed;

		rb.velocity = direction;
        */
        // Hunger
        currentHunger -= fullnessDrainRate * Time.deltaTime;
        hungerSlider.value = currentHunger;
        if (currentHunger < 0)
            currentHunger = 0;

        /* Commented out for new input system
        if(Input.GetKeyDown(KeyCode.E))
        {
            weaponScript.EatWeapon();
        }
        */

        if (currentHunger == 0)
        {
            Hit(healthDrainRate * Time.deltaTime);
		}
	}

    // Increase players health by amount passed
    public void Eat(float amount)
    {
        currentHunger += amount;
        if(currentHunger > maxHunger)
        {
            currentHunger = maxHunger;
		}

        hungerSlider.value = currentHunger;
	}

    // Reduce players health by amount passed
    public void Hit(float amount)
    {
        currentHealth -= amount;
        if (currentHealth < 0)
            currentHealth = 0;

        if(currentHealth == 0)
        {
            Die();
		}

        healthSlider.value = currentHealth;
    }

    // Increase players health by amount passed
    public void Heal(float amount)
    {
        currentHealth += amount;
        if(currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
		}

        healthSlider.value = currentHealth;
	}

    // Run death sequence
    private void Die()
    {
        gameObject.SetActive(false);
        print("GAME OVER");
	}
}
