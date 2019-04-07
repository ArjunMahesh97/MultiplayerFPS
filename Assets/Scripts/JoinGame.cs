using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using UnityEngine.Networking.Match;
using System;

public class JoinGame : MonoBehaviour {

    List<GameObject> roomList = new List<GameObject>();

    [SerializeField] Text status;
    [SerializeField] private GameObject roomListItemPrefab;
    [SerializeField] private Transform roomListParent;

    private NetworkManager networkManager;

	// Use this for initialization
	void Start () {
        networkManager = NetworkManager.singleton;
        if (networkManager.matchMaker == null)
        {
            networkManager.StartMatchMaker();
        }

        RefreshRoom();

    }

    public void RefreshRoom()
    {
        networkManager.matchMaker.ListMatches(0, 20, "", true, 0, 0 OnMatchList);
        status.text = "Loading...";
    }

    public void OnMatchList(bool success, string extendedInfo, List<MatchInfoSnapshot> matchList)
    {
        status.text = "";

        if (!success || matchList == null)
        {
            status.text = "Couldn't get room list.";
            return;
        }

        ClearRoomList();

    }

    private void ClearRoomList()
    {
        for(int i = 0; i < roomList.Count; i++)
        {
            Destroy(roomList[i]);
        }
        roomList.Clear();
        foreach(MatchInfoSnapshot match in matchList.matches)
        {
            GameObject roomListItem = Instantiate(roomListItemPrefab);
            roomListItem.transform.SetParent(roomListParent);
        }
    }

    // Update is called once per frame
    void Update () {
		
	}
}
