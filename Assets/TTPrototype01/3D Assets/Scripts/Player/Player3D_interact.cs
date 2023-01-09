using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player3D_interact : MonoBehaviour
{
    [Header("Interaction :: ")]
    public Vector3 interactionSize;
    private float interactReach = 50f;
    public LayerMask interactionLayer;
    


    //public GameObject interactObject;



    /// // Interacting
    public void Interact(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            Debug.Log("Looking to interact");
            // Find interactable object nearby

            //if (gameObject.GetComponent<Player_Stats>().nearInteract)
            //{

            // IF: We find one
            //if (Physics.BoxCast(transform.position, interactionSize, Vector3.forward, out RaycastHit hit ,transform.rotation, interactReach, interactionLayer))
            
            Ray theRay = new Ray(transform.position, transform.TransformDirection(Vector3.forward * interactReach));

            if (Physics.Raycast(theRay, out RaycastHit hit, interactReach, interactionLayer))
            {
                
                Debug.Log("Object hit :: "+hit.transform.gameObject.name);

                if (hit.transform.gameObject.GetComponent<InteractableClass>())
                {
                    Debug.Log("HAS COMPONENT");

                    // Tell them to execute their script
                    hit.transform.gameObject.GetComponent<InteractableClass>().ExecuteScript();
                }

            }
            else
            {
                Debug.Log("No Object found");
            }

            //}
        }
    }
}