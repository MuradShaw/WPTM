using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gm_groundCheck : MonoBehaviour
{
    bool groundCheck;

    public bool onGround()
    {
        RaycastHit hit;

	    float distance = 1f;
	    Vector3 dir = new Vector3(0, -1f);

        //See if we're grounded via raycast
	    return(Physics.Raycast(transform.position, dir, out hit, distance));
    }
}
