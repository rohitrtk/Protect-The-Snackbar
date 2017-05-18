using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Class for the M4A1
/// </summary>
public class Weapon_M4A1 : Weapon_Abstract
{
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
    public override void Fire(bool held)
    {
        base.Fire(held);
        PlaySound(held);
    }

    /// <summary>
    /// Called to play this weapons sound
    /// </summary>
    protected override void PlaySound(bool held)
    {
        if(!held)
        {
            
            
            return;
        }
    }
}