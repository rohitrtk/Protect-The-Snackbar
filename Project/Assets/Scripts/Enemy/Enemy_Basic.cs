﻿using UnityEngine;

/// <summary>
/// The basic enemy class, extends from the Enemy_Abstract class
/// </summary>
public partial class Enemy_Basic : Enemy_Abstract
{
    /// <summary>
    /// Holds the model for the enemy as a gameobject
    /// </summary>
    [SerializeField] private GameObject _model;

    /// <summary>
    /// Use this for initialization
    /// </summary>
    protected override void Start()
    {
        Dead = false;
    }

    /// <summary>
    /// Update is called once per frame
    /// </summary>
    protected override void Update()
    {
        // If the health is equal to or less than 0, this enemy is dead
        if(Health <= 0)
        {
            Dead = true;
        }

        GameObject closestPlayer = null;

        foreach (GameObject player in GameObject.FindGameObjectsWithTag("MultiPlayer"))
        {
            if (closestPlayer == null) closestPlayer = player;
            if(Vector3.Distance(transform.position, closestPlayer.transform.position) < Vector3.Distance(transform.position, player.transform.position))
            {
                closestPlayer = player;
            }
        }

        Follow(closestPlayer);

        // Inverted if statement to reduce nesting
        if (!Dead) return;

    }
}