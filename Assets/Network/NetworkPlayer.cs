using UnityEngine;

public class NetworkPlayer : Photon.MonoBehaviour
{
    public GameObject camera;
    Vector3 docelowaPozycja;
    Quaternion docelowaRotacja;
    bool isAlive = true;
    public float razy = 5f;
    // Use this for initialization
    void Start()
    {
        PhotonNetwork.sendRate = 30;
        PhotonNetwork.sendRateOnSerialize = 30;
        if (photonView.isMine)
        {

        }
  
     
    }

    private void Update()
    {
        if (!photonView.isMine)
        {
            transform.position = Vector3.Lerp(transform.position, docelowaPozycja, razy * Time.deltaTime);
            transform.rotation = Quaternion.Slerp(transform.rotation, docelowaRotacja, razy * Time.deltaTime);
        }
    }

    void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (photonView.isMine)
        {
            stream.SendNext(transform.position);
            stream.SendNext(transform.rotation);
        }
        else
        {
            docelowaPozycja = (Vector3)stream.ReceiveNext();
            docelowaRotacja = (Quaternion)stream.ReceiveNext();
        }
    }
}