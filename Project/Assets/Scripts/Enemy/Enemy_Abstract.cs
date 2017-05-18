using UnityEngine;

/// <summary>
/// Abstract class for enemies
/// </summary>
public abstract partial class Enemy_Abstract : MonoBehaviour
{
    #region Variables
    /// <summary>
    /// This enemies health
    /// </summary>
    [SerializeField] private float _health;

    /// <summary>
    /// Is this enemy dead?
    /// </summary>
    private bool _dead;
    #endregion

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
}