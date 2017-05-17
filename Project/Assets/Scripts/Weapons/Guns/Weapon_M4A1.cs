using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Class for the M4A1
/// </summary>
public partial class Weapon_M4A1 : Weapon_Abstract
{
#pragma warning disable 0414 //Disables the goddamn warning for the shoot bool
    bool shoot = false; //Whats this for?

    /// <summary>
    /// Use this for initialization
    /// </summary>
    protected override void Start()
    {
    }

    /// <summary>
    /// Update is called once per frame
    /// </summary>
    protected override void Update()
    {
    }

    /// <summary>
    /// Called to fire weapon; calls the super method
    /// </summary>
    public override void Fire()
    {
        base.Fire();
    }
}