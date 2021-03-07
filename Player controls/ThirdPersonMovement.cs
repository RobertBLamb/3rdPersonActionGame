using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ThirdPersonMovement : MonoBehaviour
{
    public AimDownSights aimDownSights;
    public float walkSpeed = 6f;
    public float runSpeed = 12f;

    [HideInInspector]
    public bool usingSpecial;
    //[HideInInspector]
    public float speed;
    [HideInInspector]
    public bool animationLocked;
    public Animator anime;

    #region Done Modifying

    Vector3 velocity;
    float gravity = 9.81f;

    [HideInInspector]
    public bool running;
    [HideInInspector]
    public bool canRun = true;

    //camera variables
    float turnSmoothTime = 0.1f;
    float turnSmoothVelocity;
    public Vector3 direction;


    public CharacterController controller;
    public Transform cam;
    # endregion

    //ToDo: for if I decide to implement jump controls
    //https://youtu.be/fyV77lN1Yl0?t=2032

    // Update is called once per frame
    void Start()
    {
        speed = walkSpeed;
    }

    void Update()
    {
        if(!animationLocked)
        {
            float horizontal = Input.GetAxisRaw("Horizontal");
            float vertical = Input.GetAxisRaw("Vertical");
            ApplyGravity();
            if (!usingSpecial)
            {
                direction = new Vector3(horizontal, 0f, vertical).normalized;
                if(canRun)
                {
                    if (Input.GetButtonDown("Running") && !running)
                    {
                        StartRunning();
                    }
                    else if (Input.GetButtonDown("Running") && running)
                    {
                        StopRunning();
                    }
                    else if (horizontal == 0 && vertical == 0 && !Input.GetButton("Running") && running)
                    {
                        StopRunning();
                    }
                }

                //Currently stops the player in place when using special
                if (direction.magnitude >= 0.1f)
                {
                    MovePlayer();
                    anime.SetBool("Walking", true);
                }
                else
                {
                    anime.SetBool("Walking", false);
                }
            }

        }

    }

    void StartRunning()
    {
        speed = runSpeed;
        running = true;
        anime.SetBool("Running", true);
    }

    public void StopRunning()
    {
        speed = walkSpeed;
        running = false;
        anime.SetBool("Running", false);
    }

    void ApplyGravity()
    {
        if(controller.isGrounded)
        {
            velocity.y = 0;
        }
        else
        {
            velocity.y -= gravity * Time.deltaTime;
        }
        controller.Move(velocity * Time.deltaTime);
    }

    public void MovePlayer()
    {
        if(!usingSpecial)
        {
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;

            if (!aimDownSights.aiming)
            {
                //move player towards where the camera is facing
                float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
                transform.rotation = Quaternion.Euler(0f, angle, 0f);
            }


            Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
            controller.Move(moveDir.normalized * speed * Time.deltaTime);
        }
        else
        {
            if (direction.magnitude >= 0.1f)
            {
                Vector3 moveDir = Quaternion.Euler(0f, transform.eulerAngles.y, 0f) * Vector3.forward;
                controller.Move(moveDir.normalized * speed * Time.deltaTime);
            }
            else
            {
                Vector3 moveDir = Quaternion.Euler(0f, transform.eulerAngles.y-180, 0f) * Vector3.forward;
                controller.Move(moveDir.normalized * speed * Time.deltaTime);
            }
                
        }
        
    }

    public void ResetMovement()
    {
        anime.SetBool("Running", false);
        if (aimDownSights.aiming)
        {
            speed = aimDownSights.aimMovespeed;
            //canRun = false;
        }
        else
        {
            speed = walkSpeed;
            canRun = true;
        }
        
        running = false;
        usingSpecial = false;
        animationLocked = false;
        //canRun = true;
    }


    public void FaceCrossHair()
    {
        float targetAngle = cam.eulerAngles.y;

        if (Mathf.Abs(targetAngle - transform.eulerAngles.y)>1f)
        {
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);
        }

    }
}


