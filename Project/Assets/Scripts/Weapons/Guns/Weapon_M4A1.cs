using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Class for the M4A1
/// </summary>
public class Weapon_M4A1 : Weapon_Abstract
{
    bool shoot = false;
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

    public override void Fire()
    {
        if (shoot) return;
        shoot = true;

        // If we decide to use bullets
        //Bullet_Basic bulletInstance = Instantiate(BulletInstance, BulletSpawn.position, BulletSpawn.rotation) 
        //    as Bullet_Basic;

        // If we decide to use ray casts
    }
}