using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonPressure_moveDoor : MonoBehaviour
{
    [SerializeField]
    GameObject door;

    public Vector3 doorDestination;

    private void OnTriggerEnter(Collider other)
    {
        door.transform.position += doorDestination;
    }

    private void OnTriggerExit(Collider other)
    {
        door.transform.position -= doorDestination;
    }

}
