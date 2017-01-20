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
        var rigid = GetComponent<Rigidbody>();
        var health = GetComponent<CharacterHealth>();
        if (stream.isWriting)
        {
            if (rigid != null) stream.SendNext(rigid.useGravity);
            if (health != null) stream.SendNext(health.healthValue);
        }
        else
        {
            if (rigid != null) rigid.useGravity = (bool)stream.ReceiveNext();
            if (health != null) health.healthValue = (float)stream.ReceiveNext();
        }
    }
}
