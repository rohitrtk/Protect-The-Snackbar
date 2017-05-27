using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Class for the M4A1
/// </summary>
public class Weapon_M4A1 : Weapon_Abstract
{
    /// <summary>
    /// Use this for initialization
    /// </summary>
    protected override void Start()
    {
        NumberOfBullets = 30;
        NumberOfBulletsInGun = 30;
        Damage = 20f;

        GunAmmoText = GameObject.Find("Gun Ammo").GetComponent<Text>();
        UpdateGunAmmo();
    }

    /// <summary>
    /// Update is called once per frame
    /// </summary>
    protected override void Update()
    {
    }

    protected override void OnEnable()
    {
         //WeaponInHand = true;
    }
}