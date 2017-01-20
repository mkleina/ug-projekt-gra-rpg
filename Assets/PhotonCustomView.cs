using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhotonCustomView : MonoBehaviour, IPunObservable
{

	// Use this for initialization
	void Awake () {
		
	}

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.isWriting)
        {
            stream.SendNext(GetComponent<Rigidbody>().useGravity);
        }
        else
        {
            GetComponent<Rigidbody>().useGravity = (bool)stream.ReceiveNext();
        }
    }
}
