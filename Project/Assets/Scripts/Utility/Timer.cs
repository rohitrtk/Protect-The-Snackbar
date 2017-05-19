using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Class used for making timers that count down from a specifed time
/// </summary>
public class Timer : MonoBehaviour{

    /// <summary>
    /// Time left on the timer in seconds
    /// </summary>
    public float _timeLeft; // Doesnt need to be serialized but it will make debugging easier at a glance mid game
    /// <summary>
    /// Debugging info just a note to leave on the timer
    /// </summary>
    public string _name; // Doesnt need to be serialized but it will make debugging easier at a glance mid game
    
    /// <summary>
    /// If the timer has finished
    /// </summary>
    public bool _finished;

    void Start()
    {
        _finished = false;
    }

	void Update () {
		if(_timeLeft > 0) //If a timer is running
        {
            _timeLeft -= Time.deltaTime;
        }
        else
        {
            _finished = true;
        }
	}
}
