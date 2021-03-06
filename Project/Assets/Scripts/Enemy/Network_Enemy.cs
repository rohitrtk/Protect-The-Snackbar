﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// The instance of enemy on the network
/// </summary>
public class Network_Enemy : AbstractNetworkSync
{
    /// <summary>
    /// The real position of this network enemy
    /// </summary>
    private Vector3 _realPosition;

    /// <summary>
    /// The real rotation of this network enemy
    /// </summary>
    private Quaternion _realRotation;

    /// <summary>
    /// Reference to the Enemy_Basic class
    /// </summary>
    private Enemy_Basic _enemy;

    /// <summary>
    /// Use this for initialization
    /// </summary>
    protected override void Start()
    {
        if (photonView.isMine)
        {
            _enemy = GetComponent<Enemy_Basic>();
        }
    }

    /// <summary>
    /// Called once per frame
    /// </summary>
    protected override void Update()
    {
        //If I am NOT the host/owner of enemy, set its position on my client to a mix of what I think it is and what the server thinks it is
        if (!photonView.isMine)
        {
            transform.position = Vector3.Lerp(transform.position, _realPosition, Time.deltaTime * LerpSmoothing);
            transform.rotation = Quaternion.Lerp(transform.rotation, _realRotation, Time.deltaTime * LerpSmoothing);
        }
    }

    protected override void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        // Are we reading or writing to the stream?
        if (stream.isWriting) // HOST
        {
            //WRITING
            stream.SendNext(transform.position);
            stream.SendNext(transform.rotation);

        }
        else if(stream.isReading) // CLIENT
        {
            //READING
            _realPosition = (Vector3)stream.ReceiveNext();
            _realRotation = (Quaternion)stream.ReceiveNext();

        }
    }

    /// <summary>
    /// Makes an enemy take damage and if it gets too low it dies
    /// </summary>
    /// <param name="dmg"></param>
    [PunRPC]
    public void TakeDamage(float dmg) // Host is the only one who should be running this
    {
        if (photonView.isMine) // Double checking this is the owner incase the RPC is sent to the wrong client
        {
            _enemy.Health -= dmg;
            //print("Health: " + _enemy.Health);

            if (_enemy.Health <= 0)
            {
                PhotonNetwork.Destroy(gameObject); // Kill the enemy over the server
            }
        }
        else
        {
            Debug.LogError("You tried to set damage to an object you dont own! Did you make sure the RPC was only sent to the master? ");
        }
    }
}