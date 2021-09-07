using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pl_wallDetection : MonoBehaviour
{
    bool idk;

    public void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Stage")
            idk = true;
    }

    public void OnCollisionExit(Collision collision)
    {
        if(collision.gameObject.tag == "Stage")
            idk = false;
    }

    public bool wallDetected()
    {
        return idk;
    }
}
