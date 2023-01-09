using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI_Manager : MonoBehaviour
{
    [Header("Speed")]
    [SerializeField] private float movement;
    [SerializeField] private float jumpHeight;

    [Header("Physics")]
    private float playerGravity;

    [SerializeField] private float collideOffsetX;
    [SerializeField] private float collideOffsetY;
    [SerializeField] private Vector3 collideSize;

    public Transform target;
    private Rigidbody2D rb;
    private Vector3 targetPosition;
    

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        // Follow the Target
        targetPosition = new Vector3(target.position.x, rb.position.y, 0f);
        rb.position = Vector2.MoveTowards(transform.position,targetPosition, movement * Time.deltaTime);

        /*
            Ok great, we still need to make a countdown that turns of jumping so we cannot
            climb indefinitely.
            
            The Countdown should be reset when you are:
            - On the ground
            - Not next to a wall
            


        */

        if (gameObject.GetComponent<Player_Stats>().canJump)
        {
            // IF: A wall is in the way.
            if (detectWall())
            {
                // Jump over it.
                Jump();
            }
        }

    }

    /// 
    /// // FUNCTIONS
    ///

    private void Jump()
    {

        rb.AddForce(new Vector2( 0f, jumpHeight), ForceMode2D.Impulse);
    }

    private bool detectWall()
    {
        Vector3 transform = gameObject.transform.position;


        // LOOK FOR EVERYTHING

        
        if( Physics2D.BoxCast(new Vector2(transform.x + collideOffsetX, transform.y - collideOffsetY), collideSize,0f, Vector2.zero) || // RIGHT
            Physics2D.BoxCast(new Vector2(transform.x - collideOffsetX, transform.y - collideOffsetY), collideSize, 0f, Vector2.zero) ) // LEFT
        {
            //RaycastHit2D other = Physics2D.BoxCast(new Vector2(transform.x + collideOffsetX, transform.y - collideOffsetY), collideSize, 0f, Vector2.zero);
            //Debug.Log("collide (name) : " + other.transform.gameObject.name );//.name);
            return (true);
        }

        return (false);
    }






    /// DEBUGGING 

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.magenta;

        Vector3 transform = gameObject.transform.position;
        Gizmos.DrawWireCube(new Vector3(transform.x + collideOffsetX, transform.y - collideOffsetY, 0f), collideSize);
        Gizmos.DrawWireCube(new Vector3(transform.x - collideOffsetX, transform.y - collideOffsetY, 0f), collideSize);
    }
}
