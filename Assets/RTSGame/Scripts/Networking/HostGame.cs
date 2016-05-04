using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.Networking.Types;
using UnityEngine.Networking.Match;
using System.Collections.Generic;

//using System.Collections;

public class HostGame : MonoBehaviour {

	List<MatchDesc> matchList = new List<MatchDesc>();
	bool matchCreated;
	NetworkMatch networkMatch;

	// Use this for initialization
	void Start () {
		networkMatch = gameObject.AddComponent<NetworkMatch> ();
	}

	void OnGUI(){
		if(GUILayout.Button("Create Room")){
			CreateMatchRequest create = new CreateMatchRequest ();
			create.name = "New Room";
			create.size = 4;
			create.advertise = true;
			create.password = "";

			networkMatch.CreateMatch (create, OnMatchCreate);
		}

		if(GUILayout.Button("List Rooms")){
			networkMatch.ListMatches (0, 20, "", OnMatchList);
		}

		if(matchList.Count > 0){
			GUILayout.Label ("Current Rooms");

			foreach(var match in matchList){
				if(GUILayout.Button(match.name)){
					if(GUILayout.Button(match.name)){
						networkMatch.JoinMatch (match.networkId, "", OnMatchJoined);
					}
				}
			}
		
		}
	}

	public void OnMatchCreate(CreateMatchResponse matchResponse){
		if(matchResponse.success){
			Debug.Log ("Create match succeeded");
			matchCreated = true;
			Utility.SetAccessTokenForNetwork (matchResponse.networkId, new NetworkAccessToken (matchResponse.accessTokenString));
			NetworkServer.Listen (new MatchInfo (matchResponse), 9000);
		}
		else{
			Debug.LogError ("Create match failed");
		}
	}

	public void OnMatchList(ListMatchResponse matchListResponse)
	{
		if (matchListResponse.success && matchListResponse.matches != null)
		{
			networkMatch.JoinMatch(matchListResponse.matches[0].networkId, "", OnMatchJoined);
		}
	}

	public void OnMatchJoined(JoinMatchResponse matchJoin)
	{
		if (matchJoin.success)
		{
			Debug.Log("Join match succeeded");
			if (matchCreated)
			{
				Debug.LogWarning("Match already set up, aborting...");
				return;
			}
			Utility.SetAccessTokenForNetwork(matchJoin.networkId, new NetworkAccessToken(matchJoin.accessTokenString));
			NetworkClient myClient = new NetworkClient();
			myClient.RegisterHandler(MsgType.Connect, OnConnected);
			myClient.Connect(new MatchInfo(matchJoin));
		}
		else
		{
			Debug.LogError("Join match failed");
		}
	}

	public void OnConnected(NetworkMessage msg)
	{
		Debug.Log("Connected!");
	}
}
