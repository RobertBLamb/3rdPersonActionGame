using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DodgeSpecial : MonoBehaviour
{
    public ThirdPersonMovement thirdPersonMovement;

    public float dodgeDuration;
    float dodgeDurationReset;

    public float dodgeSpeed;

    // Start is called before the first frame update
    void Start()
    {
        dodgeDurationReset = dodgeDuration;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetButtonDown("Dodge") && !thirdPersonMovement.usingSpecial)
        {
            StartCoroutine(Dodge());
        }
    }

    //currently there is a bug with returning movespeed back to its original value
    //player should only dash in their last direction atm
    public IEnumerator Dodge()
    {
        thirdPersonMovement.usingSpecial = true;
        thirdPersonMovement.speed = dodgeSpeed;

        thirdPersonMovement.anime.SetTrigger("Dodge");

        while(dodgeDuration>0)
        {
            dodgeDuration -= Time.deltaTime;
            thirdPersonMovement.MovePlayer();
            yield return new WaitForFixedUpdate();

        }
        dodgeDuration = dodgeDurationReset;


        thirdPersonMovement.ResetMovement();
    }

}
