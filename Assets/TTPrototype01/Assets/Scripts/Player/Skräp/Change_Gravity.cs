using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Change_Gravity : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        //Vector2 GravityAmount = (2f,2f);
        float GravityAmount = 3f;
        //Physics2D.gravity = Vector2.down * GravityAmount;

        gameObject.GetComponent<Rigidbody2D>().gravityScale = GravityAmount;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
