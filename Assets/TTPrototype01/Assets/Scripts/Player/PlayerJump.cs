using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerJump : MonoBehaviour
{

    //
    //Rigidbody2D rb;
    Rigidbody2D rb;

    [Header("JUMP Variables")]
    public float jumpForce = 10f;
    public float jumpTime = 1f;
    float jumpCountdown;
    public float gravityMultiplier = 1.5f;
    public float maxFallVelocity = 1f;


    Vector2 jumpVector;

    PlayerInput playerInput;

    bool jumping = false;
    bool falling = true;



    ///
    /// //
    /// 

    private void Awake()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
        jumpVector = new Vector2(0,jumpForce);

        playerInput = gameObject.GetComponent<PlayerInput>();

        playerInput.onActionTriggered += PlayerInput_onActionTriggered;

    }

    private void PlayerInput_onActionTriggered(InputAction.CallbackContext context)
    {
        // IF: We are on the ground
        //Debug.Log(context);
    }

    private void FixedUpdate()
    {

        if (jumpCountdown < Time.time)
        {
            jumping = false;
            falling = true;
        }

        if(jumping)
        {
            //Debug.Log("Jumping");

            // Move Upwards
            rb.AddForce(jumpVector, ForceMode2D.Impulse);

        }

        if (falling)
        {
            // Fall faster and faster
            rb.gravityScale = rb.gravityScale * gravityMultiplier;

            // Clamp speed to a Max
            rb.gravityScale = Mathf.Clamp(rb.gravityScale, 0, maxFallVelocity);

        }

        GetJumpingOrFalling();


    }

    // JUMPING
    public void Jump(InputAction.CallbackContext context)
    {
        // IF: We are on the ground
        //Debug.Log(context);

        if (gameObject.GetComponent<Player_Stats>().onGround)
        {
            // Start Jumping
            if (context.started)
            {
                jumpCountdown = Time.time + jumpTime;
                rb.gravityScale = 1;
                jumping = true;
                //falling = false;
            }
        }
        if (context.canceled)
        {
            jumping = false;
            //falling = true;
            
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
            if (!jumping)
            {
                falling = true;
            }
        }
    }
}
