using AC;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerGrabObject : MonoBehaviour
{
    // Carrier is the character carring the grabbed object

    // Grabbed object is the object being grabbed

    [Header("Carrier")]
    public GameObject carrierHand;
           Transform grabPoint;
    public LayerMask interactLayer;
    public Vector3 handOffset;
    public int grabReach = 3; 
    //public InputAction playerAction;


    private Vector3 grabDragDifference;

    public bool isGrabbing;
    GameObject grabbedObject;
    string grabType;

    float carrierSpeed;
    float carrierMaxSpeed;
    float grabbedObject_gravityScale;

    // Start is called before the first frame update
    void Start()
    {

        isGrabbing = gameObject.GetComponent<Player_Stats>().isGrabbing;
        carrierHand = gameObject;
        grabPoint = carrierHand.transform;
    }



    // Update is called once per frame
    void Update()
    {
        

        // Grab Input
        if (Input.GetKeyDown(KeyCode.E))
        {
            Debug.Log("Attempt to Grab");
            GrabDropObject();
        }

        

    }

    // Update is called once per physics frame 
    private void FixedUpdate()
    {
        if (grabbedObject != null)
        {
            // Move grabbed Object
            switch (grabType)
            {
                case ("Grab Light"):
                    // Hold Object
                    grabbedObject.GetComponent<Rigidbody2D>().MovePosition(carrierHand.transform.position + handOffset);//transform.position = carrierHand.transform.position + handOffset;

                    break;
                case ("Grab Heavy"):

                    // Get Push Vector
                    Vector3 pushVector = gameObject.transform.position - grabDragDifference;

                    //Debug.Log("Push Vector :: " + (grabbedObject.transform.position + pushVector));

                    // Push Object
                    grabbedObject.GetComponent<Rigidbody2D>().MovePosition( (grabbedObject.transform.position + pushVector) ); //* 1.2f ); 

                    // Update vector for next "Get Push Vector"
                    grabDragDifference = gameObject.transform.position;

                    break;
            }
        }
    }



    public bool GrabDropObject()
    {
        // If we have something in our hands
        // Let go
        if (gameObject.GetComponent<Player_Stats>().isGrabbing)
        {
            gameObject.GetComponent<Player_Stats>().isGrabbing = false;

            // Set altered variables to their origin

            switch (grabType)
            {
                case ("Grab Light"):
                    grabbedObject.GetComponent<Rigidbody2D>().gravityScale = grabbedObject_gravityScale;


                    // Reset "grabbed object" id
                    grabbedObject = null;
                    grabbedObject_gravityScale = 0f;
                    break;

                case ("Grab Heavy"):

                    // reset Carrier Speed
                    gameObject.GetComponent<PlayerMoveKeys>().moveSpeed = carrierSpeed;
                    gameObject.GetComponent<PlayerMoveKeys>().maxSpeed = carrierMaxSpeed;

                    // reset All variables
                    grabbedObject = null;
                    carrierSpeed = 0f;
                    carrierMaxSpeed = 0f;
                    break;
            }
            return (isGrabbing);

        }
        // If we are empty handed
        // Pick up
        else
        {
            // // // Checking for objects to grab

            grabbedObject = DetectGrabObject();
            if (grabbedObject != null)
            {
                // Tell Stats that we are grabbing

                grabDragDifference = gameObject.transform.position;
                gameObject.GetComponent<Player_Stats>().isGrabbing = true;
                grabType = grabbedObject.GetComponent<InteractableClass>().interactType;

                // Identify what type of object
                switch (grabType)
                {
                    case ("Grab Light"):
                        // Set HandOffset
                        //handOffset = new Vector3(1.3f,1f,0f);

                        // Store Grabbed object Gravity values
                        grabbedObject_gravityScale = grabbedObject.GetComponent<Rigidbody2D>().gravityScale;

                        // Remove Gravity from object
                        grabbedObject.GetComponent<Rigidbody2D>().gravityScale = 0f;
                        break;

                    case ("Grab Heavy"):
                        // Set HandOffset
                        //handOffset = new Vector3(1.3f, 0f, 0f);

                        // Store Player speed
                        carrierSpeed = gameObject.GetComponent<PlayerMoveKeys>().moveSpeed;
                        carrierMaxSpeed = gameObject.GetComponent<PlayerMoveKeys>().maxSpeed;

                        // Change Player speed
                        gameObject.GetComponent<PlayerMoveKeys>().moveSpeed = carrierSpeed / 4;
                        gameObject.GetComponent<PlayerMoveKeys>().maxSpeed  = carrierMaxSpeed / 4;
                        // 

                        handOffset = grabbedObject.transform.position - carrierHand.transform.position;


                        break;

                    case ("Grab TOOHeavy"):

                        break;
                }

            }

            return (gameObject.GetComponent<Player_Stats>().isGrabbing);
        }
    }

    void flipGrabbedObject()
    {
        // Flip position if Carrier is flipped
    }

    GameObject DetectGrabObject()
    {
        // Check if we have a grabbable object in front of us


        RaycastHit2D grabCheck = Physics2D.BoxCast(transform.position, Vector2.one * grabReach, 0f, Vector2.zero, 1f, interactLayer);


        if(grabCheck)
        {
            return (grabCheck.transform.gameObject);
            
        }
        else
        {
            return (null);
        }
        
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(transform.position, Vector3.one * grabReach);
    }
}
