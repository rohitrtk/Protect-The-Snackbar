using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerManager : MonoBehaviour {

    private bool _timerOn = false;
    private float _goalTime = 0;

	// Use this for initialization
	void Start () {
	    
	}
	
	// Update is called once per frame
	void Update () {
        if (PhotonNetwork.isMasterClient && _goalTime > 0)
        {
            StartCoroutine("TimeDown");
        }
	}

    public void Spawn() //REMEMBER TO SUMMARY THIS LATER
    {
        
    }

    //TODO: IM AWARE THIS STUFF ISNT WORKING RN  ILL FIXIXIXIXIX IT WHEN I GET HOME

    public bool Timer(int timeInSeconds)
    {
        if (_goalTime <= 0)
        {
            _goalTime = timeInSeconds;

            return false;
        }

        return false;
    }


    public void displayTime() //GUI a time mainly for countdowns before waves
    {
        
    }

    private IEnumerator TimeDown()
    {
        while (_goalTime > 0)
        {
            _goalTime -= Time.deltaTime;
            yield return null;
        }
        
    }



}