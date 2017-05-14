using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Handles player movement
/// </summary>
public partial class Player_Movement : Player_Movement_Abstract
{
    /// <summary>
    /// Called on object initialization
    /// </summary>
    protected override void Start()
    {
    }

    /// <summary>
    /// Called once per frame
    /// </summary>
    protected override void Update()
    {
        Move(); 
        Rotate();
    }

    /// <summary>
    /// Handels the movement of the player
    /// </summary>
    protected override void Move()
    {
        var horizontalMove = Input.GetAxis("Horizontal") * (_moveSpeedScale / _moveSpeed);
        var verticalMove = Input.GetAxis("Vertical") * (_moveSpeedScale / _moveSpeed);

        transform.Translate(horizontalMove, 0f, verticalMove);
    }

    /// <summary>
    /// Handles the rotation of the player
    /// </summary>
    [System.Obsolete]
    protected override void Rotate()
    {
    }
}
