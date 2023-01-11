using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeControlled : MonoBehaviour
{
    public bool timeUpdating = true;
    public Vector3 velocity;
    public bool _isKinematic;

    private void Start()
    {
        if(GetComponent<Rigidbody>())
        {
            if(GetComponent<Rigidbody>().isKinematic)
            {
                _isKinematic = true;
            }
        }
    }

    public virtual void TimeUpdate()
    {

    }

}
