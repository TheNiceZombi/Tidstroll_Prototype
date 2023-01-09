using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoveClicking : MonoBehaviour
{
    float direction = 0f;
    bool stopMoving = true;
    bool Climbing = false;

    Vector3 currentPosition;
    Vector3 clickPosition;

    

    Rigidbody2D player_Rigidbody;

    /// MOVEMENT
    [Header ("Movement")]
    public float deadZone = 5f;
    public Camera ReferenceCamera;
    public float movementSpeed = 20f;

    Vector3 wallDetectWidth;
    Vector3 wallDetectOffset;
    public float wallWidth  = 1f;
    public float wallOffset = 0.1f;
    public LayerMask wallLayer;

    public float climbSpeed = 50f; 
    //
    

    // Start is called before the first frame update
    void Start()
    {
        wallDetectWidth  = new Vector3(wallWidth,0f,0f);
        wallDetectOffset = new Vector3(0f,wallOffset,0f);
        player_Rigidbody = gameObject.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (Input.GetMouseButton(0))
        {
            updateGoal();
        }

        
        if (!Climbing)
        {
            if ( !checkForWalls() )
            {
                moveTowardsGoal();
            }
        }
        else
        {
            player_Rigidbody.AddForce(Vector2.up * climbSpeed);
        }

    }

    ///
    /// /// FUNCTIONS
    /// 

    void moveTowardsGoal()
    {
        direction = 0f;

        // Get your position on the screen
        currentPosition = gameObject.transform.position;
        Vector3 currentPositionInPixels = ReferenceCamera.WorldToScreenPoint(currentPosition);

        // Compare your position with where you clicked
        // Get Direction;
        if (clickPosition.x > currentPositionInPixels.x + deadZone)
        {
            direction = 1;

        }
        if (clickPosition.x < currentPositionInPixels.x - deadZone)
        {
            direction = -1;
            
        }
        if (direction == 0)
        {
            stopMoving = true;
        }

        // If you have not reached the goal
        // Move in direction of click;
        if(!stopMoving)
        { 
            player_Rigidbody.AddForce(Vector2.right * movementSpeed * direction); //* Time.deltaTime);
        }

    }

    void updateGoal()
    {
        clickPosition = Input.mousePosition;
        stopMoving = false;

        Debug.Log("Position :[" + currentPosition.x + "/" + Input.mousePosition.x + "]: Mouse");
    }

    // CHECKING FOR WALLS 
    // -----------------------------

    private bool checkForWalls()
    {
        bool wallCollision = Physics2D.Raycast(transform.position,Vector3.right, wallDetectWidth.x, wallLayer);

        return (wallCollision);
    }

    // Debug

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawLine(transform.position + wallDetectOffset, transform.position + wallDetectOffset + Vector3.right.x * wallDetectWidth);
        Gizmos.DrawLine(transform.position + wallDetectOffset, transform.position + wallDetectOffset + Vector3.left.x * wallDetectWidth);
    }
}
