using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking.Match;
using UnityEngine.UI;

public class RoomListItem : MonoBehaviour {

    public delegate void JoinRoomDelegate(MatchInfoSnapshot match);
    private JoinRoomDelegate joinRoomCallBack;

    private MatchInfoSnapshot match;

    [SerializeField] private Text roomNameText;
     
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void Setup(MatchInfoSnapshot match1,JoinRoomDelegate joinRoomCallback1)
    {
        match = match1;
        joinRoomCallBack = joinRoomCallback1;

        roomNameText.text = match.name + " ( " + match.currentSize + " / " + match.maxSize + " )";
    }

    public void JoinRoom()
    {
        joinRoomCallBack.Invoke(match);
    }

}
