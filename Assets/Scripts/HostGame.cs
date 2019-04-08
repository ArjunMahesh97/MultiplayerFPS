using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class HostGame : MonoBehaviour {

    [SerializeField] private uint roomSize = 6;

    private string roomName;
    private NetworkManager networkManager;

	// Use this for initialization
	void Start () {
        networkManager = NetworkManager.singleton;
        if (networkManager.matchMaker == null)
        {
            networkManager.StartMatchMaker();
        }
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void SetRoomName(string name)
    {
        //Debug.Log(name);
        roomName = name;
        //Debug.Log(roomName);
    }

    public void CreateRoom()
    {
        if (roomName != "" && roomName != null)
        {
            //Debug.Log("a");
            networkManager.matchMaker.CreateMatch(roomName, roomSize, true, "", "", "", 0, 0, networkManager.OnMatchCreate);
        }
    }
}
