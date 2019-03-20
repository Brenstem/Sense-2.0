using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer
{
    private bool mTimerFinished;
    public bool TimerFinished { get { return mTimerFinished; } }

    private bool timerRunning = false;
    private float timeLeft;

    public void UpdateTimer()
    {
        if (timerRunning)
        {
            timeLeft -= Time.deltaTime;

            if (timeLeft <= 0)
            {
                mTimerFinished = true;
                timerRunning = false;
            }
        }
    }

    public void StartTimer(float time)
    {
        mTimerFinished = false;
        timeLeft = time;
        timerRunning = true;
    }


    public void ResetTimer()
    {
        mTimerFinished = false;
        timerRunning = false;
    }
}
