using AC;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Stats : MonoBehaviour
{


    [Header("ACTIONS")]
    public bool isGrabbing = false;

    [Header("States")]
    public bool onGround = false;
    public bool nextToWall = false;
    public bool canJump = true;
    public bool nearInteract = false;
    public bool zMoveLocked = false;


    [Header("Collision")]
    public float gravityAmount = 5f;
    public float groundLength = 0.6f;
    public LayerMask GroundLayer;
    Vector3 collisionOffset;
    Rigidbody rb;

    public GameObject timeMaster;

    public static float gravity = 100f;

    private void Start()
    {
        //KickStarter.stateHandler.SetMovementSystem(false);
        gravity = gravityAmount;
        rb = gameObject.GetComponent<Rigidbody>();

        timeMaster = GameObject.Find("Time_Master");
        if(timeMaster == null)
        {
            Debug.LogError("Player_Stats:: Time_Master was not found in scene.");
        }
    }

    /// // ADD Extra Gravity
    private void FixedUpdate()
    {
        rb.AddForce(Vector3.down * gravity, ForceMode.Force);
    }

    /// // TIME STOP
    /*
    public override void TimeUpdate()
    {
        gravity = 0f;
    }
    */

    /// DEBUG: Show how far down we are checking for the ground
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position + collisionOffset, transform.position + collisionOffset + Vector3.down * groundLength);
        Gizmos.DrawLine(transform.position - collisionOffset, transform.position - collisionOffset + Vector3.down * groundLength);

    }
}
