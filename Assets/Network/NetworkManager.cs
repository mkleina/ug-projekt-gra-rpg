using UnityEngine;
using UnityEngine.AI;

public class NetworkManager : MonoBehaviour {
    const string VERSION = "v0.0.1";
    public string roomName = "Dungeon";

    public Transform[] spawnPoints;
    public GameObject[] playerPrefabs;

    private GameObject playerPrefab;
    private Transform playerSpawnPoint;

    public GameObject skeleton;
    public Transform skeletonSpawnPoint;
    private Transform pozycjaSzkieletu = null;
    int skeletonNumber = 3;

    // Use this for initialization
    void Start() {
        PhotonNetwork.ConnectUsingSettings(VERSION);

    }

    private void Awake()
    {
        PhotonNetwork.automaticallySyncScene = true;
    }

    // Update is called once per frame
    void OnJoinedLobby()
    {
        RoomOptions roomOprions = new RoomOptions() { IsVisible = false, maxPlayers = 4 };
        PhotonNetwork.JoinOrCreateRoom("Projekt", roomOprions, TypedLobby.Default);

    }
    void OnCreatedRoom()
    {

        //PhotonNetwork.LoadLevel("Projekt");

            SpawnSkeleton();
            SpawnCube();
        

    }

    void OnJoinedRoom()
    {
            // Get current player spawn point and prefab
            playerPrefab = playerPrefabs[PhotonNetwork.countOfPlayersInRooms];
            playerSpawnPoint = spawnPoints[PhotonNetwork.countOfPlayersInRooms];

            // Instantiate player
            GameObject player = (GameObject)PhotonNetwork.InstantiateGameObject(playerPrefab.name, playerSpawnPoint.position, playerSpawnPoint.rotation, 0);
            player.GetComponent<WSAD>().enabled = true;
            player.GetComponent<CameraMove>().enabled = true;
            player.transform.FindChild("Camera").gameObject.SetActive(true);
            player.GetComponent<CapsuleCollider>().enabled = true;
            player.GetComponent<BoxCollider>().enabled = true;
            player.GetComponent<CharacterHealth>().enabled = true;
            player.GetComponent<PlayerHealthBar>().enabled = true;
            player.GetComponent<Animator>();

            // Enable character-specific scripts
            if (playerPrefab.name == "Wizard")
            {
                player.GetComponent<WizardHolding>().enabled = true;
            }
            if (playerPrefab.name == "Warrior")
            {
                player.GetComponent<Attack>().enabled = true;
            }
            if (playerPrefab.name == "Archer")
            {
                player.GetComponent<BowAttack>().enabled = true;
            }
    }

    void SpawnSkeleton()
    {
        Transform spawnPoint = GameObject.Find("SkeletonSpawn").transform;
        GameObject monster = PhotonNetwork.Instantiate("Skeleton", spawnPoint.position, spawnPoint.rotation, 0, null);
        monster.GetComponent<CharacterHealth>().enabled = true;
        monster.GetComponent<detectHit>().enabled = true;
        monster.GetComponent<Movement>().enabled = true;
        monster.GetComponent<NavMeshAgent>().enabled = true;

    }

    void SpawnCube()
    {
        Transform spawnPoint = GameObject.Find("CubeSpawn").transform;
        GameObject cube = PhotonNetwork.Instantiate("Konar", spawnPoint.position, spawnPoint.rotation, 0, null);
        cube.GetComponent<MeshCollider>().enabled = true;
    }

private void Update()
    {   
    }

}
