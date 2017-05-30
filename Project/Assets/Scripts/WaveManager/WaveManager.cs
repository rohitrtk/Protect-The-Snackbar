using System.Collections;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Handles spawning waves, pickup spawns, enemy difficulty by wave. May get merged to a bigger picture gameController
/// </summary>
public class WaveManager : MonoBehaviour    //Maybe have a master controller that turns this script off if not host
{
    /// <summary>
    /// Current round/wave the player(s) are currently on
    /// </summary>
    [SerializeField] private int _wave = 0;
    
    /// <summary>
    /// Ref to SpawnerManager script
    /// </summary>
    [SerializeField] private EnemySpawnerBasic _spawner;

    /// <summary>
    /// Representation of the wave number on players GUI
    /// </summary>
    [SerializeField] private Text _waveText;

    /// <summary>
    /// Game object containing all the enemy spawners
    /// </summary>
    [SerializeField] private GameObject _enemySpawnLocations;

    /// <summary>
    /// Spawn locations of enemies, filled through _enemySpawnLocations
    /// </summary>
    Transform[] _spawnLocations;

    /// <summary>
    /// Timer object
    /// </summary>
    private Timer time;

    /// <summary>
    /// What phase in the game is it (Warmup, RoundPlay, RoundWait, etc)
    /// </summary>
    private enum Phase { Warmup, Wait, Play, Pause};

    /// <summary>
    /// The games current phase
    /// </summary>
    private Phase _currentPhase;

    /// <summary>
    /// Stops coroutine SpawnLoop from being run every frame
    /// </summary>
    private bool run = false;

    // Use this for initialization
    //TODO: Change this for networking
    void Start ()
    {
        _spawnLocations = _enemySpawnLocations.GetComponentsInChildren<Transform>();
        _waveText.text = "Wave " + _wave;

        _currentPhase = Phase.Warmup;
    }
	
	// LateUpdate is called after Update
	void FixedUpdate ()
    {
        // If I am the host aka the only one who should be using this script
        if (PhotonNetwork.isMasterClient)
        {
            if(!run)
            {
                StartCoroutine(SpawnLoop());
                run = true;
            }
        }
	}

    /// <summary>
    /// Sets the wave up by one and sets the HUD's info -- MAY BE REPLACED LATER
    /// </summary>
    [PunRPC] private void WaveUp()
    {
        //Goto the next wave
        _wave++;

        _waveText.text = "Wave " + _wave; 
        
        // Sets the HUD's 'Wave: ' string
        _currentPhase = Phase.Play;
    }

    /// <summary>
    /// Tells everyone in the room to increase the wave number by one
    /// </summary>
    private void NetWaveUp()
    {
        GetComponent<PhotonView>().RPC("WaveUp", PhotonTargets.All); // Tell the network to go up a wave
    }

    /// <summary>
    /// Used to spawn a prefab of type name and at transform spawn
    /// </summary>
    /// <param name="prefabName"></param>
    /// <param name="spawn"></param>
    /// <returns></returns>
    public GameObject Spawn(string prefabName, Transform spawn)
    {
        return PhotonNetwork.Instantiate(prefabName, spawn.position, spawn.rotation, 0);
    }

    /// <summary>
    /// Used to spawn an Abstract Enemy of type name and at transform spawn
    /// </summary>
    /// <param name="spawn"></param>
    /// <param name="prefabName"></param>
    /// <returns></returns>
    public Enemy_Abstract Spawn(Transform spawn, string prefabName)
    {
        return PhotonNetwork.Instantiate(prefabName, spawn.position, spawn.rotation, 0).GetComponent<Enemy_Abstract>();
    }

    /// <summary>
    /// Is the primary loop in which waves/rounds are handled
    /// </summary>
    /// <returns></returns>
    private IEnumerator SpawnLoop()
    {
        print("Entered spawn loop | " + _currentPhase);
        yield return new WaitForSeconds(0.5f);

        if(_currentPhase == Phase.Pause)        yield return StartCoroutine(RoundPause());
        else if(_currentPhase == Phase.Warmup)  yield return StartCoroutine(WarmpUp()); 

        yield return StartCoroutine(RoundPlay());
        yield return StartCoroutine(RoundWait());

        // Restarts this function to continue game loop
        StartCoroutine(SpawnLoop());
    }

    /// <summary>
    /// Should only be called at the start of a game where the players should be warming up
    /// </summary>
    /// <returns></returns>
    private IEnumerator WarmpUp()
    {
        print("Warm Up | " + _currentPhase);
        yield return new WaitForSecondsRealtime(Wave.WarmupTime);
        NetWaveUp();
    }

    /// <summary>
    /// Let's the player cool down between rounds (Wait between rounds function)
    /// </summary>
    /// <returns></returns>
    private IEnumerator RoundWait()
    {
        print("Round Wait | " + _currentPhase);
        yield return new WaitForSecondsRealtime(Wave.RoundWaitTime);
        NetWaveUp();
    }

    /// <summary>
    /// Responsible for spawning enemies and waits until all enemies die before ending
    /// </summary>
    /// <returns></returns>
    private IEnumerator RoundPlay()
    {
        print("Round Play | " + _currentPhase);
        var numberOfEnemies = Wave.GetEnemiesForRound(_wave);

        // Array of Enemy_Abstracts to store the spawned enemies
        Enemy_Abstract[] enemies = new Enemy_Abstract[numberOfEnemies];

        // Counter to track enemy count
        int counter = 0;

        while (counter < numberOfEnemies || !Wave.CheckEnemies(enemies))
        {
            if (counter < numberOfEnemies)
            {
                // Random int which will be used to spawn enemies at a random spawner
                int rand = UnityEngine.Random.Range(0, _spawnLocations.Length);

                // Spawn a basic enemy at a random spawner
                enemies[counter] = Spawn(_spawnLocations[rand], "BasicEnemy");

                yield return new WaitForSecondsRealtime(Wave.SpawnWaitTime);
                counter++;

                // Continue to the next iteration of the loop
                continue;
            }

            // Need this to stop infinite loop
            yield return new WaitForSeconds(0.1f);
        }

        _currentPhase = Phase.Wait;
        yield return null;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    private IEnumerator RoundPause()
    {
        yield return null;
    }
}

/// <summary>
/// Contains functions and variables for wave spawning
/// </summary>
public struct Wave
{
    /// <summary>
    /// Time the player has to warm up
    /// </summary>
    public const int WarmupTime = 10;
    
    /// <summary>
    /// Time player waits between rounds of enemies spawning
    /// </summary>
    public const int RoundWaitTime = 5;

    /// <summary>
    /// Time between spawning enemies
    /// </summary>
    public const int SpawnWaitTime = 1;

    /// <summary>
    /// The base number of enemies to spawn
    /// </summary>
    private const int _BaseNumberOfEnemies = 8;

    /// <summary>
    /// Returns the number of enemies needed for an int round
    /// </summary>
    /// <param name="round"></param>
    /// <returns></returns>
    public static int GetEnemiesForRound(int round)
    {
        // Return round^2 + base
        return Mathf.RoundToInt(Mathf.Pow(round, 2) + _BaseNumberOfEnemies);
    }

    /// <summary>
    /// Returns true if all of the enemies in an array are null
    /// </summary>
    /// <param name="enemies"></param>
    /// <returns></returns>
    public static bool CheckEnemies(Enemy_Abstract[] enemies)
    {
        // Foreach Enemy_Abstract in enemies
        foreach(var e in enemies)
        {
            if (e != null) return false;
        }

        return true;
    }
}