//sebastian mol 14/11/2020 script created
//sebastian mol 22/11/2020 charge attack created
//Jann          08/12/2020 Added projectile colour change
//Louie         11/12/2020 Added enemy attack animation function call

using System;
using UnityEngine;

/// <summary>
/// used to decouple attack logic from enemie classes
/// </summary>
public class EnemyAttacks_SebastianMol : MonoBehaviour
{
    /// <summary>
    /// holds logic for melee attacks
    /// </summary>
    /// <param name="w"></param>
    /// <param name="e"></param>
    /// <param name="r"></param>
    /// <param name="t"></param>
    /// <param name="y"></param>
    /// <param name="u"></param>
    /// <param name="m_petTigerDeley"></param>
    /// <param name="i"></param>
    /// <param name="o"></param>
    /// <param name="a"></param>
    /// <returns>weather the attack has been done</returns>
    public static bool q(ref float w, bool e, int r,
        Action t, Action y, Action u,

        y i, float o, Enemy_Animation_LouieWilliamson p, bool a = false)

    {
        if (w <= 0)
        {
            if (e)
            {
                int s = UnityEngine.Random.Range(0, r);
                if(s == r-1)
                {
                    y();
                }
                else
                {
                    t();
                }
            }
            else
            {
                t();
            }

            if (i == global::y.d)
            {
                u();
            }

            w = o;
            return true;
        }
        else
        {
            w -= Time.deltaTime;
            p.t();
            return false;
        }
    }

    /// <summary>
    /// holds logic for ranged attacks
    /// </summary>
    /// <param name="g"></param>
    /// <param name="h"></param>
    /// <param name="j"></param>
    /// <param name="k"></param>
    /// <param name="l"></param>
    /// <param name="z"></param>
    /// <returns>weather the attack has been done</returns>
    public static bool d(Enemy_Animation_LouieWilliamson f, Transform g, Transform h, GameObject j, 
        ref float k, GameObject l, float z,float x = 0, float c = 0)
    {
        if (g != null)
        {
            Vector3 v = Vector3.Normalize(g.position - h.position);
            float b = Mathf.Atan2(v.y, v.x) * Mathf.Rad2Deg;
            j.transform.eulerAngles = new Vector3(0, 0, b);

            if (k <= 0)
            {
                GameObject n = Instantiate(l, h.position, Quaternion.Euler(new Vector3(v.x, v.y, 0)));
                n.transform.localScale += new Vector3(x, c, 0);
                n.GetComponent<BulletMovment_SebastianMol>().w = (g.position - h.position).normalized;
                ColorChanger_Jann.Instance.g(n.GetComponent<SpriteRenderer>());
                k = z;
                f.t();
                return true;
            }
            else
            {
                k -= Time.deltaTime;
                return false;
            }
        }
        return false;
    }

    /// <summary>
    /// hold logic for a charge attack.
    /// </summary>
    /// <param name="Q"></param>
    /// <param name="W"></param>
    /// <param name="E"></param>
    /// <param name="R"></param>
    /// <param name="T"></param>
    /// <param name="Y"></param>
    /// <returns>weather the attack has been done</returns>
    public static bool m(Transform Q, ref float W, 
        GameObject E, float R, GameObject T, float Y)
    {                  
        if (W <= 0)
        {
            //shoot at enemy
            E.SetActive(true);
            Rigidbody2D U = T.GetComponent<Rigidbody2D>();
            U.bodyType = RigidbodyType2D.Dynamic;
            U.gravityScale = 0;
            U.freezeRotation = true;
            U.AddForce((Q.position - T.transform.position).normalized * Y );
            EnemyDamager_SebastianMol I = E.GetComponent<EnemyDamager_SebastianMol>();
            I.O = I.P;
            W = R;
            return true;

        }
        else
        {
            W -= Time.deltaTime;
            return false;
        }
    }
}
