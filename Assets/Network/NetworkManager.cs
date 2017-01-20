using Boo.Lang;
using UnityEngine;
using UnityEngine.AI;

public class NetworkManager : MonoBehaviour
{
    const string VERSION = "v0.0.1";
    public string roomName = "Dungeon";

    // Use this for initialization
    void Start()
    {
        PhotonNetwork.ConnectUsingSettings(VERSION);
    }

    private void Awake()
    {
        PhotonNetwork.automaticallySyncScene = true;
    }

    // Update is called once per frame
    void OnJoinedLobby()
    {
        RoomOptions roomOptions = new RoomOptions() { IsVisible = false, MaxPlayers = 4 };
        PhotonNetwork.JoinOrCreateRoom(roomName, roomOptions, TypedLobby.Default);
    }
    void OnCreatedRoom()
    {

        //PhotonNetwork.LoadLevel("Projekt");

        //SpawnSkeleton();
        //SpawnCube();

        var spawnPoints = GameObject.FindGameObjectsWithTag("SpawnPoint");
        foreach (var spawnPoint in spawnPoints)
        {
            var spawnParams = spawnPoint.GetComponent<SpawnPointParameters>();
            if (spawnParams.objectType != "player" && spawnParams.objectType != "stone")
            {
                Debug.Log(spawnPoint.name + ": " + spawnParams.objectType);
                PhotonNetwork.Instantiate(spawnParams.prefab.name, spawnPoint.transform.position, spawnPoint.transform.rotation, 0, null);
            }
        }

    }

    void OnJoinedRoom()
    {
        Debug.Log("Joined room");

        // All available players with spawn order
        List<string> availPlayers = new List<string>();
        availPlayers.Add("Wizard");
        availPlayers.Add("Warrior");
        availPlayers.Add("Archer");


        foreach (PhotonPlayer otherPlayer in PhotonNetwork.otherPlayers)
        {
            availPlayers = availPlayers.Remove(otherPlayer.NickName);
            Debug.Log("Found other player: " + otherPlayer.NickName);
        }
        if (availPlayers.Count > 0)
        {
            PhotonNetwork.player.NickName = availPlayers[0];
        }
        else
        {
            Debug.Log("No more players left");
            return;
        }
        Debug.Log("Let begin as " + PhotonNetwork.player.NickName);

        // Scan all player spawn points
        var spawnPoints = GameObject.FindGameObjectsWithTag("SpawnPoint");
        foreach (var spawnPoint in spawnPoints)
        {
            var spawnParams = spawnPoint.GetComponent<SpawnPointParameters>();

            // Spawn next player if available
            if (spawnParams.objectType == "player" && spawnParams.prefab.name == PhotonNetwork.player.NickName)
            {


                // Instantiate player
                GameObject player = (GameObject)PhotonNetwork.InstantiateGameObject(spawnParams.prefab.name, spawnPoint.transform.position, spawnPoint.transform.rotation, 0);
                player.GetComponent<WSAD>().enabled = true;
                player.GetComponent<CameraMove>().enabled = true;
                player.transform.FindChild("Camera").gameObject.SetActive(true);
                player.GetComponent<CapsuleCollider>().enabled = true;
                player.GetComponent<BoxCollider>().enabled = true;
                player.GetComponent<CharacterHealth>().enabled = true;
                player.GetComponent<PlayerHealthBar>().enabled = true;
                player.GetComponent<Animator>().enabled = true;

                // Enable character-specific scripts
                if (spawnParams.prefab.name == "Wizard")
                {
                    player.GetComponent<WizardHolding>().enabled = true;
                }
                if (spawnParams.prefab.name == "Warrior")
                {
                    player.GetComponent<Attack>().enabled = true;
                    Debug.Log("------------------ Warrior ATTACK!");
                }
                if (spawnParams.prefab.name == "Archer")
                {
                    player.GetComponent<BowAttack>().enabled = true;
                }
            }
        }
    }

    //void SpawnSkeleton()
    //{
    //    Transform spawnPoint = GameObject.Find("SkeletonSpawn").transform;
    //    GameObject monster = PhotonNetwork.Instantiate("Skeleton", spawnPoint.position, spawnPoint.rotation, 0, null);
    //    monster.GetComponent<CharacterHealth>().enabled = true;
    //    monster.GetComponent<detectHit>().enabled = true;
    //    monster.GetComponent<Movement>().enabled = true;
    //    monster.GetComponent<NavMeshAgent>().enabled = true;

    //}

    //void SpawnCube()
    //{
    //    Transform spawnPoint = GameObject.Find("CubeSpawn").transform;
    //    GameObject cube = PhotonNetwork.Instantiate("Konar", spawnPoint.position, spawnPoint.rotation, 0, null);
    //    cube.GetComponent<MeshCollider>().enabled = true;
    //}

    private void Update()
    {
    }

}
