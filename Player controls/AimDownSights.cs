using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AimDownSights : MonoBehaviour
{
    public ThirdPersonMovement thirdPersonMovement;
    public GameObject freeCam;
    public Reticle reticle;

    float normalFov;
    float curFov;
    float fovMaxDif;
    Coroutine a;
    public float zoomedFov;

    public bool aiming;
    public bool shotUsed;
    float triggerCutOff = .09f;
    public float aimMovespeed = 5f;
    float zoomTime = .5f;

    // Start is called before the first frame update
    void Start()
    {
        normalFov = freeCam.GetComponent<Cinemachine.CinemachineFreeLook>().m_Lens.FieldOfView;
        curFov = normalFov;
        fovMaxDif = normalFov - zoomedFov;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetAxisRaw("Aim")>triggerCutOff && !aiming)
        {
            aiming = true;
            thirdPersonMovement.speed = aimMovespeed;
            thirdPersonMovement.canRun = false;
            if(a!=null)
            {
                StopCoroutine(a);
            }
            a = StartCoroutine(Zoom(zoomedFov));
        }//on LT press aim, disable running
        else if(Input.GetAxisRaw("Aim") < triggerCutOff && aiming)
        {
            aiming = false;
            thirdPersonMovement.ResetMovement();
            if (a != null)
            {
                StopCoroutine(a);
            }
            shotUsed = false;
            a = StartCoroutine(Zoom(normalFov));
        }//letting go of LT, reset cam zoom and MS

        if(aiming && Input.GetAxisRaw("Shoot")>triggerCutOff && !shotUsed)
        {
            Debug.Log("bang bang bang");
            shotUsed = true;
            //add function for trying to shoot
        }
        else if(aiming && Input.GetAxisRaw("Shoot") < triggerCutOff && shotUsed)
        {
            shotUsed = false;
        }
    }

    void FixedUpdate()
    {
        if (aiming)
        {
            thirdPersonMovement.FaceCrossHair();
        }//TODO: probably doesnt need to be called every frame, link to R stick input
    }

    public IEnumerator Zoom(float goalFov)
    {
        float t =0; 
        if(aiming)
        {
            t = (normalFov - curFov)/fovMaxDif;
            reticle.ToggleReticle(true);
        }
        else
        {
            t = 1 - ((normalFov - curFov) / fovMaxDif);
            reticle.ToggleReticle(false);
        }//calculate how long the zoom should take

        while(t<1)
        {
            //this isnt going to work atm

            curFov = freeCam.GetComponent<Cinemachine.CinemachineFreeLook>().m_Lens.FieldOfView = Mathf.Lerp(curFov, goalFov, t);
            t += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }//zoom in or out

        a = null; //let script know a coroutine doesnt have to be cancelled later
    }
}
