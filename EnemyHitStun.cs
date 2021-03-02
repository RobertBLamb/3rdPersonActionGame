using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyHitStun : MonoBehaviour
{

    NavMeshAgent agent;
    bool inHitstun;
    float ogMovespeed;
    float stunSpeed = 0;
    float stunTimeReset;
    public float hitStunTimer;

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        ogMovespeed = agent.speed;
        stunTimeReset = hitStunTimer;
    }

    // Update is called once per frame
    void Update()
    {
        if(inHitstun && hitStunTimer>0)
        {
            hitStunTimer -= Time.deltaTime;
        }
        else if(inHitstun)
        {
            inHitstun = false;
            hitStunTimer = stunTimeReset;
            agent.speed = ogMovespeed;
        }
    }

    public void StartHitstun()
    {
        Debug.Log("activated");
        inHitstun = true;
        agent.speed = stunSpeed;
    }

}
