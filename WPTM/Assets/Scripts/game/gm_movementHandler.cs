using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gm_movementHandler : MonoBehaviour
{
    bool moveLeft;
    bool moveRight;
    bool jumping;
    bool run;
    int jumps;
    int defaultJumps;

    //Crucial player stats:
    float maxSpeed;
    float runTimeZeroToMax; //accel
    float runTimeMaxToZero; //dccel
    float accel;
    float dccel;
    float fallspeed;

    float leftVelocity;
    float rightVelocity;
    int neg;

    Rigidbody rb; 

    public void OnTriggerEnter(Collider other)
    {
        //if(other.tag == "JumpRestoration" && jumps < defaultJumps) restoreJumps();
    }

    void Start()
    {
        moveLeft = false;
        moveRight = false;
        run = false;

        maxSpeed = 20f;
        runTimeZeroToMax = 0.2f;
        runTimeMaxToZero = 0.3f;

        accel = maxSpeed / runTimeZeroToMax;
        dccel = -maxSpeed / runTimeMaxToZero;
        leftVelocity = 0f;
        rightVelocity = 0f;
        neg = 1;

        fallspeed = 12.0f;
        defaultJumps = 1;

        jumps = defaultJumps;
        rb = GetComponent<Rigidbody>();
        rb.useGravity = false;
    }

    void Update()
    {
        if(Input.GetKey(KeyCode.A))
            moveLeft = true;
        if(Input.GetKey(KeyCode.D))
            moveRight = true;
        if(Input.GetKey(KeyCode.Space))
            jumping = true;
    }

    void FixedUpdate()
    {
        if(moveRight) neg = -1; else if(moveLeft) neg = 1;

        /* Move Left */
        if(moveLeft || moveRight)
        {
            leftVelocity += accel * Time.deltaTime;
            leftVelocity = Mathf.Min(leftVelocity, maxSpeed); //are we max speed?

            rb.velocity = Vector3.left * leftVelocity * neg; 
        }
        else //dcell from lack of movement
        {
            leftVelocity += dccel * Time.deltaTime;
            leftVelocity = Mathf.Max(leftVelocity, 0);

            rb.velocity = Vector3.left * leftVelocity * neg; 
        }

        if(moveLeft && !Input.GetKey(KeyCode.A))
            moveLeft = false;
        if(moveRight && !Input.GetKey(KeyCode.D))
            moveRight = false;

        updateGravity();
    }

    // Maybe soon <3
    void Accel(float accel)
    {
        return;
    }

    void updateGravity()
    {
        Vector3 gravity = -35.81f * fallspeed * Vector3.up;
        rb.AddForce(gravity, ForceMode.Acceleration);
    }
}
