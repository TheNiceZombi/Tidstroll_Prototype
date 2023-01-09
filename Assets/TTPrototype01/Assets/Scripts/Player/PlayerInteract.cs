using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInteract : MonoBehaviour
{
    [Header("Interaction :: ")]
    public Vector3 inteactionSize;
    public LayerMask interactionLayer;


    //public GameObject interactObject;



    /// // Interacting
    public void Interact(InputAction.CallbackContext context)
    {
        if(context.started)
        {
            // Find interactable object nearby

            if (gameObject.GetComponent<Player_Stats>().nearInteract)
            {
                RaycastHit2D foundInteraction = Physics2D.BoxCast(transform.position, inteactionSize, 0, Vector2.one, 0f, interactionLayer);

                Debug.Log("Found Object: " + foundInteraction.transform.gameObject.name);

                // IF: We find one
                if (foundInteraction)
                {
                    //interactObject = foundInteraction.transform.gameObject;

                    // Tell them to execute their script
                    foundInteraction.transform.gameObject.GetComponent<InteractableClass>().ExecuteScript();

                }


            }

            
        }
    }

}
