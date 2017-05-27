using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon_Inventory : MonoBehaviour
{ 
    [SerializeField] private Weapon_Abstract[] weaponsInHand;

    private void Start ()
    {
        WeaponsInHand = GetComponentsInChildren<Weapon_Abstract>();
        WeaponsInHand[0].WeaponInHand = true;
	}
	
	private void Update ()
    {
    }

    public Weapon_Abstract[] WeaponsInHand
    {
        get
        {
            return weaponsInHand;
        }

        set
        {
            weaponsInHand = value;
        }
    }
}