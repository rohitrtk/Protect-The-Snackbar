using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Handels spawning waves, pickup spawns, enemy difficulty by wave. May get merged to a bigger picture gameController
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
	void FixedUpdate ()
    {
        if (PhotonNetwork.isMasterClient)//If I am the host aka the only one who should be using this script
        {
            switch (_wave)//For every wave
            {   
                case 0: //Warmup, 10 second countdown
                    #region Literally just a countdown
                    if(time == null)//No timer set yet
                    {
                        time = new Timer(10);
                    }
                    else
                    {
                        if (time.Complete())
                        {
                            time = null; //Empty it for possible later use
                            NetWaveUp();
                        }
                    }
                    break;
                #endregion

                default: // Will work for all rounds, based spawn of a math function
                    #region Wave 1
                    if(time == null) //If no timer exists
                    {
                        time = new Timer(2);//Make a timer countdown from 2
                        counter++;
                    }
                    else
                    {
                        if (time.Complete())//When the timer has finished counting
                        {
                            int rand = Random.Range(0, _spawnLocations.Length); // Random int.
                            Spawn("BasicEnemy", _spawnLocations[rand]);// Spawn a basic enemy at a random spawner();
                            time = null; //Reset the timer
                        }
                    }

                    // TODO: Need to make it so the counter only goes up when all the enemies are dead
                    if(counter > WaveFunction.GetEnemiesForRound(_wave)) //If we have the desired amount of enemies spawned
                    {
                        print("Desired number of enemies has been reached!");
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

public struct WaveFunction
{
    public static int GetEnemiesForRound(int round)
    {
        return Mathf.RoundToInt(Mathf.Pow(round, 2) + 10);
    }
}