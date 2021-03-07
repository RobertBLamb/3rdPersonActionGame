using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClimbWall : MonoBehaviour
{
    public ThirdPersonMovement thirdPersonMovement;
    public Transform wallHighEnough;
    public Transform wallTooHigh;

    float castDist = .5f;
    public LayerMask isClimbable;
    public bool climbing;
    public float wallClimbAngle = 20;

    float rightAngle = 90;
    float climbSpeed = 10;

    float moveForwardtime=.1f;
    float moveForwardtimeReset;

    RaycastHit hit;
    // Start is called before the first frame update
    void Start()
    {
        moveForwardtimeReset = moveForwardtime;
    }

    // Update is called once per frame
    void Update()
    {
        //Add ground check?
        if(Raycast(wallHighEnough) && !Raycast(wallTooHigh) && !climbing)
        {
            
            //TODO: optimize code later, maybe make method
            if (Physics.Raycast(wallHighEnough.position, wallHighEnough.forward, out hit, castDist, isClimbable))
            {
                
                if (Mathf.Abs((hit.transform.eulerAngles.y % rightAngle)  - (transform.eulerAngles.y % rightAngle)) <= wallClimbAngle)
                {
                    if (Input.GetButtonDown("Interact"))
                    {
                        StartCoroutine(Climbing());
                    }
                    
                }
            }
        }

        Debug.DrawRay(wallHighEnough.position, wallHighEnough.forward, Color.green);
        Debug.DrawRay(wallTooHigh.position, wallTooHigh.forward, Color.red);
    }

    private void OnDrawGizmosSelected()
    {
        //basic layout
        Gizmos.color = Color.red;
        Gizmos.DrawLine(wallHighEnough.position, wallHighEnough.position + transform.forward);
    }

    bool Raycast(Transform cast)
    {
        RaycastHit ray;// = wallHighEnough.position, transform.forward, castDist, isClimbable;
        if (Physics.Raycast(cast.position, cast.forward, out ray, castDist, isClimbable))
        {
            return true;
        }
        return false;
    }

    public IEnumerator Climbing()
    {
        climbing = true;
        thirdPersonMovement.anime.SetTrigger("Climb");
        float i = .7f;
        while(i>0)
        {
            i -= Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }

        
        thirdPersonMovement.animationLocked = true;
        while(hit.collider.bounds.max.y>thirdPersonMovement.controller.bounds.min.y)
        {
            thirdPersonMovement.controller.Move(new Vector3(0, climbSpeed, 0) * Time.deltaTime);
            yield return new WaitForFixedUpdate();
        }//vertical climb
        while(moveForwardtime>0)
        {
            moveForwardtime -= Time.deltaTime;
            yield return new WaitForFixedUpdate();
            Vector3 moveDir = Quaternion.Euler(0f, transform.eulerAngles.y, 0f) * Vector3.forward;
            thirdPersonMovement.controller.Move(moveDir.normalized * climbSpeed * Time.deltaTime);
        }//move forward onto platform
        moveForwardtime = moveForwardtimeReset;
        thirdPersonMovement.animationLocked = false;
        climbing = false;

    }

}
