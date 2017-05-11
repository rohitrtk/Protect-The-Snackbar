using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    public Transform Spawn;
    public GameObject PlayerPrefab;
    public Player_Control Instance;

	// Use this for initialization
	void Start () {
        Instance = PlayerPrefab.GetComponent<Player_Control>();
        Instance.Instance = Instantiate(PlayerPrefab, Spawn.position, Spawn.rotation) as GameObject;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
