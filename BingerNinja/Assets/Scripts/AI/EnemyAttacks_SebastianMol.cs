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
    /// <param name="a"></param>
    /// <param name="q"></param>
    /// <param name="w"></param>
    /// <param name="e"></param>
    /// <param name="r"></param>
    /// <param name="t"></param>
    /// <param name="m_petTigerDeley"></param>
    /// <param name="y"></param>
    /// <param name="u"></param>
    /// <param name="p"></param>
    /// <returns>weather the attack has been done</returns>
    public static bool MelleAttack(ref float a, bool q, int w,
        Action e, Action r, Action t,
        WU y, float u, Enemy_Animation_LouieWilliamson i, bool p = false)

    {
        if (a <= 0)
        {
            if (q)
            {
                int m = UnityEngine.Random.Range(0, w);
                if(m == w-1)
                {
                    r();
                }
                else
                {
                    e();
                }
            }
            else
            {
                e();
            }

            if (y == WU.WF)
            {
                t();
            }

            a = u;
            return true;
        }
        else
        {
            a -= Time.deltaTime;
            i.AttackAnimation();
            return false;
        }
    }

    /// <summary>
    /// holds logic for ranged attacks
    /// </summary>
    /// <param name="d"></param>
    /// <param name="f"></param>
    /// <param name="g"></param>
    /// <param name="h"></param>
    /// <param name="j"></param>
    /// <param name="k"></param>
    /// <returns>weather the attack has been done</returns>
    public static bool RangedAttack(Enemy_Animation_LouieWilliamson s, Transform d, Transform f, GameObject g, 
        ref float h, GameObject j, float k,float l = 0, float z = 0)
    {
        if (d != null)
        {
            Vector3 x = Vector3.Normalize(d.position - f.position);
            float c = Mathf.Atan2(x.y, x.x) * Mathf.Rad2Deg;
            g.transform.eulerAngles = new Vector3(0, 0, c);

            if (h <= 0)
            {
                GameObject v = Instantiate(j, f.position, Quaternion.Euler(new Vector3(x.x, x.y, 0)));
                v.transform.localScale += new Vector3(l, z, 0);
                v.GetComponent<BulletMovment_SebastianMol>().m_direction = (d.position - f.position).normalized;
                ColorChanger_Jann.Instance.UpdateColor(v.GetComponent<SpriteRenderer>());
                h = k;
                s.AttackAnimation();
                return true;
            }
            else
            {
                h -= Time.deltaTime;
                return false;
            }
        }
        return false;
    }

    /// <summary>
    /// hold logic for a charge attack.
    /// </summary>
    /// <param name="q"></param>
    /// <param name="w"></param>
    /// <param name="e"></param>
    /// <param name="r"></param>
    /// <param name="t"></param>
    /// <param name="y"></param>
    /// <returns>weather the attack has been done</returns>
    public static bool ChargeAttack(Transform q, ref float w, 
        GameObject e, float r, GameObject t, float y)
    {                  
        if (w <= 0)
        {
            //shoot at enemy
            e.SetActive(true);
            Rigidbody2D rijy = t.GetComponent<Rigidbody2D>();
            rijy.bodyType = RigidbodyType2D.Dynamic;
            rijy.gravityScale = 0;
            rijy.freezeRotation = true;
            rijy.AddForce((q.position - t.transform.position).normalized * y );
            EnemyDamager_SebastianMol u = e.GetComponent<EnemyDamager_SebastianMol>();
            u.m_damage = u.m_baseDamage;
            w = r;
            return true;

        }
        else
        {
            w -= Time.deltaTime;
            return false;
        }
    }
}
