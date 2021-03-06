﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    public static GameManager instance;

    public MatchSettings matchSettings;

    [SerializeField] private GameObject sceneCamera;

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogError("multiple gamemanager");
        }
        else
        {
            instance = this;
        }
    }

    public void SetSceneCamera(bool isActive)
    {
        if (sceneCamera == null)
        {
            return;
        }

        sceneCamera.SetActive(isActive);
    }



    #region PlayerTracking
    private static Dictionary<string, Player> players = new Dictionary<string, Player>();

    private const string PLAYER_ID_PREFIX = "Player ";

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public static void RegisterPlayer(string netID,Player player)
    {
        string playerID = PLAYER_ID_PREFIX + netID;
        players.Add(playerID, player);
        player.transform.name = playerID;
    }

    public static void UnregisterPlayer(string playerID)
    {
        players.Remove(playerID);
    }

    public static Player getPlayer(string playerID)
    {
        return players[playerID];
    }

    /*   private void OnGUI()
       {
           GUILayout.BeginArea(new Rect(200,200, 200, 500));
           GUILayout.BeginVertical();

           foreach(string playerID in players.Keys)
           {
               GUILayout.Label(playerID + "-" + players[playerID].transform.name);
           }

           GUILayout.EndVertical();
           GUILayout.EndArea();
       }
   */
    #endregion

}
