using System.Collections;
using UnityEngine;

/// <summary>
/// Abstract class for weapons
/// </summary>
public abstract partial class Weapon_Abstract : MonoBehaviour
{
    #region Variables
    [SerializeField] private int numberOfBullets;
    [SerializeField] private int numberOfBulletsInGun;
    [SerializeField] private float reloadTime;
    [SerializeField] private float _attackTime;
    [SerializeField] private bool hasScope;
    [SerializeField] private float damage = 15f;
    [SerializeField] private Transform _bulletSpawn;
    [SerializeField] private Camera _playerCam;
    [SerializeField] private Weapon_Sounds _weaponSound;
    [SerializeField] private ParticleSystem _muzzleFlash;

    /// <summary>
    /// The time to wait before the next gunshot is fired
    /// </summary>
    private float waitTime = 0f;

    #endregion

    #region Abstract Methods
    protected abstract void Start();
    protected abstract void Update();
    #endregion

    #region Methods
    /// <summary>
    /// Handles the player's firing logic and setting raycasts.
    /// </summary>
    public virtual void Fire()
    {
        waitTime = Time.time + _attackTime;

        RaycastHit hitInfo;

        // Sets the middle of the players view as our start point
        Vector3 rayStart = PlayerCam.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, 0)); 

        Debug.DrawRay(rayStart, PlayerCam.transform.forward * 5000f, Color.red);

        if (Physics.Raycast(rayStart, PlayerCam.transform.forward, out hitInfo, 5000f))
        {
            Transform go = hitInfo.transform;

            if (go.tag.Equals("Enemy"))
            {
                go.GetComponent<PhotonView>().RPC("TakeDamage", PhotonTargets.MasterClient, damage);
            }
        }
        _muzzleFlash.Play();
        // Plays the weapon sound
        _weaponSound.PlayShotSound();
    }

    /// <summary>
    /// Calls the Fire method when possible based on time
    /// </summary>
    public virtual void AttemptToFire()
    {
        if (Time.time > waitTime && Time.timeScale > 0) Fire();
    }
    #endregion

    #region Getters and Setters
    /// <summary>
    /// The number of bullets the gun can hold (Ammo count)
    /// </summary>
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

    /// <summary>
    /// The number of bullets currently in the gun (Current ammo count)
    /// </summary>
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

    /// <summary>
    /// The amount of time in seconds it takes to reaload the gun
    /// </summary>
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

    /// <summary>
    /// Does this gun have a scope?
    /// </summary>
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

    /// <summary>
    /// Time between shots firing
    /// </summary>
    protected float AttackTime
    {
        get
        {
            return _attackTime;
        }

        set
        {
            _attackTime = value;
        }
    }

    /// <summary>
    /// Returns players camera
    /// </summary>
    protected Camera PlayerCam
    {
        get
        {
            return _playerCam;
        }
    }

    /// <summary>
    /// Returns the muzzle flash particle system for the gun
    /// </summary>
    protected ParticleSystem MuzzleFlash
    {
        get
        {
            return _muzzleFlash;
        }
    }
    #endregion
}