using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Basic : Enemy_Abstract
{
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
        if(!Dead)
        {
            if (Health <= 0) Dead = true;
            return;
        }

        Destroy(gameObject);
    }
}