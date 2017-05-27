using System.Collections;
using UnityEngine;
using UnityEngine.UI;


/// <summary>
/// Abstract class for weapons
/// </summary>
public abstract partial class Weapon_Abstract : MonoBehaviour
{
    #region Variables
    // Reload variables
    // Number of bullets the gun can hold
    [SerializeField] private int numberOfBullets;

    // The number of bullets currently in the gun
    [SerializeField] private int numberOfBulletsInGun;
    [SerializeField] private float reloadTime;
    private bool _reloading;

    // Weapon damage default at 10 (Override in actual weapon class)
    [SerializeField] private float _damage = 10f;

    // Time between shots for this gun
    [SerializeField] private float _attackTime;

    [SerializeField] private bool hasScope;
    [SerializeField] private Camera _playerCam;
    [SerializeField] private Weapon_Sounds _weaponSound;
    [SerializeField] private ParticleSystem _muzzleFlash;

    private bool weaponInHand = false;

    // Reference to the canvas text object which will display ammmo
    [SerializeField] private Text _gunAmmoText;

    // The time to wait before the next gunshot is fired
    private float waitTime = 0f;

    #endregion

    #region Abstract Methods
    protected abstract void Start();
    protected abstract void Update();
    protected abstract void OnEnable();
    #endregion

    #region Methods
    /// <summary>
    /// Handles the player's firing logic and setting raycasts.
    /// </summary>
    protected void Fire()
    {
        if (_reloading) return;

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
                go.GetComponent<PhotonView>().RPC("TakeDamage", PhotonTargets.MasterClient, _damage);
            }
        }

        numberOfBulletsInGun--;
        UpdateGunAmmo();

        // Plays the muzzle flash
        _muzzleFlash.Play();

        // Plays the weapon sound
        _weaponSound.PlayShotSound();
    }

    /// <summary>
    /// Calls the Fire method when possible based on time
    /// </summary>
    public virtual void AttemptToFire()
    {
        if (NumberOfBulletsInGun <= 0) AttemptReload();
        else if (Time.time > waitTime && Time.timeScale > 0 && !_reloading) Fire();
    }

    /// <summary>
    /// Displays the ammo of the gun to text
    /// </summary>
    protected virtual void UpdateGunAmmo()
    {
        GunAmmoText.text = NumberOfBulletsInGun + "/" + NumberOfBullets;
    }

    /// <summary>
    /// Plays the reload animation and waits for it to finish,
    /// then updates the number of bullets in the gun
    /// </summary>
    /// <returns></returns>
    // TODO: Add a reload animation and set the reloadTime equal to the animation time
    protected virtual IEnumerator Reload()
    {
        _reloading = true;

        yield return new WaitForSecondsRealtime(reloadTime);

        NumberOfBulletsInGun = 30;
        UpdateGunAmmo();

        _reloading = false;
    }

    /// <summary>
    /// Will be called inside the Player_Handler class to attempt a reload
    /// </summary>
    public virtual void AttemptReload()
    {
        if (NumberOfBulletsInGun == NumberOfBullets) return;

        StartCoroutine(Reload());
    }
    #endregion

    #region Getters and Setters
    /// <summary>
    /// The number of bullets the gun can hold (Ammo count)
    /// </summary>
    public int NumberOfBullets
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
    public int NumberOfBulletsInGun
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

    protected float Damage
    {
        set
        {
            _damage = value;
        }
        get
        {
            return _damage;
        }
    }

    public bool WeaponInHand
    {
        get
        {
            return weaponInHand;
        }

        set
        {
            weaponInHand = value;
        }
    }

    protected Text GunAmmoText
    {
        get
        {
            return _gunAmmoText;
        }
        set
        {
            _gunAmmoText = value;
        }
    }

    public bool Reloading
    {
        get
        {
            return _reloading;
        }
        set
        {
            _reloading = value;
        }
    }
    #endregion
}