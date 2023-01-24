using AC;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AC_DoorBehaviour : MonoBehaviour
{
    public bool isLocked = true;
    public Vector3 hotspotSize;

    /// // This is the Actionlist that triggers when you open the door.
    public AC.ActionListAsset actionListed;


    public void GoThroughDoor()
    {
        if (!isLocked)
        {
            Debug.Log("Going through Door");
            //actionListed.Interact();
            Destroy(gameObject);

        }
        else
        {
            Debug.Log("Door is Locked");
        }

    }

    public void UnlockDoor()
    {
        isLocked = false;

        Debug.Log("Unlocking Door");
        //gameObject.GetComponent<InteractableClass>().interactType = "Open Door";

        // For now: DESTROY
        //actionListed.Interact();

        Destroy(gameObject);

    }

    public void LockDoor()
    {
        isLocked = true;

        Debug.Log("Locking Door");
        //gameObject.GetComponent<InteractableClass>().interactType = "Unlock Door";
    }

    /// DEBUG: Show how far down we are checking for the ground
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(gameObject.transform.position,hotspotSize);


    }


}
