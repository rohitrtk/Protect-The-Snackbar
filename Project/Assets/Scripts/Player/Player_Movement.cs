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

    protected override void Move()
    {
        var verticalMove = Input.GetAxis("Vertical");
        var horizontalMove = Input.GetAxis("Horizontal");

        transform.position = new Vector3(verticalMove, 0f, horizontalMove);
    }
}
