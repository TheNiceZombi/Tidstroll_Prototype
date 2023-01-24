using AC;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player3D_Grab : MonoBehaviour
{
    // Carrier is the character carring the grabbed object

    // Grabbed object is the object being grabbed

    [Header("Carrier")]
    public GameObject carrierHand;
    Transform grabPoint;
    public LayerMask interactLayer;
    public Vector3 handOffset;
    public float grabReach = 3f;
    //public InputAction playerAction;


    private Vector3 grabDragDifference;

    public bool isGrabbing;
    GameObject grabbedObject;
    string grabType;

    float carrierSpeed;
    float carrierMaxSpeed;
    float grabbedObject_gravityScale;

    bool debugging = false;

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
            if(debugging)Debug.Log("Attempt to Grab");
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
                    grabbedObject.GetComponent<Rigidbody>().MovePosition(carrierHand.transform.position + handOffset);//transform.position = carrierHand.transform.position + handOffset;

                    break;
                case ("Grab Heavy"):

                    // Get Push Vector
                    Vector3 pushVector = gameObject.transform.position - grabDragDifference;

                    //Debug.Log("Push Vector :: " + (grabbedObject.transform.position + pushVector));

                    // Push Object
                    grabbedObject.GetComponent<Rigidbody>().MovePosition((grabbedObject.transform.position + pushVector)); //* 1.2f ); 

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
                    grabbedObject.GetComponent<Rigidbody>().useGravity = true;


                    // Reset "grabbed object" id
                    grabbedObject = null;
                    grabbedObject_gravityScale = 0f;
                    break;

                case ("Grab Heavy"):

                    // reset Carrier Speed
                    gameObject.GetComponent<PlayerMove3D>().moveSpeed = carrierSpeed;
                    //gameObject.GetComponent<PlayerMove3D>().maxSpeed = carrierMaxSpeed;

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
            if(debugging)Debug.Log("Look for Item to grab");
            grabbedObject = DetectGrabObject();

            if (grabbedObject != null)
            {
                // Tell Stats that we are grabbing
                if(debugging)Debug.Log("Grabbed Obejct = " + grabbedObject.name);

                grabDragDifference = gameObject.transform.position;
                gameObject.GetComponent<Player_Stats>().isGrabbing = true;
                grabType = grabbedObject.GetComponent<InteractableClass>().interactType;

                // Identify what type of object
                switch (grabType)
                {
                    case ("Grab Light"):
                        // Set HandOffset
                        //handOffset = new Vector3(1.3f,1f,0f);

                        // Remove Gravity from object
                        grabbedObject.GetComponent<Rigidbody>().useGravity = false;

                        break;

                    case ("Grab Heavy"):
                        // Set HandOffset
                        //handOffset = new Vector3(1.3f, 0f, 0f);

                        // Store Player speed
                        carrierSpeed = gameObject.GetComponent<PlayerMove3D>().moveSpeed;
                        //carrierMaxSpeed = gameObject.GetComponent<PlayerMove3D>().maxSpeed;

                        // Change Player speed
                        gameObject.GetComponent<PlayerMove3D>().moveSpeed = carrierSpeed / 4;
                        //gameObject.GetComponent<PlayerMoveKeys>().maxSpeed = carrierMaxSpeed / 4;
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


        //bool grabCheck = Physics.BoxCast(transform.position,grabReach,transform.forward);//(transform.position, Vector2.one * grabReach, 0f, Vector2.zero, 1f, interactLayer);
        Debug.Log("Cast Ray");


        //new Vector3(transform.position.x, transform.position.y, transform.position.z + 2f)
        Ray theRay = new Ray( transform.position , transform.TransformDirection(Vector3.forward * grabReach));

        if (Physics.Raycast(theRay, out RaycastHit hit, grabReach, interactLayer))
        {
            /*if (hit.collider.tag == "interact")//gameObject.GetComponent<InteractableClass>())
            {
                Debug.Log("FOUND THE CUBE");
                return (hit.transform.gameObject);
            }
            else
            {
                Debug.Log("Did not find Cube");
                return (null);
                //return (hit.transform.gameObject);
            }*/
            if (debugging)Debug.Log("FOUND THE CUBE");
            return (hit.transform.gameObject);

        }
        else
        {

            return (null);
        }

    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawRay( transform.position , transform.TransformDirection(Vector3.forward * grabReach));//DrawWireCube(transform.position, grabReach);
    }
}

