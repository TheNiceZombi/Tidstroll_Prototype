using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player3D_CheckGround : MonoBehaviour
{
    
    private void OnTriggerEnter(Collider collision) //OnCollisionEnter Collision
    {
        if (collision.gameObject.GetComponent<Ground>())
        {
            //Debug.Log("GROUNDED");
            gameObject.transform.parent.GetComponentInParent<Player_Stats>().onGround = true;
        }
    }

}
