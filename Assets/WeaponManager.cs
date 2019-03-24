using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class WeaponManager : NetworkBehaviour {

    [SerializeField] string weaponLayerName = "Weapon";
    [SerializeField] private PlayerWeapon primaryWeapon;

    private PlayerWeapon currentWeapon;

	// Use this for initialization
	void Start () {
        EquipWeapon(primaryWeapon);
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    void EquipWeapon(PlayerWeapon weapon){
        currentWeapon = weapon;
    }

    public PlayerWeapon GetCurrentWeapon()
    {
        return currentWeapon;
    }
}
