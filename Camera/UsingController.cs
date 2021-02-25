using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UsingController : MonoBehaviour
{
    public bool usingController;
    public GameObject freeCam;

    // Start is called before the first frame update
    void Start()
    {
        if(usingController)
        {
            freeCam.GetComponent<Cinemachine.CinemachineFreeLook>().m_YAxis.m_MaxSpeed *= 3;
            freeCam.GetComponent<Cinemachine.CinemachineFreeLook>().m_XAxis.m_MaxSpeed *= 3;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
