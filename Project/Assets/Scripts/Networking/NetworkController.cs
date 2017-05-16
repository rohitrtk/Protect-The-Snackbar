using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NetworkController : MonoBehaviour {

    private const string VERSION = "v0.0.1";
    [SerializeField] private string _roomName = "Server01";
    [SerializeField] private string _playerPrefabName = "Player";
    [SerializeField] private Transform _spawn;

	void Start () {

        PhotonNetwork.ConnectUsingSettings(VERSION);

	}


    // called when the Photon Network is connected to with ConnectUsingSettings //So I belive this means when you connect to the PHOTON SEVRER, not another player's server
    void OnJoinedLobby()
    {
        RoomOptions roomOptions = new RoomOptions() { IsVisible = false, MaxPlayers = 4 }; //Setup options for room creation
        PhotonNetwork.JoinOrCreateRoom(_roomName, roomOptions, TypedLobby.Default); //Attempt to join a room, if none exist, make one.
    }

    // called when a room is joined by the player //So when a client connects to another client
    void OnJoinedRoom()
    {
        PhotonNetwork.Instantiate(_playerPrefabName, _spawn.position, _spawn.rotation, 0); //Spawns a player on the server, everyone can see it!
    }


}
