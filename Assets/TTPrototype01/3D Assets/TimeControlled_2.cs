using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.PlayerLoop;
using UnityEngine.Rendering.VirtualTexturing;

public class TimeControlled_2 : MonoBehaviour
{
    /*
        This script records the position and velocity of this object (unless time is being manipulated).
    */

    bool debugging = true;


    // State control
    public bool is_timeManipulated = false;
    bool        was_TimeManipulated = false;

    GameObject timeMaster;

    bool _isKinematic = false;

    // Data variables
    Rigidbody rigidBody;
    public struct RecordedData
    {
        public Vector3 recordedPosisiton;
        public Quaternion recordedRotation;

        public Vector3 recordedVelocity;
        public Vector3 recordedAngularVelocity;

    }

    // Time Travel Variables
    public int rewindSpeedAmount = 0;

    RecordedData[] recordedData;
    int recordMax = 10000;
    int recordCount;
    int recordIndex;
    bool wasSteppingBack = false;



    // Start is called before the first frame update
    void Start()
    {
        recordedData = new RecordedData[recordMax];

        if (GetComponent<Rigidbody>())
        {
            rigidBody = GetComponent<Rigidbody>();

            if (rigidBody.isKinematic)
            {
                _isKinematic = true;
            }
        }
        else
        {
            Debug.LogError("TimeControlled: Object does not have a RigidBody");
        }

        timeMaster = GameObject.Find("Time_Master");
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        /// IF: Time is being manipulated
        if (is_timeManipulated)
        {
            if (debugging) Debug.Log("STOP Recording");

            /// PRIME: Reset 
            was_TimeManipulated = true;

            TimeUpdate();

        }
        else
        {
            /// RESET: So that the newest entry is the current entry
            if (was_TimeManipulated)
            {
                recordCount = recordIndex;
                rewindSpeedAmount = 0;
                was_TimeManipulated = false;

            }

            // Get component
            RecordedData data = new RecordedData();

            /// Update Component

            //  CHECK FOR RIGIDBODY                    
            if (gameObject.GetComponent<Rigidbody>() ) 
            {
                // Record Velocity 
                data.recordedVelocity = rigidBody.velocity;
                data.recordedAngularVelocity = rigidBody.angularVelocity;

                // I: Originally "isKinematic", don't turn it off.
                if (!_isKinematic)
                {
                    rigidBody.isKinematic = false;
                }

            }

            // Record position
            data.recordedPosisiton = transform.position;
            data.recordedRotation = transform.rotation;

            // SET: Data in array
            recordedData[recordCount] = data;

            // Increment
            recordCount++;
            recordIndex = recordCount;

        }


    }

    /// Travel in Time
    public void TimeUpdate()
    {
        Debug.Log("TimeUPDATE: ACTIVATED");
        /// IF: Rewind speed isn't zero
        if (rewindSpeedAmount != 0)
        {
            // Step through recording
            recordIndex -= rewindSpeedAmount;

            // CLAMP: RecordIndex so it does not go out of bounds  
            recordIndex = Mathf.Clamp(recordIndex, 1, recordCount - 1);
            if (debugging)
            {
                Debug.Log("RewindSpeed: " + rewindSpeedAmount);
                Debug.Log("RecordIndex: " + recordIndex);
            }
        }
        else
        {
            if (debugging) Debug.Log("STOP Winding");
        }


        /// SET: Position to past position
        RecordedData data = recordedData[recordIndex];
        gameObject.transform.position = data.recordedPosisiton;
        gameObject.transform.rotation = data.recordedRotation;

        // SET: Velocity to past velocity
        if (gameObject.GetComponent<Rigidbody>())
        {
            rigidBody.velocity = data.recordedVelocity;
            rigidBody.angularVelocity = data.recordedAngularVelocity;


        }

        /// IF: TimeMaster has stopped time travel
        if (timeMaster != null)
        { 
            if(timeMaster.GetComponent<Time_Control_3>().timeState == Time_Control_3.timeWinding.none)
            {
                is_timeManipulated = false;
            }

        }


    }


}
