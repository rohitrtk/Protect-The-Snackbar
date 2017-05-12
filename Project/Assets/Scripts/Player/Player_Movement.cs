using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Movement : Player_Movement_Abstract
{
    protected override void Start()
    {

    }

    protected override void Update()
    {
        Move();
    }

    /// <summary>
    /// Handels the movement of the player
    /// </summary>
    protected override void Move()
    {
        var verticalMove = Input.GetAxis("Vertical") * MoveSpeed;
        var horizontalMove = Input.GetAxis("Horizontal") * MoveSpeed;

        transform.Translate(horizontalMove, 0f, verticalMove);
    }
}
