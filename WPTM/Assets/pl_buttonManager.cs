using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
    Button manager 
 */
public class pl_buttonManager : MonoBehaviour
{
    bool moveLeft;
    bool moveRight;
    bool walkLeft;
    bool walkRight;
    bool crouch;
    bool block;
    bool attackBtn;
    bool specialBtn;
    bool upButton;
    bool dodge;
    bool dodgeR;

    bool package;
    float identification; 
    
    /* 
        Attack ID's:
        Jab / Nair: 1.1
        Ftilt / Fair: 1.2
        Bair?: 1.3
        Dtilt / Dair: 1.4
        Utilt / Uair: 1.5
        Grab / Zair: 1.6

        Specials: 2.1, 2.2, 2.3, 2.4...

        Fsmash / Fair: 3.2
        Bair?: 3.3
        Dsmash / Dair: 3.4
        Usmash / Uair: 3.5
        Zair: 3.6

        nairDodge: 4.5
        1airDodge: 4.1
        2airDodge: 4.2
        3airDodge: 4.3
     */
    
    public bool awaitingPackage()
    {
        return (identification > 0) ? true : false;
    }

    public void resetPackageData()
    {
        identification = 0;
    }

    public float attackIdentity()
    {
        return identification;
    }

    void Start()
    {
        moveLeft = false;
        moveRight = false;
        walkLeft = false;
        walkRight = false;
        crouch = false;
        block = false;
        package = false;
        attackBtn = false;
        specialBtn = false;
        upButton = false;
        dodge = false;
        dodgeR = false;
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.W))
            upButton = true;
        if(Input.GetKey(KeyCode.S))
            crouch = true;
        if(Input.GetKeyDown(KeyCode.A))
            moveLeft = true;
        if(Input.GetKeyDown(KeyCode.D))
            moveRight = true;
        if(Input.GetKeyDown(KeyCode.LeftArrow))
            walkLeft = true;
        if(Input.GetKeyDown(KeyCode.RightArrow))
            walkRight = true;
        if(Input.GetKeyDown(KeyCode.P))
            attackBtn = true;
        if(Input.GetKey(KeyCode.O))
            specialBtn = true;
        if(Input.GetKey(KeyCode.F))
            dodge = true;
        if(Input.GetKey(KeyCode.G))
            dodgeR = true;
        
        if(dodge)
        {
            identification = 4.3f;
        }
        else if(dodgeR)
        {
            identification = 4.1f;
        }

        /*
            Tilts
        */

        else if(walkRight && attackBtn)
            identification = 1.2f;
        else if(walkLeft && attackBtn)
            identification = 1.3f;
        else if(crouch && attackBtn)
            identification = 1.4f;
        else if(upButton && attackBtn)
            identification = 1.5f;     
        else if(attackBtn)
            identification = 1.1f;

        /*
            Some aerials (smashes later)
        */
        
        else if(moveRight && attackBtn)
            identification = 3.2f;
        else if(moveLeft && attackBtn)
            identification = 3.3f;

        /*
            Specials
        */

        else if((moveLeft || walkLeft) && specialBtn)
            identification = 2.1f;
        else if((moveRight || walkRight) && specialBtn)
            identification = 2.2f;  
        else if(crouch && specialBtn)
            identification = 2.3f;    
        else if(upButton && specialBtn)
            identification = 2.4f;  
        else if(specialBtn)
            identification = 2.5f;     

        /* 
            Reset keys
         */

        if(!Input.GetKey(KeyCode.W))
            upButton = false;
        if(!Input.GetKey(KeyCode.S))
            crouch = false;
        if(!Input.GetKey(KeyCode.A))
            moveLeft = false;
        if(!Input.GetKey(KeyCode.D))
            moveRight = false;
        if(!Input.GetKey(KeyCode.LeftArrow))
            walkLeft = false;
        if(!Input.GetKey(KeyCode.RightArrow))
            walkRight = false;
        if(!Input.GetKey(KeyCode.P))
            attackBtn = false;
        if(!Input.GetKey(KeyCode.O))
            specialBtn = false;
        if(!Input.GetKey(KeyCode.F))
            dodge = false;
        if(!Input.GetKey(KeyCode.G))
            dodgeR = false;;
    }
}

