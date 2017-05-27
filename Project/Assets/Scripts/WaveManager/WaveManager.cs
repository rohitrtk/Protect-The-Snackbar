using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Handles spawning waves, pickup spawns, enemy difficulty by wave. May get merged to a bigger picture gameController
/// </summary>
public class WaveManager : MonoBehaviour    //Maybe have a master controller that turns this script off if not host
{

    /// <summary>
    /// Current round
    /// </summary>
    [SerializeField] private int _wave = 0; //wave is the wave that the player(s) are currently on. 0 is a warmup round/get ready
    
    /// <summary>
    /// ref to SpawnerManager script
    /// </summary>
    [SerializeField] private EnemySpawnerBasic _spawner;

    [SerializeField] private Text _waveText;

    /// <summary>
    /// Game object containing all the enemy spawners
    /// </summary>
    [SerializeField] private GameObject _enemySpawnLocations;

    /// <summary>
    /// Used for simple counting down the line
    /// </summary>
    private int counter;

    Transform[] _spawnLocations;

    private Timer time;

    // Use this for initialization
    //TODO: Change this for networking
    void Start ()
    { 
        _spawnLocations = _enemySpawnLocations.GetComponentsInChildren<Transform>();
        _waveText.text = "Wave " + _wave;

    }
	
	// Update is called once per frame
    // How come using FixedUpdate instead of LateUpdate?
	void FixedUpdate ()
    {
        // If I am the host aka the only one who should be using this script
        if (PhotonNetwork.isMasterClient)
        {
            // TODO: Replace timer spawn system with a coroutine system, also can replace the switch 
            // for an if statement since spawning is now based off of a math function instead of static numbers
            switch (_wave)//For every wave
            {
                // Warmup, 10 second countdown
                case 0:
                    #region Literally just a countdown
                    if(time == null)
                    {
                        time = new Timer(10);
                        return;
                    }

                    if (!time.Complete()) return;
                    
                    NetWaveUp();
                    break;
                #endregion
                
                default:
                    // Will work for all rounds, based spawn off of a math function (See the struct at the bottom of the script)
                    #region Wave X
                    if (time == null) //If no timer exists
                    {
                        time = new Timer(2);//Make a timer countdown from 2
                        counter++;
                        return;
                    }

                    if (time.Complete())//When the timer has finished counting
                    {
                        int rand = Random.Range(0, _spawnLocations.Length); // Random int.
                        Spawn("BasicEnemy", _spawnLocations[rand]);// Spawn a basic enemy at a random spawner();
                        time = null; //Reset the timer
                    }

                    // TODO: Need to make it so the counter only goes up when all the enemies are dead
                    if (counter > WaveFunctions.GetEnemiesForRound(_wave)) //If we have the desired amount of enemies spawned
                    {
                        print("Wave " + _wave + " completed.\nStarting next wave.");
                        counter = 0; //Reset counter for other waves
                        NetWaveUp(); //Go to the next wave
                    }
                    #endregion
                    break;
            }
        }
	}

    /// <summary>
    /// Sets the wave up by one and sets the HUD's info -- MAY BE REPLACED LATER
    /// </summary>
    [PunRPC]
    private void WaveUp()
    {
        _wave++; //Goto the next wave
        _waveText.text = "Wave " + _wave; //Sets the HUD's 'Wave: ' string
    }

    /// <summary>
    /// Tells everyone in the room to increase the wave number by one
    /// </summary>
    private void NetWaveUp()
    {
        GetComponent<PhotonView>().RPC("WaveUp", PhotonTargets.All); //Tell the network to go up a wave
    }

    public void Spawn(string prefabName, Transform spawn) //REMEMBER TO SUMMARY THIS LATER
    {
        PhotonNetwork.Instantiate(prefabName, spawn.position, spawn.rotation, 0);
    }

    /*
    -------------------------
    | TODO: Connor's notes: |
    -------------------------

     -Maybe use a PUNRPC to tell everyone to increment the round
     -Add a cooldown between rounds for the user to chill out
     -
     -
    */
}

/// <summary>
/// Contains functions for wave spawning
/// </summary>
public struct WaveFunctions
{
    public static int GetEnemiesForRound(int round)
    {
        return Mathf.RoundToInt(Mathf.Pow(round, 2) + 10);
    }
}