using UnityEngine;

/// <summary>
/// Abstract class for weapons
/// </summary>
public abstract partial class Weapon_Abstract : MonoBehaviour
{
    #region Abstract Methods
    protected abstract void Start();
    protected abstract void Update();
    #endregion

    #region Methods
    public virtual void Fire()
    {
        RaycastHit hitInfo;
            Debug.DrawRay(BulletSpawn.position, BulletSpawn.forward, Color.red);
        if(Physics.Raycast(BulletSpawn.position, BulletSpawn.forward, out hitInfo, 100f))
        {
            print(hitInfo.transform.name);
        }
    }
    #endregion

    #region Getters and Setters
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
    protected int NumberOfBulletsInGun
    {
        get
        {
            return numberOfBulletsInGun;
        }

        set
        {
            numberOfBulletsInGun = value;
        }
    }
    protected float WaitTime
    {
        get
        {
            return waitTime;
        }

        set
        {
            waitTime = value;
        }
    }

    protected Transform BulletSpawn
    {
        get
        {
            return _bulletSpawn;
        }
    }

    #endregion

    #region Variables
    [SerializeField] private int numberOfBullets;
    [SerializeField] private int numberOfBulletsInGun;
    [SerializeField] private float reloadTime;
    [SerializeField] private float waitTime;
    [SerializeField] private bool hasScope;
    [SerializeField] private Transform _bulletSpawn;
    #endregion
}