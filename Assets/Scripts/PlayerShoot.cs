using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class PlayerShoot : NetworkBehaviour {

    private const string PLAYER_TAG = "Player";

    [SerializeField] private PlayerWeapon weapon;
    [SerializeField] private Camera cam;
    [SerializeField] LayerMask mask;
    [SerializeField] GameObject weaponGFX;
    [SerializeField] string weaponLayerName = "Weapon";

	// Use this for initialization
	void Start () {
        if (cam == null)
        {
            this.enabled = false;
        }

        weaponGFX.layer = LayerMask.NameToLayer(weaponLayerName);
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetButtonDown("Fire1"))
        {
            Shoot();
        }
    }

    [Client]
    void Shoot()
    {
        RaycastHit hit;
        if (Physics.Raycast(cam.transform.position,cam.transform.forward,out hit,weapon.range,mask))
        {
            if (hit.collider.tag == PLAYER_TAG)
            {
                CmdPlayerShot(hit.collider.name,weapon.damage);
            }
        }
    }

    [Command]
    void CmdPlayerShot(string playerID,int damage)
    {
        Debug.Log(playerID + " has been shot");

        Player player = GameManager.getPlayer(playerID);

        player.RpcTakeDamage(damage);
    }
}
