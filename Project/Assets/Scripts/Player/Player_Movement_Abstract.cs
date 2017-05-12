using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Player_Movement_Abstract : MonoBehaviour
{

    #region Abstract Methods
    protected abstract void Start();
    protected abstract void Update();
    protected abstract void Move(); //Handels the movement of the player
    #endregion

    #region Variables
    [SerializeField] private float _moveSpeed;

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
