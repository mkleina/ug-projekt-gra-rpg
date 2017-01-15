using System.Collections;
using UnityEngine;

public class NetworkManager : MonoBehaviour {
    const string VERSION = "v0.0.1";
    public string roomName = "Dungeon";
    public Transform spawnPoint;
    public Transform spawnPoint2;
    public GameObject playerPrefab;
    public GameObject playerPrefab2;
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
        //if (PhotonNetwork.countOfPlayersInRooms == 0)
        //{
            GameObject player =(GameObject)PhotonNetwork.Instantiate(playerPrefab.name, spawnPoint.position, Quaternion.identity, 0);
            player.GetComponent<WSAD>().enabled = true;
            player.GetComponent<CameraMove>().enabled = true;
            player.GetComponent<WizardHolding>().enabled = true;
            player.transform.FindChild("Camera").gameObject.SetActive(true);
            player.GetComponent<CapsuleCollider>().enabled = true;
             player.GetComponent<PlayerHealth>().enabled = true;
            
        //}
        //if (PhotonNetwork.countOfPlayersInRooms == 1)
        //{
        //    GameObject player = PhotonNetwork.Instantiate(playerPrefab2.name, spawnPoint2.position, Quaternion.identity, 0);
        //    WSAD controller = player.GetComponent<WSAD>();
        //    controller.enabled = true;
        //    Camera camera = setCamera.GetComponentInChildren<Camera>();
        //    camera.enabled = true;
        //    CameraMove camera2 = player.GetComponent<CameraMove>();
        //    camera2.enabled = true;
        //    WizardHolding holding = player.GetComponent<WizardHolding>();
        //    holding.enabled = true;
        //}
    }

    private void Update()
    {


        
    }

    IEnumerator SpawnMyPlayer()
    {
        GameObject MyPlayer = PhotonNetwork.Instantiate(playerPrefab.name, spawnPoint.position, Quaternion.identity, 0);
        yield return new WaitForSeconds(0);
        MyPlayer.GetComponent<WSAD>().enabled = true;
        MyPlayer.GetComponent<CameraMove>().enabled = true;
        MyPlayer.GetComponent<Animator>().enabled = true;
        MyPlayer.GetComponentInChildren<Camera>().enabled = true;
    }
}
