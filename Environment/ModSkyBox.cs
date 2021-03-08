using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModSkyBox : MonoBehaviour
{
    public Light levelLight;

    public Color initalColor;
    public Transform player;
    //R=.4274, G=.0117, B = .4823
    public Color corruptionColor = new Color(0.4274f, 0.0117f, 0.4823f, 1f);

    public float farSkyRadius;
    public float closeSkyRadius;

    bool changeSky;

    void Start()
    {
        initalColor = levelLight.color;
    }

    // Update is called once per frame
    void Update()
    {
        float dist = Vector3.Distance(player.position, transform.position);
        if(dist > farSkyRadius && changeSky)
        {
            changeSky = false;
            levelLight.color = initalColor;
        }
        else if(dist<closeSkyRadius && changeSky)
        {
            changeSky = false;
            levelLight.color = corruptionColor;
        }
        else if(dist > closeSkyRadius && dist < farSkyRadius)
        {
            changeSky = true;
            float distFract = (farSkyRadius - dist)/(farSkyRadius - closeSkyRadius);
            levelLight.color = Color.Lerp(initalColor, corruptionColor, distFract);
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(transform.position, farSkyRadius);
    }
}
