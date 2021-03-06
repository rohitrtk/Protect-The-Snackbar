﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Networked player class, responsible for enabling this clients Player_Handler script
/// </summary>
public partial class NetworkPlayer : AbstractNetworkSync
{
    /// <summary>
    /// Gameobject with reference to the scenes main camera
    /// </summary>
    [SerializeField] private GameObject _myCamera;

    private bool _isAlive = true; //If the player is alive, may have to remove later for networking reasons
    private Vector3 _position; // Sever relative position
    private Quaternion _rotation; // Server relative rotation


    protected override void Start()
    {
        if (photonView.isMine) //If this is my client's instance
        { 
            _myCamera.SetActive(true); //Set my camera to the main one.
            GetComponent<Player_Handler>().enabled = true;//Allow me to use the player controls script
        }
    }

    protected override void Update()
    {
        if (_isAlive && !photonView.isMine)
        {
            //Set a smoothed out Rotation and position as live so its not exactly what host is saying but it keeps the game from looking gittery.
            transform.position = Vector3.Lerp(transform.position, _position, Time.deltaTime * LerpSmoothing);
            transform.rotation = Quaternion.Lerp(transform.rotation, _rotation, Time.deltaTime * LerpSmoothing);
        }
    }

    protected override void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        //Are we reading our writing to the stream
        if (stream.isWriting)
        {
            //LAYOUT MATTERS!

            //WRITING
            stream.SendNext(transform.position);
            stream.SendNext(transform.rotation);
        }
        else 
        {
            //READING
            _position = (Vector3)stream.ReceiveNext();
            _rotation = (Quaternion)stream.ReceiveNext();
        }
    }

}
