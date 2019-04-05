using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

[RequireComponent(typeof(Player))]
[RequireComponent(typeof(PlayerController))]
public class PlayerSetup : NetworkBehaviour {

    public Behaviour[] componentsToDisable;

    [SerializeField] string remoteLayerName = "RemotePlayer";
    [SerializeField] string dontDrawLayerName = "DontDraw";
    [SerializeField] GameObject playerGraphics;
    [SerializeField] GameObject playerUIPrefab;
    [HideInInspector] public GameObject playerUIInstance;

    // Use this for initialization
    void Start () {
        if (!isLocalPlayer)
        {
            DisableComponents();
            AssignRemoteLayer();
        }
        else
        {    
            SetLayerRecursively(playerGraphics, LayerMask.NameToLayer(dontDrawLayerName));

            playerUIInstance = Instantiate(playerUIPrefab);
            playerUIInstance.name = playerUIPrefab.name;

            PlayerUI ui = playerUIInstance.GetComponent<PlayerUI>();
            if (ui == null)
                Debug.Log("No UI");

            ui.SetController(GetComponent<PlayerController>());

            GetComponent<Player>().PlayerSetup();
        }                
	}

    private void SetLayerRecursively(GameObject obj, int newLayer)
    {
        obj.layer = newLayer;
        foreach(Transform child in obj.transform)
        {
            SetLayerRecursively(child.gameObject, newLayer);
        }
    }

    public override void OnStartClient()
    {
        base.OnStartClient();

        string netID = GetComponent<NetworkIdentity>().netId.ToString();
        Player player = GetComponent<Player>();

        GameManager.RegisterPlayer(netID, player);
    }

    void AssignRemoteLayer()
    {
        gameObject.layer = LayerMask.NameToLayer(remoteLayerName);
    }

    void DisableComponents()
    {
        for (int i = 0; i < componentsToDisable.Length; i++)
        {
            componentsToDisable[i].enabled = false;
        }
    }

    private void OnDisable()
    {
        GameManager.instance.SetSceneCamera(true);

        GameManager.UnregisterPlayer(transform.name);

        Destroy(playerUIInstance);
    }

    // Update is called once per frame
    void Update () {
		
	}
}
