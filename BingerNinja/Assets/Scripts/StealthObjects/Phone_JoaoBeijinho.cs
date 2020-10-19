//Joao Beijinho
///This class handles interaction with phone, makes player stealth for a while but unable to move

//Joao Beijinho 18/10/2020 - Created draft for
//Update Movement

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Phone_JoaoBeijinho : MonoBehaviour
{
    StealthObject_JoaoBeijinho steathObjectScript;
    PlayerMovement_MarioFernandes playerMovementScript;

    //Variable to store player speed
    private float playerSpeed;

    // Start is called before the first frame update
    void Start()
    {

        steathObjectScript = FindObjectOfType<StealthObject_JoaoBeijinho>();
        playerMovementScript = FindObjectOfType<PlayerMovement_MarioFernandes>();
        
        //Store player movement speed
        playerSpeed = playerMovementScript.m_speed;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        steathObjectScript.Hide();

        StartCoroutine(PhoneDuration());
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        steathObjectScript.Hide();

        playerMovementScript.m_speed = playerSpeed;
    }

    IEnumerator PhoneDuration()
    {
        playerMovementScript.m_speed = 0;

        yield return new WaitForSeconds(5);
    }

    // Update is called once per frame
    //void Update()
    //{
    //    
    //}
}
