using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCheckInteract : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<InteractableClass>())
        {
            Debug.LogWarning("Near Interaction");
            gameObject.transform.parent.GetComponentInParent<Player_Stats>().nearInteract = true;

            
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<InteractableClass>())
        {
            Debug.LogWarning("not GROUNDED");
            gameObject.transform.parent.GetComponentInParent<Player_Stats>().nearInteract = false;
        }
    }
}
