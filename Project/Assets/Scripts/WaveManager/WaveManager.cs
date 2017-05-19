using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Handels spawning waves, pickup spawns, enemy difficulty by wave. May get merged to a bigger picture gameController
/// </summary>
public class WaveManager : MonoBehaviour { //Maybe have a master controller that turns this script off if not host

    /// <summary>
    /// Current round
    /// </summary>
    [SerializeField] private int _wave = 0; //wave is the wave that the player(s) are currently on. 0 is a warmup round/get ready
    
    /// <summary>
    /// ref to SpawnerManager script
    /// </summary>
    [SerializeField] private SpawnerManager _spawner;

    [SerializeField] private Text _waveText;

    private Timer time;

	// Use this for initialization
	void Start () { //TODO: Change this for networking
        _spawner = GetComponent <SpawnerManager> ();
        _waveText.text = "Wave " + _wave;
    }
	
	// Update is called once per frame
	void Update () {
        if (PhotonNetwork.isMasterClient)//If I am the host aka the only one who should be using this script
        {
            switch (_wave)//For every wave
            {
                case 0: //Warmup, 10 second countdown
                    #region Literally just a countdown
                    if (time == null)//No timer has been made
                    {
                        MakeTimer(10f, "Warmup Timer"); //Not a constructor, its a private method that parents a game object.
                    }
                    else //Timer has already been made
                    {
                        if (time._finished)//Timer is done
                        {
                            Destroy(time);//Clear the time
                            WaveUp();
                        }
                    }

                    break;
                #endregion

                case 1: //Round 1, 10 basic enemies, 4 second spawn interval, spawners(1-2)
                    #region Wave 1



                    #endregion
                    break;




            }



        }
	}



    /// <summary>
    /// TEMP. Sets the wave up by one and sets the HUD's info
    /// </summary>
    private void WaveUp()
    {
        _wave++; //Goto the next wave
        _waveText.text = "Wave " + _wave; //Sets the HUD's 'Wave: ' string
    }

    /// <summary>
    /// Makes a timer.
    ///  For some reason I cant make a constructor on components?? so i have to make the variables public and set em manually with this method.
    /// </summary>
    /// <param name="duration"></param>
    /// <param name="name"></param>
    private void MakeTimer(float duration, string name = "NOT SET")
    {
        time = gameObject.AddComponent<Timer>(); //Make a timer.
        time._timeLeft = duration; //Time in seconds
        time._name = name; //Set a name
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
