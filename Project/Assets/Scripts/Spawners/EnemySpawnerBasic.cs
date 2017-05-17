using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Temp script to spawn an enemy every so often, in final game it will be wave based
/// </summary>
public class EnemySpawnerBasic : MonoBehaviour {

    [SerializeField] private string _enemyPrefab = "BasicEnemy"; //Name of the enemy prefab
    [SerializeField] private float _spawnInterval = 10f; //How often should an enemy spawn in seconds
    private float _maxTime; //A copy of _spawnInterval before it starts to decrement

    void Start()
    {
        _maxTime = _spawnInterval; //Set a ref to what the origonal time was
    }

    void Update()
    {
        if (PhotonNetwork.isMasterClient)//If I am the HOST
        {
            _spawnInterval -= Time.deltaTime; //Decrement

            if (_spawnInterval <= 0)//If the timer is done
            {
                SpawnEnemy(); //Spawn an enemy
                _spawnInterval = _maxTime; //Reset the timer
            }
        }
    }


    void SpawnEnemy()
    {
        if (PhotonNetwork.isMasterClient)//If I am the HOST
        {
            PhotonNetwork.Instantiate(_enemyPrefab, transform.position, transform.rotation, 0); //Spawn and sync an enemy over the network
        }
    }
}
