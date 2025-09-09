using UnityEngine;
using UnityEngine.UIElements;
public delegate void TimerFunc();
public class Timer
{
    private float time;
    private TimerFunc timerFunc;

    public Timer(float limitTime, TimerFunc timerFunc)
    {
        time = limitTime;
        this.timerFunc = timerFunc;
    }
    public void Update()
    {
        time -= Time.deltaTime;
        if(time <= 0 && timerFunc != null)
        {
            timerFunc();
            timerFunc = null;
        }
    }

};