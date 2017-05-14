using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Abstract class for player movement
/// </summary>
public abstract partial class Player_Movement_Abstract : MonoBehaviour
{

    #region Abstract Methods
    protected abstract void Start();
    protected abstract void Update();
    protected abstract void Move();         // Handles the movement of the player

    [System.Obsolete]
    protected abstract void Rotate();       // Handles rotating the player based on mouse input
    #endregion

    #region Variables
    [SerializeField] protected float _moveSpeed;
    [HideInInspector] protected const float _moveSpeedScale = 0.15f;

    /// <summary>
    /// Multiplier for the speed of the player
    /// </summary>
    public float MoveSpeed
    {
        get
        {
            return _moveSpeed;
        }
        set
        {
            _moveSpeed = value;
        }
    }
    #endregion
}
