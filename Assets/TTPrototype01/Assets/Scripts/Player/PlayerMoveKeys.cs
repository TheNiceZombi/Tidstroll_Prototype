using AC;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMoveKeys : MonoBehaviour
{
    [Header("Components")]
    public  Rigidbody2D rb;
    public  LayerMask GroundLayer;

    UnityEngine.InputSystem.PlayerInput playerInput;

    [Header("Horizontal Movement")]
    public  float moveSpeed = 10f;

            Vector2 direction;
            bool isMoving = false;
            bool facingRight = true;

    [Header("Physics")]
    public  float linearDrag = 4f;
    public  float maxSpeed = 7f;


    private void Start()
    {
        AC.KickStarter.stateHandler.SetMovementSystem(false);
       // AC.KickStarter.stateHandler.

        playerInput = gameObject.GetComponent<UnityEngine.InputSystem.PlayerInput>();

    }
    private void FixedUpdate()
    {
        movePlayer();

    }

    /// INPUT: Get direction / When to stop
    /// // ------------------------------------------------
    public void testMoveAxis(InputAction.CallbackContext context)
    {
        Debug.Log(context);
        direction = context.ReadValue<Vector2>();

        isMoving = true;

        rb.drag = 0;

        //Stop moving
        if (context.canceled)
        {
            rb.drag = linearDrag;
            direction = Vector2.zero;
            isMoving = false;
        }

    }

    private void movePlayer()
    {
        if (isMoving)
        {
            // Move
            rb.AddForce(Vector2.right * direction * moveSpeed, ForceMode2D.Impulse);

            // Cap speed
            if (Mathf.Abs(rb.velocity.x) > maxSpeed)
            {
                rb.velocity = new Vector2(Mathf.Sign(rb.velocity.x) * maxSpeed, rb.velocity.y);
            }
        }
    }

    void Flip()
    {
        // Convert facing to the opposite of itself
        facingRight = !facingRight;
        transform.rotation = Quaternion.Euler(0, facingRight ? 0 : 180, 0);
    }

}
