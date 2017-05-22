using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerManager : MonoBehaviour {

    public void Spawn(string prefabName, Transform spawn) //REMEMBER TO SUMMARY THIS LATER
    {
        PhotonNetwork.Instantiate(prefabName, spawn.position, spawn.rotation, 0);
    }

    void Start()
    {
        
    }

    void Update()
    {

    }

}