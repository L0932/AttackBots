using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.Networking.Types;
using UnityEngine.Networking.Match;
using System.Collections.Generic;

//using System.Collections;

// Fix Updates: https://docs.unity3d.com/Manual/UpgradeGuide54Networking.html

public class HostGame : MonoBehaviour {

	List<MatchInfoSnapshot> matchList = new List<MatchInfoSnapshot>();
	bool matchCreated;
	NetworkMatch networkMatch;

	// Use this for initialization
	void Start () {
		networkMatch = gameObject.AddComponent<NetworkMatch> ();
	}

	void OnGUI(){
		if(GUILayout.Button("Create Room")){
            networkMatch.CreateMatch("New Room", 4, true, "", "", "", 0, 0, OnMatchCreate);
		}

		if(GUILayout.Button("List Rooms")){
			networkMatch.ListMatches (0, 10, "", true, 0, 0, OnMatchList);
		}

		if(matchList.Count > 0){
			GUILayout.Label ("Current Rooms");

			foreach(var match in matchList){
				if(GUILayout.Button(match.name)){
					if(GUILayout.Button(match.name)){
						networkMatch.JoinMatch (match.networkId, "", "", "", 0, 0, OnMatchJoined);
					}
				}
			}
		
		}
	}

    public void OnMatchCreate(bool success, string extendedInfo, MatchInfo matchInfo)
    {
        if (success)
        {
            Debug.Log("Create match succeeded");
            matchCreated = true;
            Utility.SetAccessTokenForNetwork(matchInfo.networkId, new NetworkAccessToken());
            NetworkServer.Listen(new MatchInfo(), 9000);
        }
        else
        {
            Debug.LogError("Create match failed");
        }
    }

    public void OnMatchList(bool success, string extendedInfo, List<MatchInfoSnapshot> matches)
    {
        if (success && matches != null)
        {
           // Implement this
           //networkMatch.JoinMatch(matches.networkId, "", OnMatchJoined);
        }
    }

    public void OnMatchJoined(bool success, string extendedInfo, MatchInfo matchInfo)
	{
		if (success)
		{
			Debug.Log("Join match succeeded");
			if (matchCreated)
			{
				Debug.LogWarning("Match already set up, aborting...");
				return;
			}
			Utility.SetAccessTokenForNetwork(matchInfo.networkId, new NetworkAccessToken());
			NetworkClient myClient = new NetworkClient();
			myClient.RegisterHandler(MsgType.Connect, OnConnected);
			myClient.Connect(new MatchInfo());
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
