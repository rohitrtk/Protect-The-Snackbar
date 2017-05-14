using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Temporary GameManager (base NetworkManager off of this class)
/// </summary>
public class GameManager : MonoBehaviour
{

    /// <summary>
    /// Where the player spawns, connected to empty object
    /// </summary>
    public Transform Spawn;

    public GameObject PlayerPrefab;

    /// <summary>
    /// A copy of the player control script from the player prefab
    /// </summary>
    public Player_Control Instance;

	// Use this for initialization
	void Start ()
    {
        //Spawn a player
        Instance = PlayerPrefab.GetComponent<Player_Control>();
        Instance.Instance = Instantiate(PlayerPrefab, Spawn.position, Spawn.rotation) as GameObject;




    }
	
	// Update is called once per frame
	void Update ()
    {

        

    }
}
