using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player3D_Jump : MonoBehaviour
{
    Rigidbody rb;

    public float jumpForce;
    bool jumping = false;
    bool falling = true;

    float jumpCountdown = 0;
    [SerializeField] float jumpTime;

    PlayerInput playerInput;

    //
    private void Awake()
    {
        rb = gameObject.GetComponent<Rigidbody>();
        playerInput = gameObject.GetComponent<PlayerInput>();

    }

    private void FixedUpdate()
    {

        if (jumpCountdown < Time.time)
        {
            jumping = false;
            falling = true;
        }


        if (falling)
        {
            rb.AddForce(Vector3.down * jumpForce, ForceMode.Force);

        }

        GetJumpingOrFalling();


    }

    // JUMP
    public void Jump(InputAction.CallbackContext context)
    {
        /// IF: Time is not Rewinding
        if (gameObject.GetComponent<Player_Stats>().timeMaster.GetComponent<Time_Control_3>().timeState == Time_Control_3.timeWinding.none)
        {

            if (gameObject.GetComponent<Player_Stats>().onGround)
            {
                // Start Jumping
                if (context.started)
                {
                    jumpCountdown = Time.time + jumpTime;
                    rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);

                    gameObject.GetComponent<Player_Stats>().onGround = false;

                    //jumping = true;
                    //falling = false;
                    Debug.Log("Jumped");
                }

            }
        }
        
    }

    // FALLING
    void GetJumpingOrFalling()
    {

        // IF: We are on the ground
        // Don't fall
        if (gameObject.GetComponent<Player_Stats>().onGround)
        {
            falling = false;
        }
        // IF: Not on the ground
        else
        {
            // AND: We are not jumping
            // Fall

            //if (!jumping)
            {
                falling = true;
            }
        }
    }

    /*
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    */
}
