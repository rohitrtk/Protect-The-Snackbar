using UnityEngine;

[System.Obsolete]
public abstract partial class Bullet_Abstract : MonoBehaviour
{
    #region Abstract Methods
    protected abstract void Start();
    protected abstract void Update();
    #endregion

    #region Getters and Setters
    public float Damage
    {
        get
        {
            return _damage;
        }

        set
        {
            _damage = value;
        }
    }
    #endregion

    #region Methods
    #endregion

    #region Variables
    [SerializeField] private float _damage;
    #endregion
}