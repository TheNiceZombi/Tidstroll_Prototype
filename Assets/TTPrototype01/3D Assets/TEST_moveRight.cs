using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TEST_moveRight : TimeControlled
{
    Rigidbody rb;

    float moveSpeed     = 2;
    float jumpVelocity  = 20;

    bool  wantToJump = false;


    // Start is called before the first frame update
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody>();

       
    }

    public override void TimeUpdate()
    {
        if(wantToJump)
        {
            ForceJump();

        }

    }

    void ForceJump()
    {
        // JUMP
        Vector3 forcedJump = new Vector3(1f, 1f, 0f);
        rb.AddForce(forcedJump * jumpVelocity, ForceMode.Impulse);

        wantToJump = false;
    }

    private void OnCollisionEnter(Collision collision)
    {
        // IF: Time is not rewinding
        if (timeUpdating)
        {
            Debug.LogWarning("Hoppitoss: Collided");

            wantToJump = true;

            //velocity.y = 0;
        }
    }
}
