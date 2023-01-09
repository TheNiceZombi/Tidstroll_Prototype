using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCheckWalls : MonoBehaviour
{


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<Wall>())
        {
            gameObject.transform.parent.GetComponentInParent<Player_Stats>().nextToWall = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<Wall>())
        {
            gameObject.transform.parent.GetComponentInParent<Player_Stats>().nextToWall = false;
        }
    }
}
