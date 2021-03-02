using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public int totalHealth;
    public int currentHealth;
    int outOfLife = 0;

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = totalHealth;
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
        Debug.Log("Ya got me");
        gameObject.SetActive(false);
    }
}
