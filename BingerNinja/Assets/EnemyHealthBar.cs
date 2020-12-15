using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealthBar : MonoBehaviour
{
    // Start is called before the first frame update
    private float maxHealth;
    private float currentHealth;
    private GameObject Enemy;

    private Slider slider;
    private Vector2 scale;

    public void UpdateHealth(float newHealth)
    {
        currentHealth = newHealth;
    }
    void Start()
    {
        //Enemy = transform.parent.parent.gameObject;
        Enemy = GameObject.FindGameObjectWithTag("Enemy");
        maxHealth = 0;
        currentHealth = Enemy.GetComponent<BaseEnemy_SebastianMol>().m_health;
        slider = GetComponent<Slider>();
        scale = transform.lossyScale;
    }

    // Update is called once per frame
    void Update()
    {
        //transform.position = Camera.main.ScreenToWorldPoint(new Vector3(Enemy.transform.position.x, Enemy.transform.position.y, 1));
        transform.position = Enemy.transform.position;
        transform.localScale = scale;
         //new Vector3(Camera.main.ScreenToWorldPoint(Enemy.transform.position).x, Camera.main.ScreenToWorldPoint(Enemy.transform.position).y, 1);
        maxHealth = Enemy.GetComponent<BaseEnemy_SebastianMol>().m_maxHealth;

        slider.value = currentHealth / maxHealth;

        print("max: " + maxHealth + " - - current: " + currentHealth + "GO:" + Enemy.name);
    }
}
