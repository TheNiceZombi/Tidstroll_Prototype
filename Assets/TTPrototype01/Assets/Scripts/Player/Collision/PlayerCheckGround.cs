using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCheckGround : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if ( collision.gameObject.GetComponent<Ground>() )
        {
            Debug.LogWarning("GROUNDED");
            gameObject.transform.parent.GetComponentInParent<Player_Stats>().onGround = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if ( collision.gameObject.GetComponent<Ground>() )
        {
            Debug.LogWarning("not GROUNDED");
            gameObject.transform.parent.GetComponentInParent<Player_Stats>().onGround = false;
        }
    }
}
