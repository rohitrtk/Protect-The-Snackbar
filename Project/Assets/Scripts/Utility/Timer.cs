using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer {

    /// <summary>
    /// Total duration of the timer in seconds
    /// </summary>
    private float _maxTime;
    /// <summary>
    /// How long in seconds the timer has been active for
    /// </summary>
    private float _currTime;
    /// <summary>
    /// Time when the constructor was called
    /// </summary>
    private long _then;



    public Timer(float maxTime)
    {
        _maxTime = maxTime;
        _then = DateTime.Now.Ticks / TimeSpan.TicksPerSecond; //Set the current time
    }


    public bool Complete()
    {

        long _now = DateTime.Now.Ticks / TimeSpan.TicksPerSecond; //Set the current time
        if (_now - _then >= _maxTime) return true;//If the timer has gone over its time return true
        return false;//If not return false;
    }






    public float MaxTime
    {
        get
        {
            return _maxTime;
        }
    }

    public float CurrentTime
    {
        get
        {
            long _now = DateTime.Now.Ticks / TimeSpan.TicksPerSecond;
            return _now - _then;
        }
    }
}
