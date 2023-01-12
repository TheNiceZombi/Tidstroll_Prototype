using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Time_Control : MonoBehaviour
{

    /*
        This script rewinds all objects with the "TimeControlled" component attached to it.
    */


    bool debugging = false;

    //TimeControlled[] timeObjects;

    TimeControlled[] timeObjects;

    int rewindSpeedAmount = 1;
    public enum timeWinding
    {
        none,
        backwards
    }

    public timeWinding timeState;


    // Start is called before the first frame update
    void Awake()
    {
        // INITIATE TimeObject Array
        timeObjects = GameObject.FindObjectsOfType<TimeControlled>();

        // Time starts as normal
        timeState = timeWinding.none;

    }



    // Update is called once per frame
    void FixedUpdate()
    {
        // What to do with time.
        switch (timeState)
        {
            // NONE ( No time effects )
            case (timeWinding.none):

                break;

            // REWIND
            case (timeWinding.backwards):


                /// TELL: Every Time Object to Rewind
                for (int objectIndex = 0; objectIndex < timeObjects.Length; objectIndex++)
                {
                    TimeControlled timeObject = timeObjects[objectIndex];

                    timeObject.rewindSpeedAmount = rewindSpeedAmount;
                    timeObject.is_timeManipulated = true;


                }
                break;
        }


    }

    /// BUTTON FOR REWINDING
    public void rewindTime(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            timeState = timeWinding.backwards;
            if (debugging) Debug.Log("REWIND: Initiated");
        }
        if (context.canceled)
        {
            timeState = timeWinding.none;
            rewindSpeedAmount = 1;
            if (debugging) Debug.Log("REWIND: Cancled");
        }


    }
    
    /// CONTROL Rewind speed
    public void rewindSpeed(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            // IF: We are rewinding Time
            if (timeState == timeWinding.backwards)
            {
                int _previousSpeed = rewindSpeedAmount;

                // Use arrows to set rewind speed
                int speed = (int)Mathf.Round(context.ReadValue<Vector2>().x);

                rewindSpeedAmount += speed;

                if (_previousSpeed != rewindSpeedAmount)
                {
                    if (debugging) Debug.Log("REWIND SPEED: " + rewindSpeedAmount);
                }

            }

        }

    }

}

