//sebastian mol
//sebastian mol 30/10/20 removed patrol function
//sebastian mol 02/11/20 removed player behaviour switch replaced it with abstract functions
// louie        11/12/2020 Attack animation

using UnityEngine;

/// <summary>
/// class that ranged enemys use
/// </summary>
class RangedEnemy_SebastianMol : BaseEnemy_SebastianMol
{

    public GameObject m_aimer;
    public GameObject m_projectile;
    public float m_shootDeley;
    public float m_projectileSpeed;


    /// <summary>
    /// ovveride class that holds logic for what the enemy shoudl do when in the attack state
    /// </summary>
    internal override void ET()
    {
        EnemyAttacks_SebastianMol.RangedAttack(GetComponent<Enemy_Animation_LouieWilliamson>(), QY, transform, m_aimer, ref QO, m_projectile, m_shootDeley);
    }
}
