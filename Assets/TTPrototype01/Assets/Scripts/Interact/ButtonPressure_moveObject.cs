using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonPressure_moveObject : MonoBehaviour
{
    [SerializeField]
    GameObject _object;

    public Vector3 objectDestination;

    private void OnTriggerEnter(Collider other)
    {
        _object.transform.position += objectDestination;
    }

    private void OnTriggerExit(Collider other)
    {
        _object.transform.position -= objectDestination;
    }

}
