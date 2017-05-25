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

    #region Methods
    /// <summary>
    /// Zombie-like following of the player for basic testing
    /// </summary>
    /// <param name="speed"></param>
    /// <param name="dampener"></param>
    protected void Follow(GameObject target, float speed = 3f)
    {
        Vector3 targetPos = target.transform.position; //Get the vector of the target
        transform.LookAt(new Vector3(targetPos.x, transform.position.y, targetPos.z)); //Look at the target
        transform.position = Vector3.MoveTowards(transform.position, targetPos, speed * Time.deltaTime); //Move towards the player
        
    }

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