using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealthBar : MonoBehaviour
{
    private Vector2 originalScale;
    private Slider slider;
    private GameObject Enemy;
    public Vector3 Offset;

    private void Start()
    {
        originalScale = transform.localScale;
        slider = gameObject.GetComponent<Slider>();
        Enemy = transform.parent.parent.gameObject;
        transform.parent.parent = null;
    }

    // Update is called once per frame
    void Update()
    {
        transform.localScale = originalScale;
        transform.parent.position = Enemy.transform.position + Offset;
        float health = Enemy.GetComponent<BaseEnemy_SebastianMol>().m_health;
        float maxHealth = Enemy.GetComponent<BaseEnemy_SebastianMol>().m_maxHealth;
        slider.value = health / maxHealth;
        if (health <= 0) Destroy(gameObject);
    }
}
