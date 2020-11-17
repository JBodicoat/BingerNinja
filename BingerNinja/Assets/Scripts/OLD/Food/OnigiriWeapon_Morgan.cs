using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnigiriWeapon_Morgan : MonoBehaviour
{
    public Sprite explosion;

    Rigidbody2D RB2D;

    //projectile velocity using mouse click
    internal Vector2 velocity;
    public float velocityMultiplier = 2.5f;

    bool spriteChanged = false;

    //projectile lifetime
    float lifetime;
    public float detonateLifetime;
    public float deleteLifetime;

    // Start is called before the first frame update
    void Start()
    {
        RB2D = GetComponent<Rigidbody2D>();
        velocity *= velocityMultiplier;
    }

    // Update is called once per frame
    void Update()
    {
        lifetime += Time.deltaTime;
      
        if(lifetime < detonateLifetime)
        {
            RB2D.velocity = velocity;
            RB2D.AddTorque(2);
        }
        if(lifetime > detonateLifetime)
        {
            if (!spriteChanged)
            {
                GetComponent<SpriteRenderer>().sprite = explosion;
                transform.localScale *= 2;
                spriteChanged = true;

                Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, 1);
                foreach (Collider2D collider in hits)
                {
                    if (collider.tag == "Enemy")
                    {
                        collider.GetComponent<EnemyAi>().Hit(30);
                    }
                }
            }
            RB2D.velocity = Vector2.zero;
            RB2D.angularVelocity = 0;
        }
        if (lifetime > deleteLifetime)
        {
            Destroy(gameObject);
        }
    }
}

