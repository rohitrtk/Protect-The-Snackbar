using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Handels spawning waves, pickups, enemy difficulty by wave. May get merged to a bigger picture gameController
/// </summary>
public class WaveManager : MonoBehaviour { //Maybe have a master controller that turns this script off if not host

    /// <summary>
    /// Current round
    /// </summary>
    [SerializeField] private int _wave = 0; //wave is the wave that the player(s) are currently on. 0 is a warmup round/get ready
    /// <summary>
    /// ref to SpawnerManager script
    /// </summary>
    private SpawnerManager _spawner; 

	// Use this for initialization
	void Start () {
        _spawner = GetComponent <SpawnerManager> ();
        
    }
	
	// Update is called once per frame
	void Update () {
        if (PhotonNetwork.isMasterClient)//If I am the host aka the only one who should be using this script
        {

            switch (_wave)//For every wave
            {
                case 0: //Warmup, 10 second countdown

                    break;

                case 1: //Round 1, 10 basic enemies, 4 second spawn interval, spawners(1-2)
                    
                    break;




            }



        }
	}


    /*
    -------------------------
    | TODO: Connor's notes: |
    -------------------------

     -Maybe use a PUNRPC to tell everyone to increment the round
     -
     -
     -


    */
}
