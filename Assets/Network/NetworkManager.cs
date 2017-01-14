using UnityEngine;

public class NetworkManager : MonoBehaviour {
    const string VERSION = "v0.0.1";
    public string roomName = "Dungeon";
    public string playerPrefab = "Wizard";
    public Transform spawnPoint;
	// Use this for initialization
	void Start () {
        PhotonNetwork.ConnectUsingSettings(VERSION);
	}
	
	// Update is called once per frame
	void OnJoinedLobby()
    {
        RoomOptions roomOprions = new RoomOptions() { IsVisible = false, maxPlayers = 4 };
        PhotonNetwork.JoinOrCreateRoom(roomName, roomOprions, TypedLobby.Default);
    }

    void OnJoinedRoom()
    {
        PhotonNetwork.Instantiate(playerPrefab, spawnPoint.position, spawnPoint.rotation, 0);
    }
}
