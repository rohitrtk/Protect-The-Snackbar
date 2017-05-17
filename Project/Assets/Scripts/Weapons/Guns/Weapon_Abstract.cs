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
    /// <summary>
    /// Handles the player's firing logic and setting raycasts.
    /// </summary>
    public virtual void Fire()
    {
        RaycastHit hitInfo;
        Vector3 rayStart = PlayerCam.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, 0)); //Sets the middle of the players view as our start point

        Debug.DrawRay(rayStart, PlayerCam.transform.forward*5000f, Color.red);

        if (Physics.Raycast(rayStart, PlayerCam.transform.forward, out hitInfo, 5000f))
        {
            Transform go = hitInfo.transform;
            //print(hitInfo.transform.name);

            if(go.tag.Equals("Enemy"))
            {
                go.GetComponent<PhotonView>().RPC("TakeDamage", PhotonTargets.MasterClient, 2f);
            }
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

    protected Camera PlayerCam
    {
        get
        {
            return _playerCam;
        }
    }

    #endregion

    #region Variables
    [SerializeField] private int numberOfBullets;
    [SerializeField] private int numberOfBulletsInGun;
    [SerializeField] private float reloadTime;
    [SerializeField] private float waitTime;
    [SerializeField] private bool hasScope;
    [SerializeField] private Camera _playerCam;
    #endregion
}