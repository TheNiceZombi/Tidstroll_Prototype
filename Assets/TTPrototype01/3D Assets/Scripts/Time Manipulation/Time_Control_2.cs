using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Time_Control_2 : MonoBehaviour
{

    /*
        This script records every object with a "TimeControlled" component attached to it.
        It then uses those recordings to wined time backwards or forwards.
    */


    bool debugging = true;

    public static float gravity = -10;

    int rewindSpeedAmount = 1;

    public struct RecordedData
    {
        public Vector3 recordedPosisiton;
        public Quaternion recordedRotation;

        public Vector3 recordedVelocity;
        public Vector3 recordedAngularVelocity;




    }

    RecordedData[,] recordedData;
    int recordMax = 10000;
    int recordCount;
    int recordIndex;
    bool wasSteppingBack = false;

    TimeControlled[] timeObjects;

    enum timeWinding
    {
        none,
        pause,
        backwards,
        forwards

    }

    timeWinding timeState;

    // Start is called before the first frame update
    void Awake()
    {
        // INITIATE TimeObject Array
        timeObjects = GameObject.FindObjectsOfType<TimeControlled>();
        recordedData = new RecordedData[timeObjects.Length, recordMax];

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
                if(debugging) Debug.Log("TIME CONTROL: Normal time");

                if (wasSteppingBack)
                {
                    recordCount = recordIndex;
                    wasSteppingBack = false;
                }

                // Record movement
                for (int objectIndex = 0; objectIndex < timeObjects.Length; objectIndex++)
                {

                    // Get component
                    TimeControlled timeObject = timeObjects[objectIndex];
                    RecordedData data = new RecordedData();

                    /// Update Component

                    //  CHECK FOR RIGIDBODY                    // CHECK if "isKinematic" was turned on from the start
                    if (timeObject.GetComponent<Rigidbody>()) // && 
                    {
                        Rigidbody timeRigidBody = timeObject.GetComponent<Rigidbody>();
                        data.recordedVelocity = timeRigidBody.velocity;
                        data.recordedAngularVelocity = timeRigidBody.angularVelocity;

                        // I: Originally "isKinematic", don't turn it off.
                        if (!timeObject._isKinematic)
                        {
                            timeRigidBody.isKinematic = false;
                        }

                    }


                    data.recordedPosisiton = timeObject.transform.position;
                    data.recordedRotation = timeObject.transform.rotation;

                    timeObject.timeUpdating = true;



                    recordedData[objectIndex, recordCount] = data;


                }
                recordCount++;
                recordIndex = recordCount;




                // Let everything move
                foreach (TimeControlled timeObject in timeObjects)
                {
                    timeObject.TimeUpdate();
                }

                break;

            // REWIND
            case (timeWinding.backwards):
                if(debugging) Debug.Log("TIME CONTROL: Time is now rewinding");

                wasSteppingBack = true;

                /// DIRECTION / SPEED
                ///-------------------------- 

                //
                if (debugging)
                {
                    if (debugging)
                    {
                        Debug.Log("Record Index: " + recordIndex);
                        Debug.Log("Record Count:"  + recordCount);
                    }
                }

                if (rewindSpeedAmount != 0)
                {
                    //IF: We are not trying to go below limit.
                    //*
                    //if (recordIndex > 0 && recordIndex <= recordCount)
                    {
                        recordIndex -= rewindSpeedAmount;
                        recordIndex = Mathf.Clamp(recordIndex, 1, recordCount-1);
                        if (debugging)
                        {
                            Debug.Log("RewindSpeed: " + rewindSpeedAmount);
                            Debug.Log("RecordIndex: " + recordIndex);
                        }

                    }
                    // */
}
                else
{
                    if (debugging) Debug.Log("STOP Winding");
                }


                ///--------------------------

                /// Replay recorded movement
                for (int objectIndex = 0; objectIndex < timeObjects.Length; objectIndex++)
                {
                    TimeControlled timeObject = timeObjects[objectIndex];
                    RecordedData data = recordedData[objectIndex, recordIndex];
                    timeObject.transform.position = data.recordedPosisiton;
                    timeObject.transform.rotation = data.recordedRotation;
                    //timeObject.velocity = data.recordedVelocity;
                    timeObject.timeUpdating = false;

                    // SET: Rigidbody physics
                    if (timeObject.GetComponent<Rigidbody>())
                    {
                        Rigidbody timeRigidBody = timeObject.GetComponent<Rigidbody>();

                        timeRigidBody.velocity = data.recordedVelocity;
                        timeRigidBody.angularVelocity = data.recordedAngularVelocity;
                        //timeRigidBody.isKinematic = true;


                    }

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
    
    public void rewindSpeed(InputAction.CallbackContext context)
    {
        
        if (debugging) Debug.Log("REWIND SPEED: ACTIVATE");

        int speed = (int)Mathf.Round( context.ReadValue<Vector2>().x);

        if (debugging) Debug.Log("Wined speed Change:" + speed);

        rewindSpeedAmount += speed;

        if (debugging) Debug.Log("Speed is now:" + rewindSpeedAmount);
        

    }

}
