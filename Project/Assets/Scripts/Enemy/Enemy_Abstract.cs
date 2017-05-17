using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy_Abstract : MonoBehaviour
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
}