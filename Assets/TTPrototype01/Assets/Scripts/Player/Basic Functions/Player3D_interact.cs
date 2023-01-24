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
    


    /// // Interacting
    public void Interact(InputAction.CallbackContext context)
    {
        /// IF: Time is not Rewinding
        if (gameObject.GetComponent<Player_Stats>().timeMaster.GetComponent<Time_Control>().timeState == Time_Control.timeWinding.none)
        {
            if (context.started)
            {
                Debug.Log("Looking to interact");

                // Find interactable object nearby
                Ray theRay = new Ray(transform.position, transform.TransformDirection(Vector3.forward * interactReach));

                // IF: We find one
                if (Physics.Raycast(theRay, out RaycastHit hit, interactReach, interactionLayer))
                {

                    Debug.Log("Object hit :: " + hit.transform.gameObject.name);

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

            }
        }
    }
}