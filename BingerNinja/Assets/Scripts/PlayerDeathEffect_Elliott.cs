// Elliott

// Elliott 21/11/2020 - made the function death Blink(turns spite on and off) and spriteFlash 
using System.Collections;
using UnityEngine;

public class PlayerDeathEffect_Elliott : MonoBehaviour
{
    public SpriteRenderer m_player;
    private PlayerController_JamieG m_PauseInput;

    public IEnumerator DeathBlink()
    {
        for (int i = 0; i < 5; i++)
        {
            m_PauseInput.OnDisable();
            //gameObject.GetComponent<SpriteRenderer>().enabled = false;
            m_player.enabled = false;
            yield return new WaitForSeconds(.1f);
           // gameObject.GetComponent<SpriteRenderer>().enabled = true;
           m_player.enabled = true;
            yield return new WaitForSeconds(.1f);
        }
        m_PauseInput.OnEnable();
    }

    public void SpriteFlash()
    {
        StartCoroutine(DeathBlink());
    }
    // Start is called before the first frame update
    void Start()
    {
       m_player = GetComponentInChildren<SpriteRenderer>();
       m_PauseInput = GetComponent<PlayerController_JamieG>();
    }
}
