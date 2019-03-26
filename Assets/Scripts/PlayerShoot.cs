using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

[RequireComponent(typeof(WeaponManager))]
public class PlayerShoot : NetworkBehaviour {

    private const string PLAYER_TAG = "Player";
    private PlayerWeapon currentWeapon;
    private WeaponManager weaponManager;

    [SerializeField] private Camera cam;
    [SerializeField] LayerMask mask;


	// Use this for initialization
	void Start () {
        if (cam == null)
        {
            this.enabled = false;
        }
        weaponManager = GetComponent<WeaponManager>();
    }

    // Update is called once per frame
    void Update () {
        currentWeapon = weaponManager.GetCurrentWeapon();

        if (currentWeapon.fireRate <= 0)
        {
            if (Input.GetButtonDown("Fire1"))
            {
                Shoot();
            }
        }
        else
        {
            if (Input.GetButtonDown("Fire1"))
            {
                InvokeRepeating("Shoot",0f,1f/currentWeapon.fireRate);
            }else if (Input.GetButtonUp("Fire1"))
            {
                CancelInvoke("Shoot");
            }
        }
    }

    [Client]
    void Shoot()
    {

        if (!isLocalPlayer)
        {
            return;
        }

        CmdOnShoot();

        RaycastHit hit;
        if (Physics.Raycast(cam.transform.position,cam.transform.forward,out hit, currentWeapon.range,mask))
        {
            if (hit.collider.tag == PLAYER_TAG)
            {
                CmdPlayerShot(hit.collider.name, currentWeapon.damage);
            }

            CmdonHit(hit.point, hit.normal);
        }
    }

    [Command]
    void CmdOnShoot()
    {
        RpcShootEffect();
    }

    [ClientRpc]
    void RpcShootEffect()
    {
        weaponManager.GetCurrentGraphics().muzzleFlash.Play();
    }

    [Command]
    void CmdPlayerShot(string playerID,int damage)
    {
        Debug.Log(playerID + " has been shot");

        Player player = GameManager.getPlayer(playerID);

        player.RpcTakeDamage(damage);
    }

    [Command]
    void CmdonHit(Vector3 pos, Vector3 normal)
    {
        GameObject hitEffect = (GameObject)Instantiate(weaponManager.GetCurrentGraphics().hitEffectPrefab, pos, Quaternion.LookRotation(normal));
        Destroy(hitEffect,0.5f);
    }

    [ClientRpc]
    void RpcHitEffect(Vector3 pos, Vector3 normal)
    {
        
    }


}
