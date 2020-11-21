// Elliott

// Elliott 21/11/2020 - made the function death Blink(turns spite on and off) and spriteFlash 
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerDeathEffect_Elliott : MonoBehaviour
{
    public SpriteRenderer m_player;

    public IEnumerator DeathBlink()
    {
        for (int i = 0; i < 5; i++)
        {
            m_player.enabled = false;
            yield return new WaitForSeconds(.1f);
            m_player.enabled = true;
            yield return new WaitForSeconds(.1f);
        }
    }

    public void SpriteFlash()
    {
        StartCoroutine(DeathBlink());
    }
    // Start is called before the first frame update
    void Start()
    {
        m_player = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
