/*
    sys_GameRule

    Summons game with the preset ruleset including lives, timer, and etc.
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class sys_GameRule : MonoBehaviour
{
    public bool startOnLoad; //Start match instantly after boot

    public float manualTimerOverride; //forcing timer for debug purposes
    public int manualLivesOverride;
    public bool manualLagCancelOverride;
    public int manualStageOverride;

    float timer;
    int stocks;
    bool lagCancel;
    int stageNum;

    void Start()
    {
        if(!startOnLoad) return;

        setTimer(manualTimerOverride);
        setStocks(manualLivesOverride);
        setLagCancel(manualLagCancelOverride);
        setStage(manualStageOverride);

        startMatch();
    }

    /* Data settings for GameRule before we start the dinner */
    public void setTimer(float timerVal)
    {
        timer = timerVal;
    }

    public void setStocks(int stocksVal)
    {
        stocks = stocksVal;
    }

    public void setLagCancel(bool isLagCancel)
    {
        lagCancel = isLagCancel;
    }

    public void setStage(int stageId)
    {
        stageNum = stageId;
    }

    /* BEGIN! */
    void startMatch()
    {
        //Get GameState
        sys_GameState gameState = GameObject.Find("GameState").GetComponent<sys_GameState>();

        gameState.matchIsStarting(timer, stocks, lagCancel);
        SceneManager.LoadScene(stageNum);

        gameState.matchHasStarted();
    }
}
