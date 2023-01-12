using AC;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMove3D : MonoBehaviour
{
    public float moveSpeed;
    private Vector2 move;

    // Update is called once per frame
    void Update()
    {
        /// IF: Time is not Rewinding
        if (gameObject.GetComponent<Player_Stats>().timeMaster.GetComponent<Time_Control>().timeState == Time_Control.timeWinding.none)
        {
            movePlayer();
        }
    }
    ///
    




    /// Update Move variable
    public void OnMove(InputAction.CallbackContext context)
    {
        move = context.ReadValue<Vector2>();
    }

    /// Move Player
    public void movePlayer()
    {
        // Locking Z movement
        float zLock = 1;
        if (gameObject.GetComponent<Player_Stats>().zMoveLocked)
        {
            zLock = 0;
        }

        Vector3 movement = new Vector3(move.x, 0f, move.y * zLock);

        

        if (movement != Vector3.zero)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(movement), 0.15f);

            transform.Translate(movement * moveSpeed * Time.deltaTime, Space.World);
        }
    }

}
