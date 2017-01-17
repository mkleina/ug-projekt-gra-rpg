using System.Collections;
using UnityEngine;

public class NetworkManager : MonoBehaviour {
    const string VERSION = "v0.0.1";
    public string roomName = "Dungeon";

    public Transform []spawnPoints;
    public GameObject []playerPrefabs;

    private GameObject playerPrefab;
    private Transform playerSpawnPoint;

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
        // Get current player spawn point and prefab
        playerPrefab = playerPrefabs[PhotonNetwork.countOfPlayersInRooms];
        playerSpawnPoint = spawnPoints[PhotonNetwork.countOfPlayersInRooms];
        
        // Instantiate player
        GameObject player = (GameObject)PhotonNetwork.Instantiate(playerPrefab.name, playerSpawnPoint.position, playerSpawnPoint.rotation, 0);
        player.GetComponent<WSAD>().enabled = true;
        player.GetComponent<CameraMove>().enabled = true;
        player.transform.FindChild("Camera").gameObject.SetActive(true);
        player.GetComponent<CapsuleCollider>().enabled = true;
        player.GetComponent<BoxCollider>().enabled = true;
        player.GetComponent<CharacterHealth>().enabled = true;
        player.GetComponent<PlayerHealthBar>().enabled = true;

        // Enable character-specific scripts
        if (playerPrefab.name == "Wizard")
        {
            player.GetComponent<WizardHolding>().enabled = true;
        }
        if (playerPrefab.name == "Warrior")
        {
            player.GetComponent<Attack>().enabled = true;
        }
    }

    private void Update()
    {   
    }

    IEnumerator SpawnMyPlayer()
    {
        GameObject MyPlayer = PhotonNetwork.Instantiate(playerPrefab.name, playerSpawnPoint.position, playerSpawnPoint.rotation, 0);
        yield return new WaitForSeconds(0);
        MyPlayer.GetComponent<WSAD>().enabled = true;
        MyPlayer.GetComponent<CameraMove>().enabled = true;
        MyPlayer.GetComponent<Animator>().enabled = true;
        MyPlayer.GetComponentInChildren<Camera>().enabled = true;
    }
}
