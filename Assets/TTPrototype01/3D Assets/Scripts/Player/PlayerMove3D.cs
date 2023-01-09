using AC;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMove3D : MonoBehaviour
{
    public float moveSpeed;
    private Vector2 move;

    // Start is called before the first frame update
    void Start()
    {
        //AC.KickStarter.stateHandler.SetMovementSystem(false);
        // AC.KickStarter.stateHandler.

        //playerInput = gameObject.GetComponent<UnityEngine.InputSystem.PlayerInput>();
    }

    // Update is called once per frame
    void Update()
    {
        movePlayer();
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        move = context.ReadValue<Vector2>();
    }

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
            //Debug.Log("Movement vector: " + movement);

            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(movement), 0.15f);

            transform.Translate(movement * moveSpeed * Time.deltaTime, Space.World);
        }
    }

}
