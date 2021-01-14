//Joao Beijinho 13/11/2020 - Created script
//Joao Beijinho 14/11/2020 - Update collision to use Tags() stunLightTag
//Joao Beijinho 17/11/2020 - Call StunEnemyWithDeleyFunc() from baseEnemy, created triggers and Stun()
//Joao Beijinho 18/11/2020 - Check for Collision only on enemy BoxCollider and update to use StunEnemyWithLightsDeleyFunc

using System.Collections;
using UnityEngine;

/// <summary>
/// This class calls StunEnemyWithDeleyFunc() to stun the player when 
/// </summary>
internal class StunLight_JoaoBeijinho : MonoBehaviour
{
    protected BaseEnemy_SebastianMol m_baseEnemyScript;
    protected ControlPanelActivateObject_JoaoBeijinho m_stunLightFunctionalityScript;
     Collider2D q;
     Collider2D w;

    public float m_stunLightDuration;

     bool e = false; //Check if enemy is under the stun light
     bool r = false; //Check if enemy can be stunned

    /// <summary>
    /// Store the stun script and collider of the stun light that the enemy is currently under of
    /// </summary>
     void OnTriggerEnter2D(Collider2D t)
    {
        if (q.IsTouching(t) && t.gameObject.CompareTag(Tags_JoaoBeijinho.m_stunLightTag))
        {
            //m_stunLightFunctionalityScript = collision.GetComponent<ControlPanelActivateObject_JoaoBeijinho>();
            w = t.GetComponent<Collider2D>();

            e = true; //Enemy is under the stun light
            r = true; //Enemy can be stunned
        }
    }

    /// <summary>
    /// Stun enemy if the stun light is activated
    /// Turn the stun function off so that the light needs to be reset to be able to stun again
    /// Check if the enemy is stunned, if it is, then disable stun
    /// After stun enable enemy stun so that the enemy can be stunned again
    /// </summary>
     void OnTriggerStay2D(Collider2D y)
    {
        if (q.IsTouching(y) && y.gameObject.CompareTag(Tags_JoaoBeijinho.m_stunLightTag))
        {
            m_stunLightFunctionalityScript = y.GetComponent<ControlPanelActivateObject_JoaoBeijinho>();

            if (m_stunLightFunctionalityScript.m_stunLight == true && r == true)
            {
                m_stunLightFunctionalityScript.m_stunLight = false;
                r = false;

                StartCoroutine(Stun());
            }
        }
    }

     IEnumerator Stun()
    {
        m_baseEnemyScript.RA(m_stunLightDuration);

        yield return new WaitForSeconds(m_stunLightDuration);

        if (e == true) //Check if the enemy continues under the stun light
        {
            r = true; //Enemy can be stunned again
        }
    }

    /// <summary>
    /// Clear cache of the light that the enemy was under of and enable stun of an already activate stun light
    /// </summary>
     void OnTriggerExit2D(Collider2D o)
    {
        if (q.IsTouching(o) && o.gameObject.CompareTag(Tags_JoaoBeijinho.m_stunLightTag))
        {
            if (o.GetComponent<SpriteRenderer>().enabled == true && m_stunLightFunctionalityScript.m_stunLight == false)
            {
                m_stunLightFunctionalityScript.m_stunLight = true;
                r = false;
            }

            m_stunLightFunctionalityScript = null;
            w = null;
            e = false;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        m_baseEnemyScript = GetComponent<BaseEnemy_SebastianMol>();
        q = GetComponent<BoxCollider2D>();
    }
}