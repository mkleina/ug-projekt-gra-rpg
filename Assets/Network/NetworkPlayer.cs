using UnityEngine;

public class NetworkPlayer : Photon.MonoBehaviour {
    public GameObject camera;
    Vector3 position;
    Quaternion rotation;
    bool isAlive = true;
    float lerp = 5f;
    // Use this for initialization
    void Start()
    {
        PhotonNetwork.sendRate = 20;
        PhotonNetwork.sendRateOnSerialize = 10;
        if (photonView.isMine)
        {
            camera.SetActive(true);
            GetComponent<CameraMove>().enabled = true;
            GetComponent<WSAD>().enabled = true;
        }
        else
        {
            StartCoroutine("Alive");
        }
    }

    // Update is called once per frame
    void OnPhotonSerializeVIew(PhotonStream stream, PhotonMessageInfo info)
    {
        if(stream.isWriting)
        {
            stream.SendNext(transform.position);
            stream.SendNext(transform.rotation);
        }
        else
        {
            //position = (Vector3)stream.ReceiveNext();
            //rotation = (Quaternion)stream.ReceiveNext();
        }
    }

    //IEnumerator Alive()
    //{
    //    while(isAlive)
    //    {
    //        transform.position = Vector3.Lerp(transform.position, position, Time.deltaTime * lerp);
    //        transform.rotation = Quaternion.Lerp(transform.rotation, rotation, Time.deltaTime * lerp);
    //        yield return null;
    //    }
    //}
}