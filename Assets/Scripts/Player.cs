using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

[RequireComponent(typeof(PlayerSetup))]
public class Player : NetworkBehaviour {

    [SyncVar]
    private bool isDead = false;
    public bool IsDead
    {
        get { return isDead; }
        protected set { isDead=value;}
    }

    [SerializeField] private int maxHealth = 100;
    [SerializeField] private GameObject deathEffect;
    [SerializeField] private GameObject spawnEffect;

    [SerializeField] private Behaviour[] disableOnDeath;
    [SerializeField] private GameObject[] disableObjectsOnDeath;
    

    private bool[] wasEnabled;

    [SyncVar] private int currentHealth;

    public void PlayerSetup()
    {               
        GameManager.instance.SetSceneCamera(false);
        GetComponent<PlayerSetup>().playerUIInstance.SetActive(true);
   
        CmdBroadcastNewPlayerSetup();        
    }

    [Command]
    private void CmdBroadcastNewPlayerSetup()
    {
        RpcSetupPlayerOnAllClients();
    }

    [ClientRpc]
    private void RpcSetupPlayerOnAllClients()
    {
        wasEnabled = new bool[disableOnDeath.Length];
        for (int i = 0; i < wasEnabled.Length; i++)
        {
            wasEnabled[i] = disableOnDeath[i].enabled;
        }

        SetDefaults();

    }

    public void SetDefaults() {
        IsDead = false;
        currentHealth = maxHealth;

        for(int i = 0; i < disableOnDeath.Length; i++)
        {
            disableOnDeath[i].enabled = wasEnabled[i];
        }

        for (int i = 0; i < disableObjectsOnDeath.Length; i++)
        {
            disableObjectsOnDeath[i].SetActive(true);
        }

        Collider col = GetComponent<Collider>();
        if (col != null)
        {
            col.enabled = true;
        }

        GameObject spawnEffectInstance = (GameObject)Instantiate(spawnEffect, transform.position, Quaternion.identity);
        Destroy(spawnEffectInstance, 3f);
    }

    


    [ClientRpc]
    public void RpcTakeDamage(int amount)
    {
        if (IsDead)
            return;

        currentHealth -= amount;

        Debug.Log(transform.name + " has " + currentHealth + " health");

        if (currentHealth <= 0)
        {
            Die();
        }

        
    }

    private void Die()
    {
        IsDead = true;

        for(int i = 0; i < disableOnDeath.Length; i++)
        {
            disableOnDeath[i].enabled = false;
        }

        for (int i = 0; i < disableObjectsOnDeath.Length; i++)
        {
            disableObjectsOnDeath[i].SetActive(false);
        }

        Collider col = GetComponent<Collider>();
        if (col != null)
        {
            col.enabled = false;
        }

        //Debug.Log(transform.name + " is DEAD!");

        GameObject deathEffectInstance = (GameObject)Instantiate(deathEffect, transform.position, Quaternion.identity);
        Destroy(deathEffectInstance, 3f);

        if (isLocalPlayer)
        {
            GameManager.instance.SetSceneCamera(true);
            GetComponent<PlayerSetup>().playerUIInstance.SetActive(false);
        }



        StartCoroutine(Respawn());
    }

    IEnumerator Respawn()
    {
        yield return new WaitForSeconds(GameManager.instance.matchSettings.respawnTime);

        Transform spawnPoint = NetworkManager.singleton.GetStartPosition();
        transform.position = spawnPoint.position;
        transform.rotation = spawnPoint.rotation;

        GameManager.instance.SetSceneCamera(false);
        GetComponent<PlayerSetup>().playerUIInstance.SetActive(true);

        SetDefaults();
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (!isLocalPlayer)
        {
            return;
        }
        if (Input.GetKeyDown(KeyCode.K))
        {
            RpcTakeDamage(9999999);
        }
	}
}
