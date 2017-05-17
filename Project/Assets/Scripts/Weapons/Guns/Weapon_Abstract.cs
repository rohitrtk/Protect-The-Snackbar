using UnityEngine;

/// <summary>
/// Abstract class for guns
/// </summary>
public abstract partial class Weapon_Abstract : MonoBehaviour
{

    #region Variables
    [SerializeField] private int numberOfBullets;
    [SerializeField] private int numberOfBulletsInGun;
    [SerializeField] private float reloadTime;
    [SerializeField] private float waitTime;
    [SerializeField] private bool hasScope;
    [SerializeField] private Transform _bulletSpawn;
    #endregion

    #region Abstract Methods
    protected abstract void Start();
    protected abstract void Update();
    #endregion

    #region Methods
    /// <summary>
    /// Called to cast a ray forward from the players BulletSpawn object
    /// </summary>
    public virtual void Fire()
    {
        // Holds information for the object which is hit by the ray cast
        RaycastHit hitInfo;

        // Debug draw ray
        Debug.DrawRay(BulletSpawn.position, BulletSpawn.forward, Color.red);

        // Cast the ray
        if (Physics.Raycast(BulletSpawn.position, BulletSpawn.forward, out hitInfo, 5000f))
        {
            // Store the hit objects transform
            Transform go = hitInfo.transform;
            //print(hitInfo.transform.name);

            // If the object hit has a tag the same as Enemy
            if(go.tag.Equals("Enemy"))
            {
                // Get the enemy component from that object
                Enemy_Abstract ec = go.GetComponent<Enemy_Abstract>();
                if(!ec)
                {
                    print("The enemy that was just hit does not have an Enemy_Abstract script!");
                    return;
                }

                // Deal damage to the enemy
                ec.CalculateDamage(50);
            }
        }
    }
    #endregion

    #region Getters and Setters
    /// <summary>
    /// The number of bullets the gun can hold (Ammo)
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
    /// The number of bullets in the gun at a given moment
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
    /// The time in seconds it takes to reload the gun
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
    /// Does this weapon have a scope attached?
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
    /// Time in seconds beetween shots
    /// </summary>
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

    /// <summary>
    /// The transform for which the ray is cast from
    /// </summary>
    protected Transform BulletSpawn
    {
        get
        {
            return _bulletSpawn;
        }
    }

    #endregion
}