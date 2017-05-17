using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Temp script to spawn an enemy whenever a player joins the game
/// </summary>
public class EnemySpawnerBasic : MonoBehaviour {

    [SerializeField] private string _enemyPrefab = "BasicEnemy";


    void OnCreatedRoom()
    {
        PhotonNetwork.Instantiate(_enemyPrefab, transform.position, transform.rotation, 0);
    }
}
