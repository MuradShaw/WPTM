using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gm_movementHandler : MonoBehaviour
{
    bool moveLeft;
    bool moveRight;
    bool walkLeft;
    bool walkRight;
    bool jumping;
    bool activeJumping;
    bool shortHop;
    bool run;
    bool launching;
    float airdodge;
    bool isGrounded;
    bool stuckInMove;
    int jumps;
    int defaultJumps;

    //Character stats
    float maxSpeed;
    float launchSpeed;
    float launchAcceleration;
    float launchZeroToMax;
    float walkSpeed;
    float runTimeZeroToMax; //accel
    float runTimeMaxToZero; //dccel
    float accel;
    float dccel;
    float fallspeed;
    float airAccel; //Air accel
    float airDccel;
    float airDrift; //Max air speed
    float wavedashInf;
    float weight;

    float leftVelocity;
    float rightVelocity;

    float jumpVelocity;
    float fullhopMax;
    float shorthopMax;
    float jumpTimeZeroToMax; //jcell
    float jccel;

    bool stopJumping;
    bool doubleJump;
    bool stopActing;
    int neg;

    public bool player2;
    public pl_wallDetection wallDetection;
    Rigidbody rb; 
    //List<GameObject> hitboxes;

    //public void OnTriggerEnter(Collider other)
    //{
    //    
    //}

    //For all of your player launching needs
    public void launchPlayer(Vector3 direction, float baseKnockback, int neg)
    {
        if(launching) return; else launching = true;

        Debug.Log("launching");
        
        float percent = 15;
        launchSpeed = (15 * (percent/10) - (weight * 0.1f)) + baseKnockback;
        launchZeroToMax = 0.35f;

        launchAcceleration = launchSpeed / launchZeroToMax;
        
        multidimensionalAcceleration(direction, accel, maxSpeed, false, neg);
    }

    //to avoid moving while attacking and ruining the dinner
    public void updateMovementStatus(bool welldontkeepmewaiting)
    {
        stuckInMove = welldontkeepmewaiting;
    }

    //it's time to stop honey okay
    public void resetAirdodge()
    {
        airdodge = 0.0f;
    }

    public bool onGround()
    {
        return isGrounded;
    }

    void Start()
    {
        moveLeft = false;
        moveRight = false;
        activeJumping = false;
        run = false;
        launching = false;

        doubleJump = false;

        maxSpeed = 14.6f;
        airDrift = 2.5f;
        fullhopMax = 15f;
        shorthopMax = 9.5f;
        walkSpeed = 0.65f;
        jumpTimeZeroToMax = 0.2f;
        runTimeZeroToMax = 0.2f;
        runTimeMaxToZero = 0.3f;
        wavedashInf = 0.75f;
        weight = 75.0f;
        launchSpeed = 1.0f;
        launchZeroToMax = 1.0f;
        
        accel = maxSpeed / runTimeZeroToMax;
        dccel = -maxSpeed / runTimeMaxToZero;
        airAccel = airDrift / runTimeZeroToMax;
        airDccel = -airDrift / runTimeMaxToZero;

        leftVelocity = 0f;
        rightVelocity = 0f;
        jumpVelocity = 0.0f;
        neg = 1;

        isGrounded = true;
        fallspeed = 45.0f;
        defaultJumps = 1;
        jumps = defaultJumps;

        rb = GetComponent<Rigidbody>();
        rb.useGravity = false;

        launchPlayer(new Vector3(1f, 1f, 0), 5, 1);
    }

    void Update()
    {
        if(player2) return;

        if(Input.GetKey(KeyCode.A))
            moveLeft = true;
        if(Input.GetKey(KeyCode.D))
            moveRight = true;
        if(Input.GetKey(KeyCode.LeftArrow))
            walkLeft = true;
        if(Input.GetKey(KeyCode.RightArrow))
            walkRight = true;
        if(Input.GetKeyDown(KeyCode.Space))
            jumping = true;
        if(Input.GetKey(KeyCode.M))
            shortHop = true;

        if(stopJumping)
        {
            jumpVelocity = 0.0f; //my physics teacher would be proud
            
            jumps--;
            jumping = false;
            shortHop = false;

            stopJumping = false;
        }
        
        if(moveLeft && !Input.GetKey(KeyCode.A))
            moveLeft = false;
        if(moveRight && !Input.GetKey(KeyCode.D))
            moveRight = false;
         if(walkLeft && !Input.GetKey(KeyCode.LeftArrow))
            walkLeft = false;
        if(walkRight && !Input.GetKey(KeyCode.RightArrow))
            walkRight = false;
    }
    
    void FixedUpdate()
    {
        updateGravity();

        if(airdodge > 0.0f) airDodge(airdodge);

        if(stuckInMove) return;
        
        //are we grounded type check
        isGrounded = groundCheck();
        jumps = (groundCheck()) ? defaultJumps : jumps;

        if(moveRight || walkRight) neg = -1; else if(moveLeft || walkLeft) neg = 1;

        bool wallShinanigans = wallCheck(Vector3.left * neg);

        if((moveLeft || moveRight || walkLeft || walkRight) && !wallShinanigans)
            accelerate(((!isGrounded) ? airAccel : accel), ((walkRight || walkLeft) ? maxSpeed * walkSpeed : maxSpeed), false, neg);
        else
            accelerate(((!isGrounded) ? airDccel : dccel), maxSpeed, true, neg);
        
        //Double check to avoid rocket launching into a wall and ruining the dinner
        int doubleNeg = (neg > 0) ? -1 : 1;
        if(wallShinanigans) accelerate(accel * 0, maxSpeed, true, doubleNeg);

        if((jumping || shortHop) && jumps > 0)
            jump(shortHop);
    }

    //for all your movement wants and needs
    void accelerate(float a, float max, bool d, int n)
    {
        if(!d) //Accelerating
        {
            leftVelocity += a * Time.deltaTime;
            leftVelocity = Mathf.Min(leftVelocity, max); //are we max speed?

            rb.velocity = Vector3.left * leftVelocity * n; 
        }
        else //Deccelerating (from lack of schmovement)
        {
            leftVelocity += a * Time.deltaTime;
            leftVelocity = Mathf.Max(leftVelocity, 0);

            rb.velocity = Vector3.left * leftVelocity * n; 
        }
    }

    //For knockback and Melee! Melee! Melee!
    void multidimensionalAcceleration(Vector3 direction, float a, float max, bool d, int n)
    {
        float velocity = 0;

        if(!d) //Accelerating
        {
            velocity += a * Time.deltaTime;
            velocity = Mathf.Min(velocity, max); //are we max speed?

            rb.velocity = direction * velocity * n; 
        }
        else //Deccelerating (from lack of schmovement)
        {
            velocity += a * Time.deltaTime;
            velocity = Mathf.Max(velocity, 0);

            rb.velocity = direction * velocity * n; 
        }
    }

    //for all of your short and full hopping needs
    void jump(bool shorthop = false)
    {
        if(isGrounded) accelerate(dccel * 4.3f, maxSpeed, true, neg);

        jccel = ((shorthop) ? shorthopMax : fullhopMax) / jumpTimeZeroToMax; //Calculate jump accel using short/full hop values

        //Clean up the mess
        if(jumpVelocity >= (((shorthop) ? shorthopMax : fullhopMax)))
        {
            stopJumping = true;
        }

        //preform the jump!! 
        jumpVelocity += jccel * Time.deltaTime;
        rb.AddForce(new Vector3(0, jumpVelocity, 0), ForceMode.Impulse); //physics!!!!

        isGrounded = false;
    }

    //melee! melee! melee!
    public void airDodge(float dir)
    {
        airdodge = dir;
        stopJumping = true;

        Debug.Log("Airdodge");

        if(dir == 4.3f)
            multidimensionalAcceleration(new Vector3(8.5f, -1.0f, 0), accel, maxSpeed, false, 1);
        else if(dir == 4.1f)
            multidimensionalAcceleration(new Vector3(8.5f, -1.0f, 0), accel, maxSpeed, false, -1);
    }

    //updating individual gravity for characters
    void updateGravity()
    {
        Vector3 gravity = -9.81f * fallspeed * Vector3.up;
        rb.AddForce(gravity, ForceMode.Acceleration);
    }

    //are we grounded?
    bool groundCheck()
    {
        RaycastHit hit;

	    float distance = 0.7f;
	    Vector3 dir = new Vector3(0, -1);

        //See if we're grounded via raycast
	    return(Physics.Raycast(transform.position, dir, out hit, distance));
    }

    //stop clipping on walls dammit
    bool wallCheck(Vector3 dir)
    {
        RaycastHit hit;
        RaycastHit hit2;
        RaycastHit hit3;
        RaycastHit hit4;
        RaycastHit hit5;
	    float distance = 0.4f;
        //float thickness = 1.0f;

        //See if we're pinned via raycast
	    return(Physics.Raycast(transform.position, dir, out hit, distance) || Physics.Raycast(transform.position + new Vector3(0, -0.5f, 0), dir, out hit2, distance) || Physics.Raycast(transform.position + new Vector3(0, 0.5f, 0), dir, out hit3, distance) || Physics.Raycast(transform.position + new Vector3(0, 1.0f, 0), dir, out hit4, distance) || Physics.Raycast(transform.position + new Vector3(0, -1.0f, 0), dir, out hit5, distance));

        //return (Physics.SphereCast(transform.position + new Vector3(distance, 0, 0), thickness, dir, out hit)); 
    }
}
