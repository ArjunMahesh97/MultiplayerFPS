using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class WeaponManager : NetworkBehaviour {

    [SerializeField] string weaponLayerName = "Weapon";
    [SerializeField] private PlayerWeapon primaryWeapon;
    [SerializeField] private Transform weaponHolder;

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

        GameObject weaponInst = Instantiate(weapon.graphics, weaponHolder.position, weaponHolder.rotation);
        weaponInst.transform.SetParent(weaponHolder);
        if (isLocalPlayer)
        {
            weaponInst.layer = LayerMask.NameToLayer(weaponLayerName);
        }
    }

    public PlayerWeapon GetCurrentWeapon()
    {
        return currentWeapon;
    }
}
