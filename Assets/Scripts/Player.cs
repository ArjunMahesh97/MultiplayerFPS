using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Player : NetworkBehaviour {

    [SyncVar]
    private bool isDead = false;
    public bool IsDead
    {
        get { return isDead; }
        protected set { isDead=value;}
    }

    [SerializeField] private int maxHealth = 100;

    [SyncVar] private int currentHealth;

    private void Awake()
    {
        SetDefaults();
    }

    public void SetDefaults() {
        currentHealth = maxHealth;
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

        Debug.Log(transform.name + " is DEAD!");
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
