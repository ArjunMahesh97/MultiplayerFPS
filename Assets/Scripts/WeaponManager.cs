using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class WeaponManager : NetworkBehaviour {

    [SerializeField] string weaponLayerName = "Weapon";
    [SerializeField] private PlayerWeapon primaryWeapon;
    [SerializeField] private Transform weaponHolder;

    private PlayerWeapon currentWeapon;
    private WeaponGraphics currentGraphics;

	// Use this for initialization
	void Start () {
        EquipWeapon(primaryWeapon);
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public PlayerWeapon GetCurrentWeapon()
    {
        return currentWeapon;
    }

    public WeaponGraphics GetCurrentGraphics()
    {
        return currentGraphics;
    }

    void EquipWeapon(PlayerWeapon weapon){
        currentWeapon = weapon;

        GameObject weaponInst = (GameObject)Instantiate(weapon.graphics, weaponHolder.position, weaponHolder.rotation);
        weaponInst.transform.SetParent(weaponHolder);

        currentGraphics = weaponInst.GetComponent<WeaponGraphics>();
        Debug.Log(currentGraphics);
        if (currentGraphics == null)
        {
            Debug.LogError("No graphics component on weapon: " + weaponInst.name);
        }

        if (isLocalPlayer)
        {
            Util.SetLayerRecursively(weaponInst, LayerMask.NameToLayer(weaponLayerName));
        }
    }

}
