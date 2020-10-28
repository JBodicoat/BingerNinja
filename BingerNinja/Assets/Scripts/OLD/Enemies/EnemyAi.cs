// Jack 20/10 - Changed Update to Execute, a public function that needs to be called. This is for demonstration of the prototype

using System.Collections;
using System.Collections.Generic;
//using UnityEditor.Networking.PlayerConnection;
using UnityEngine;

public class EnemyAi : MonoBehaviour
{
    public Transform playerTransform;
    public float detectionRange;
    public float speed;
    public float bulletSpeed;
    public float shootDeley;
    public GameObject bullet;
    public GameObject aim;
    public enum enemyType { MELEE, RANGE, HEAVY, WHIMP };
    public enemyType type;

    private bool detected = false;
    private float timer = 0;


    private const float maxHealth = 100;
    private float currentHealth = maxHealth;


    Character characterScript;

	private void Start()
	{
        characterScript = FindObjectOfType<Character>();	
	}

	// Update is called once per frame
	public void Execute()
    {
        //if (playerTransform.gameObject.activeSelf && !characterScript.isStealthed)
        if (playerTransform.gameObject.activeSelf)
        {
            switch (type)
            {
                case enemyType.MELEE:
                    meleeBehaviour();
                    break;
                case enemyType.RANGE:
                    rangeBehaviour();
                    break;
                case enemyType.HEAVY:
                    heavyBehaviour();
                    break;
                case enemyType.WHIMP:
                    whimpBehaviour();
                    break;
            }
        }
    }

    public void Hit(float amount)
    {
        currentHealth -= amount;
        if (currentHealth < 0)
            currentHealth = 0;

        if (currentHealth == 0)
        {
            Destroy(gameObject);
        }
    }

    private void meleeBehaviour()
    {
        if (Vector2.Distance(transform.position, playerTransform.position) < detectionRange)
        {
            transform.position = Vector2.MoveTowards(transform.position, playerTransform.position, speed * Time.deltaTime);

        }
    }

    private void heavyBehaviour()
    {
        if (Vector2.Distance(transform.position, playerTransform.position) < detectionRange)
        {
            transform.position = Vector2.MoveTowards(transform.position, playerTransform.position, speed * Time.deltaTime);
        }
    }

    private void whimpBehaviour()
    {
        if (detected)
        {
            if (Vector2.Distance(transform.position, playerTransform.position) < detectionRange)
            {
                //run away
                Vector2 dir = runAway();
                transform.position = transform.position + new Vector3((dir.x * speed) * Time.deltaTime, (dir.y * speed) * Time.deltaTime, 0);
            }
            else
            {
                //shake
            }
        }
        else
        {
            if (Vector2.Distance(transform.position, playerTransform.position) < detectionRange)
            {
                detected = true;
                //run away
            }
        }

    }


    private void rangeBehaviour()
    {
        if(Vector2.Distance(transform.position, playerTransform.position) < detectionRange)
        {

            Vector3 dir = Vector3.Normalize(playerTransform.position - transform.position);
            float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
            aim.transform.eulerAngles = new Vector3(0, 0, angle);
            //instanciate bullet
            if(timer <= 0)
            {
                GameObject projectile = Instantiate(bullet, transform.position, Quaternion.Euler(new Vector3(dir.x, dir.y, 0)));
                projectile.GetComponent<Rigidbody2D>().velocity = new Vector2(dir.x * bulletSpeed, dir.y * bulletSpeed);
                timer = shootDeley;
               
            }
            else
            {
                timer -= Time.deltaTime;
            }
           
        }
        else
        {
            transform.position = Vector2.MoveTowards(transform.position, playerTransform.position, speed * Time.deltaTime);
        }
    }

    Vector2 runAway()
    {
        Vector2 dir;

        if (playerTransform.position.x > transform.position.x)
        {
            dir.x = -1;
        }
        else if (playerTransform.position.x < transform.position.x)
        {
            dir.x = 1;
        }
        else
        {
            dir.x = 0;
        }


        if (playerTransform.position.y > transform.position.y)
        {
            dir.y = -1;
        }
        else if (playerTransform.position.y < transform.position.y)
        {
            dir.y = 1;
        }
        else
        {
            dir.y = 0;
        }

        return dir;
    }
}
