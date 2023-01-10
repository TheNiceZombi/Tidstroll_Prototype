using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Time_Control_3 : MonoBehaviour
{

    /*
        This script rewinds all objects with the "TimeControlled_2" component attached to it.
    */


    bool debugging = true;

    TimeControlled_2[] timeObjects;


    int rewindSpeedAmount = 1;
    public enum timeWinding
    {
        none,
        pause,
        backwards,
        forwards

    }

    public timeWinding timeState;


    // Start is called before the first frame update
    void Awake()
    {
        // INITIATE TimeObject Array
        timeObjects = GameObject.FindObjectsOfType<TimeControlled_2>();

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
                //if (debugging) Debug.Log("TIME CONTROL: Normal time");

                break;

            // REWIND
            case (timeWinding.backwards):
                if (debugging) Debug.Log("TIME CONTROL: Time is now rewinding");

                /// TELL: Every Time Object to Rewind
                for (int objectIndex = 0; objectIndex < timeObjects.Length; objectIndex++)
                {
                    TimeControlled_2 timeObject = timeObjects[objectIndex];

                    timeObject.GetComponent<TimeControlled_2>().rewindSpeedAmount = rewindSpeedAmount;
                    timeObject.is_timeManipulated = true;


                }
                break;

            //PAUSE
            case (timeWinding.pause):
                if (debugging) Debug.Log("TIME CONTROL: ");

                //


                break;

            // FAST-FORWARD
            case (timeWinding.forwards):



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
            //rewindSpeedAmount = 0;
            if (debugging) Debug.Log("REWIND: Cancled");
        }


    }
    
    /// CONTROL Rewind speed
    public void rewindSpeed(InputAction.CallbackContext context)
    {

        if (debugging) Debug.Log("REWIND SPEED: ACTIVATE");

        int speed = (int)Mathf.Round(context.ReadValue<Vector2>().x);

        if (debugging) Debug.Log("Wined speed Change:" + speed);

        rewindSpeedAmount += speed;

        if (debugging) Debug.Log("Speed is now:" + rewindSpeedAmount);


    }

}

