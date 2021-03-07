using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDamageManager : MonoBehaviour
{
    public bool canHitstun;
    public bool damageable;
    public bool deactivateLight;

    public EnemyHitStun enemyHitstun;
    public EnemyHealth enemyHealth;
    public ModSkyBox modSkyBox;
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
            //TODO: move this, makes extra code
            if(enemyHealth.currentHealth-dmg<=0)
            {
                Debug.Log("gotem");
                anime.SetTrigger("Die");
            }
        }
        if(deactivateLight)
        {
            RenderSettings.fogDensity = 0.009f;
            modSkyBox.levelLight.color = modSkyBox.initalColor;
            modSkyBox.gameObject.SetActive(false);
        }


    }
}
