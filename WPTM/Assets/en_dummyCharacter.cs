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
        //oopsie woopsie
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

    //For allowing the player to move, this is called in animation data
    public void updateMoveStatus(bool status)
    {
        inMove = status;

        mh.updateMovementStatus(inMove);
    }

    //Parameters aren't allowed in animation triggers soooo
    public void animationTrigger_updateMoveStatus()
    {
        bm.resetPackageData();
        updateMoveStatus(false);
    }

    //for stopping the player from moving while attacking and ruining the dinner
    public bool areWeInMove()
    {
        return inMove;
    }

    void resetPlayerState()
    {
        percent = 0.0f;
    }

    void Start()
    {
        mh = GetComponent<gm_movementHandler>();
        bm = GetComponent<pl_buttonManager>();

        inBlastzone = false;
        updateMoveStatus(false);
    }

    void Update()
    {
        Debug.Log(areWeInMove());

        if(areWeInMove()) return;
        
        //something came in the mail today
        if(bm.awaitingPackage())
        {
            if(bm.attackIdentity() >= 4.1f)
            {
                airdodge(bm.attackIdentity());

                return;
            }

            //what is it?
            switch(bm.attackIdentity())
            {
                case 1.1f: //jab data
                    atk_jab(); 
                    break;
                case 2.5f:
                    special_a(); 
                    break;
            }

            bm.resetPackageData();
        }
    }

    /* Airdodges */
    void airdodge(float dir)
    {
        if(mh.onGround()) return;
        if(areWeInMove()) return; else updateMoveStatus(true);

        GetComponent<Animator>().Play("dummy_airdodge");
        mh.airDodge(dir);
    }

    /* Grounded Normals */
    
    void atk_jab()
    {
        //ruining dinners and all blah blah
        if(areWeInMove()) return; else updateMoveStatus(true);

        Debug.Log("JABBING"); //WHOOOOOOOOOO

        updateMoveStatus(false);
    }

    /* Specials */

    void special_a()
    {
        if(areWeInMove()) return; else updateMoveStatus(true);

        Debug.Log("SPECIALS"); //WHOOOOOOOOOO

        updateMoveStatus(false);
    }
}
