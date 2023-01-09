using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;

    public float smoothSpeed = 0.025f;
    public Vector3 cameraOffset;

    private void Awake()
    {
        
    }

    private void LateUpdate()
    {
        Vector3 followVector = new Vector3(target.position.x, target.position.y, 0f);
        followVector += cameraOffset;

        Vector3 smoothPosition = Vector3.Lerp(transform.position, followVector, smoothSpeed);

        transform.position = smoothPosition;
    }

    /*
    public GameObject followObject;
    public Vector2 followOffset;
    public float speed = 6f;
    private Vector2 threshold;
    private Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        threshold = calculateThreshold();
        rb = followObject.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        // Get difference between Player and Camera position
        Vector2 follow = followObject.transform.position;
        float xDifference = Vector2.Distance(Vector2.right * transform.position.x, Vector2.right * follow.x);
        float yDifference = Vector2.Distance(Vector2.up * transform.position.y, Vector2.up * follow.y);

        // Debug the difference
        Debug.Log( "X difference: "+ Mathf.Abs(xDifference) +" || Y difference: "+ Mathf.Abs(yDifference) );
        Debug.Log( "X Threashold: "+ threshold.x +" || Y Threashold: "+ threshold.y);

        Vector3 newPosition = transform.position;

        if(Mathf.Abs(xDifference) >= threshold.x)
        {
            newPosition.x = follow.x;
        }

        if (Mathf.Abs(yDifference) >= threshold.y)
        {
            newPosition.y = follow.y;
        }

        float moveSpeed = speed;// = rb.velocity.magnitude > speed ? rb.velocity.magnitude : speed;
        if (rb.velocity.magnitude > speed)
        {
            moveSpeed = rb.velocity.magnitude;
        }
        transform.position = Vector3.MoveTowards(transform.position,newPosition, moveSpeed * Time.deltaTime);

    }

    /// //
    /// Functions
    /// //
   
    private Vector3 calculateThreshold()
    {
        Rect aspect = Camera.main.pixelRect;
        Vector2 thresholdd = new Vector2(Camera.main.orthographicSize * aspect.width / aspect.height,
                                Camera.main.orthographicSize);
        thresholdd.x -= followOffset.x;
        thresholdd.y -= followOffset.y;

        return thresholdd;
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Vector2 border = calculateThreshold();

        Gizmos.DrawWireCube(transform.position, new Vector3(border.x * 2, border.y * 2, 1));
    }
    */
}
