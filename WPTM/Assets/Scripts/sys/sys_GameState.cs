using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class sys_GameState : MonoBehaviour
{
    public GameObject canvas;
    public GameObject timerDisplay;

    bool inMatch;

    float timer;
    int stocks;
    bool lagCancel;

    /* Gather data to start the dinner */
    public void matchIsStarting(float t, int s, bool lc)
    {
        timer = t;
        stocks = s;
        lagCancel = lc;
    }

    /* Begin setting UI elements */
    public void matchHasStarted()
    {
        inMatch = true;

        canvas.SetActive(true);
        timerDisplay.GetComponent<TMPro.TextMeshProUGUI>().text = timer.ToString();
    }

    public float getTimerInfo()
    {
        return timer;
    }

    void Update()
    {
        if(!inMatch) return;
    }
}
