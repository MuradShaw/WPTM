using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class en_dummyCharacter : MonoBehaviour
{
    int stocks = 3;
    float percent = 0;

    bool inMove;
    bool inBlastzone;

    gm_movementHandler mh;
    pl_buttonManager bm;
    public Transform respawn;

    public void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Blastzone" && !inBlastzone)
        {
            inBlastzone = true;
            removeStock();
        }
    }

    public void removeStock(int amount = 1, bool includesDeathAndBoard = true)
    {
        stocks -= amount;

        if(includesDeathAndBoard)
        {
            transform.position = respawn.position + new Vector3(0, -8, 0);
           
            inBlastzone = false;
            resetPlayerState();
        }

        Debug.Log("Stock taken. Current stock count: " + stocks);
    }

    void resetPlayerState()
    {
        percent = 0.0f;
    }

    void Start()
    {
        inBlastzone = false;
        inMove = false;

        mh = GetComponent<gm_movementHandler>();
        bm = GetComponent<pl_buttonManager>();
    }

    void Update()
    {
        if(inMove) return;
        
        if(bm.awaitingPackage())
        {
            switch(bm.attackIdentity())
            {
                case 1:
                    atk_jab();
                    break;
            }
        }
    }

    /* Grounded Normals */
    
    void atk_jab()
    {
        inMove = true;

        Debug.Log("JABBING");
    }
}
