using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnTriggerActivate : MonoBehaviour
{
    [HideInInspector]
    public bool areaActivated = false;

    public GameObject[] objectsToActivate;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    private void OnTriggerEnter(Collider other)
    {
        if(other.tag=="Player" && !areaActivated)
        {
            Debug.Log("walking tall");
            areaActivated = true;
            StartCoroutine(ActivateObjects()); 
        }
    }

    IEnumerator ActivateObjects()
    {
        for(int i = 0; i<objectsToActivate.Length; i++)
        {
            objectsToActivate[i].SetActive(true);
            yield return new WaitForEndOfFrame();
        }
        yield return new WaitForEndOfFrame();
    }
}
