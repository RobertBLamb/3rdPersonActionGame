using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public int totalHealth;
    public int currentHealth;
    int outOfLife = 0;
    EnemyController ec;

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = totalHealth;
        ec = GetComponent<EnemyController>();
    }

    public void TakeDmg(int dmg)
    {
        currentHealth -= dmg;

        if(outOfLife>=currentHealth)
        {
            OutOfLife();
        }
    }

    public void OutOfLife()
    {
        //gameObject.SetActive(false);
        ec.enabled = false;
    }
}
