using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Abstract class for weapons
/// </summary>
public abstract class Weapon_Abstract : MonoBehaviour
{
    #region Abstract Methods
    protected abstract void Start();
    protected abstract void Update();
    #endregion

    #region Methods
    protected int NumberOfBullets
    {
        get
        {
            return numberOfBullets;
        }
        set
        {
            numberOfBullets = value;
        }
    }
    protected float ReloadTime
    {
        get
        {
            return reloadTime;
        }
        set

        {
            reloadTime = value;
        }
    }
    protected bool HasScope
    {
        get
        {
            return hasScope;
        }
        set
        {
            hasScope = value;
        }
    }
    #endregion

    #region Variables
    private int numberOfBullets;
    private float reloadTime;
    private bool hasScope;
    #endregion
}