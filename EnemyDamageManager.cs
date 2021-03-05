using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDamageManager : MonoBehaviour
{
    public bool canHitstun;
    public bool damageable;

    public EnemyHitStun enemyHitstun;
    public EnemyHealth enemyHealth;
    public Animator anime;
    // Start is called before the first frame update
    void Awake()
    {
        if(gameObject.GetComponent<EnemyHitStun>())
        {
            enemyHitstun = gameObject.GetComponent<EnemyHitStun>();
        }
        if (gameObject.GetComponent<EnemyHealth>())
        {
            enemyHealth = gameObject.GetComponent<EnemyHealth>();
        }
    }
    public void EnemyHit(int dmg)
    {
        if(canHitstun)
        {
            enemyHitstun.StartHitstun();
            anime.SetTrigger("Take Damage");
        }

        if(damageable)
        {
            enemyHealth.TakeDmg(dmg);
        }

    }
}
