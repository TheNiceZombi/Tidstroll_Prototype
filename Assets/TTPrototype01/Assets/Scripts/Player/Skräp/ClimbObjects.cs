using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClimbObjects : MonoBehaviour
{
    Rigidbody2D player_rigidbody;
    [SerializeField] float Climbspeed = 15f;

    // Start is called before the first frame update
    void Start()
    {
        player_rigidbody = gameObject.GetComponent<Rigidbody2D>();
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Climbable")
        {
            //gameObject.force ConstantForce2D();
            player_rigidbody.AddForce(Vector2.up* Climbspeed);
        }
    }
}
