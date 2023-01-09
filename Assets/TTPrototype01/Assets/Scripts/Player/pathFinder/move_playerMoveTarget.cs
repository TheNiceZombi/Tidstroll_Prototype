using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class move_playerMoveTarget : MonoBehaviour
{

    Vector3 debugDrawing;
    

    GameObject clickedObject;
          bool lookForObject = false;

    [SerializeField] GameObject player;
    [SerializeField] string interactTag = "interact";
    [SerializeField] public Vector2 searchSize; //= Vector2.one;

    public void FixedUpdate()
    {
        if(lookForObject)
        {
            searchHitbox();
        }

    }


    public void moveMarker(InputAction.CallbackContext context)
    {
        if(context.started)
        {
            //Get Mouse Position
            Debug.Log("Player Clicked");
            Vector3 mouseWindow = Input.mousePosition;
            Vector3 mouseWorld = Camera.main.ScreenToWorldPoint(mouseWindow);

            // Debug: 
            debugDrawing = mouseWorld;
            //Debug.Log("Mouse[ " + debugDrawing.x + " / " + debugDrawing.y + " / " + debugDrawing.z + " ]");

            // Move
            gameObject.transform.position = mouseWorld;

            lookForObject = getClickedObject();

        }


    }

    //
    private bool getClickedObject()
    {
        //Collision2D.

        if(Physics2D.OverlapPoint(debugDrawing))
        {
            Collider2D other = Physics2D.OverlapPoint(debugDrawing);

            // Save clicked Object.
            clickedObject = other.gameObject;

            if (clickedObject.tag == interactTag)
            {
                return (true);
            }
            // BUT, only if we can interact with it.
            else
            {
                clickedObject = null;
            }
            
        }

        Debug.Log("Did Not find interactables");
        return (false);

    }

    //
    private void searchHitbox()
    {
        Collider2D other = Physics2D.OverlapBox(debugDrawing, searchSize, 0f);
        Debug.Log("Found :: " + other.gameObject.name + "|| Looking for :: " + clickedObject.name);

        if (other.gameObject == clickedObject)
        {
            Debug.LogError("FOUND THEM :||: ATTEMPT TO GRAB");
            player.GetComponent<PlayerGrabObject>().GrabDropObject();
            lookForObject = false;

        }

    }


    // Debug
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.black;
        
        Gizmos.DrawCube( debugDrawing, Vector3.one );

        if (lookForObject)
        {
            //Debug.Log("Looking for :: " + clickedObject.name);
            Gizmos.DrawCube( player.transform.position, searchSize );
        }

    }
}
