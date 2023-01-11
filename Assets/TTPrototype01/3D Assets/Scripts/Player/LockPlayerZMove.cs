using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockPlayerZMove : MonoBehaviour
{
    [SerializeField] public GameObject player;

    /*
    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Checking for Player");
        if (collision.gameObject == player)
        {
            Debug.Log("Player is Z LOCKED");
            player.GetComponent<Player_Stats>().zMoveLocked = true;
        }
    }
    */

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Checking for Player");
        if (other.gameObject == player)
        {
            Debug.Log("Player is Z LOCKED");
            player.GetComponent<Player_Stats>().zMoveLocked = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject == player)
        {
            player.GetComponent<Player_Stats>().zMoveLocked = false;
        }
    }


}
