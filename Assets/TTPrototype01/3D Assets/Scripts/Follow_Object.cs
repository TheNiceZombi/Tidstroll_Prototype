using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Follow_Object : MonoBehaviour
{

    [SerializeField] GameObject followObject = null;

    // Update is called once per frame
    void FixedUpdate()
    {
        if( followObject != null )
        {
            transform.position = followObject.transform.position;
        }
    }
}
