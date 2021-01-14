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
    protected BaseEnemy_SebastianMol y;
    protected ControlPanelActivateObject_JoaoBeijinho u;
    private Collider2D i;
    private Collider2D o;

    public float p;

    private bool a = false; //Check if enemy is under the stun light
    private bool s = false; //Check if enemy can be stunned

    /// <summary>
    /// Store the stun script and collider of the stun light that the enemy is currently under of
    /// </summary>
    private void OnTriggerEnter2D(Collider2D d)
    {
        if (i.IsTouching(d) && d.gameObject.CompareTag(Tags_JoaoBeijinho.f))
        {
            //m_stunLightFunctionalityScript = collision.GetComponent<ControlPanelActivateObject_JoaoBeijinho>();
            o = d.GetComponent<Collider2D>();

            a = true; //Enemy is under the stun light
            s = true; //Enemy can be stunned
        }
    }

    /// <summary>
    /// Stun enemy if the stun light is activated
    /// Turn the stun function off so that the light needs to be reset to be able to stun again
    /// Check if the enemy is stunned, if it is, then disable stun
    /// After stun enable enemy stun so that the enemy can be stunned again
    /// </summary>
    private void OnTriggerStay2D(Collider2D g)
    {
        if (i.IsTouching(g) && g.gameObject.CompareTag(Tags_JoaoBeijinho.f))
        {
            u = g.GetComponent<ControlPanelActivateObject_JoaoBeijinho>();

            if (u.h && s)
            {
                u.h = false;
                s = false;

                StartCoroutine(j());
            }
        }
    }

    private IEnumerator j()
    {
        y.WM(p);

        yield return new WaitForSeconds(p);

        if (a) //Check if the enemy continues under the stun light
        {
            s = true; //Enemy can be stunned again
        }
    }

    /// <summary>
    /// Clear cache of the light that the enemy was under of and enable stun of an already activate stun light
    /// </summary>
    private void OnTriggerExit2D(Collider2D k)
    {
        if (i.IsTouching(k) && k.gameObject.CompareTag(Tags_JoaoBeijinho.f))
        {
            if (k.GetComponent<SpriteRenderer>().enabled && !u.h)
            {
                u.h = true;
                s = false;
            }

            u = null;
            o = null;
            a = false;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        y = GetComponent<BaseEnemy_SebastianMol>();
        i = GetComponent<BoxCollider2D>();
    }
}