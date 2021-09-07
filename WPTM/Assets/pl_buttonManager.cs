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

    bool package;
    int identification; 
    
    /* 
        Attack ID's:
        Jab: 1
     */
    
    public bool awaitingPackage()
    {
        return (identification > 0) ? true : false;
    }

    public int attackIdentity()
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
    }

    void Update()
    {
        if(Input.GetKey(KeyCode.S))
            crouch = true;
        if(Input.GetKey(KeyCode.A))
            moveLeft = true;
        if(Input.GetKey(KeyCode.D))
            moveRight = true;
        if(Input.GetKey(KeyCode.LeftArrow))
            walkLeft = true;
        if(Input.GetKey(KeyCode.RightArrow))
            walkRight = true;
        if(Input.GetKey(KeyCode.P))
            attackBtn = true;
        
        if(attackBtn)
            identification = 1;
    }
}

