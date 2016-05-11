using UnityEngine;
using System.Collections;
using System;

public class PhotonNetworkManager : MonoBehaviour {

	private const string s_roomName = "Arnold's Room";
	private RoomInfo[] roomsList;

	private RoomOptions roomOptions;
	private TypedLobby typedLobby;
	// Use this for initialization
	void Start () {
		PhotonNetwork.ConnectUsingSettings ("0.1");
		roomOptions = new RoomOptions ();
		//roomOptions.customRoomPropertiesForLobby = { "map", "ai" };
		//roomOptions.customRoomProperties = new Hashtable() { {"map", 1}};
		roomOptions.maxPlayers = 5;

		typedLobby = new TypedLobby ("myLobby", LobbyType.SqlLobby);
	}
	
	// Update is called once per frame
	void Update () {
		if(PhotonNetwork.connected) {
			Debug.Log ("Is Connected!");
		}
	}

	void OnGUI(){
		if(!PhotonNetwork.connected){
			GUILayout.Label (PhotonNetwork.connectionStateDetailed.ToString());
		}
		else if(PhotonNetwork.room == null){
			// Create Room
			if (GUI.Button (new Rect (100, 100, 250, 100), "Start Server")) {
				//PhotonNetwork.CreateRoom (s_roomName + Guid.NewGuid ().ToString ("N"));
				PhotonNetwork.CreateRoom (s_roomName + Guid.NewGuid ().ToString ("N"), roomOptions, typedLobby);
			}

			// Join Room
			if (roomsList != null){
				for(int i = 0; i < roomsList.Length; i++)
				{
					if (GUI.Button (new Rect (100, 250 + (110 * i), 250, 100), "Join" + roomsList [i].name)) 
					{
						PhotonNetwork.JoinRoom (roomsList [i].name);
					}
				}
			}
		}
	}

	void OnReceivedRoomListUpdate(){
		roomsList = PhotonNetwork.GetRoomList ();
	}

	void OnJoinedRoom(){
		Debug.Log ("Connected to Room");
	}
}
