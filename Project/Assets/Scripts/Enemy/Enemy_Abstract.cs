using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract partial class Enemy_Abstract : MonoBehaviour
{
    #region Abstract Methods
    protected abstract void Start();
    protected abstract void Update();
    #endregion

    #region Getters and Setters
    public float Health
    {
        get
        {
            return _health;
        }

        set
        {
            _health = value;
        }
    }
    public bool Dead
    {
        get
        {
            return _dead;
        }

        set
        {
            _dead = value;
        }
    }
    #endregion

    #region Variables
    [SerializeField] private float _health;
    private bool _dead;
    #endregion

    #region Methods

    /// <summary>
    /// Takes damage away from Health
    /// </summary>
    /// <param name="damage"></param>
    public virtual void CalculateDamage(float damage)
    {
        _health -= damage;
        print(damage + "|" + _health);
    }

    /// <summary>
    /// Takes damage, changes it based on range and subtracts from health
    /// </summary>
    /// <param name="damage"></param>
    /// <param name="range"></param>
    public virtual void CalculateDamage(float damage, float range)
    {
        throw new NotImplementedException();
    }
    #endregion
}