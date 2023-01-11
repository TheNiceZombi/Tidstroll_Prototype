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

    Vector3 previousPosition;

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

    List<RecordedData> recordList = new List<RecordedData>();

    // Start is called before the first frame update
    void Start()
    {
        //recordedData = new RecordedData[recordMax];
        //recordList = ;//(new RecordedData[recordMax]);

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
            /// PRIME: Reset 
            was_TimeManipulated = true;

            TimeUpdate();

        }
        else
        {
            if (previousPosition != transform.position)
            {
                TimeRecord();
            }
            /// RESET: So that the newest entry is the current entry
            if (was_TimeManipulated)
            {
                recordCount = recordList.Count-1;
                rewindSpeedAmount = 0;
                was_TimeManipulated = false;

                

            }


            previousPosition = transform.position;

        }
    }
    ///

    /// Travel in Time
    public void TimeUpdate()
    {
        /// IF: Rewind speed isn't zero
        if (rewindSpeedAmount != 0)
        {
            // Step through recording
            recordIndex -= rewindSpeedAmount;

            Debug.Log("RecordIndex: " + recordIndex +" RecordCount: " + recordList.Count);

            // CLAMP: RecordIndex so it does not go out of bounds  
            recordIndex = Mathf.Clamp(recordIndex, 1, recordList.Count - 2);
            if (debugging)
            {
                Debug.Log("RewindSpeed: " + rewindSpeedAmount);
                Debug.Log("RecordIndex: " + recordIndex);
            }
        }



        /// SET: Position to past position
        RecordedData data = recordList[recordIndex];
        gameObject.transform.position = data.recordedPosisiton;
        gameObject.transform.rotation = data.recordedRotation;

        // SET: Velocity to past velocity
        if (gameObject.GetComponent<Rigidbody>())
        {
            rigidBody.velocity = data.recordedVelocity;
            rigidBody.angularVelocity = data.recordedAngularVelocity;


        }

        // REMOVE: recordings
        //recordList.RemoveAt(recordIndex);//recordList.Count - 1);

        Debug.Log("DataList size: " + recordList.Count);

        /// IF: TimeMaster has stopped time travel
        if (timeMaster != null)
        { 
            if(timeMaster.GetComponent<Time_Control_3>().timeState == Time_Control_3.timeWinding.none)
            {
                is_timeManipulated = false;
            }

        }

    }
    ///

    /// Record points in Time
    void TimeRecord()
    {
        // Get component
        RecordedData data = new RecordedData();

        /// Update Component

        //  CHECK FOR RIGIDBODY                    
        if (gameObject.GetComponent<Rigidbody>())
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
        recordList.Add( data );  //[recordCount] = data;

        // Increment
        recordCount++;
        recordIndex = recordCount;

        if (debugging) Debug.Log("recordCount: "+ recordList.Count);


    }
    ///

}
