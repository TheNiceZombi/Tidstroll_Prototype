//using AC;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FindNearestOption_OverWorld : MonoBehaviour
{
    public GameObject FindNearest( GameObject origin )
    {
        // Variables
        GameObject[] options;
        GameObject closest = null;
        float distance = Mathf.Infinity;
        Vector3 originPosition = origin.transform.position;

        // Get all options
        options = GameObject.FindGameObjectsWithTag("Option");

        // For every option we find
        foreach( GameObject go in options )
        {
            // Get distance from one option at a time
            Vector3 diff = go.transform.position - originPosition;
            float curDistance = diff.sqrMagnitude;

            // IF this option was the closest so far
            if(curDistance < distance)
            {
                // Update loop
                closest = go;
                distance = curDistance;
            }

        }

        //closest.GetComponent<Hotspot>().Invoke()

        //Return the closest
        return (closest);
    }

}
