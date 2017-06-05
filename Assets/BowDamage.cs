using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BowDamage : Photon.MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            if (photonView.isMine)
            {
                Debug.Log("Hit!");
                other.gameObject.GetPhotonView().RPC("damage", PhotonTargets.All, 50.0f);
            }
        }
        else if (other.gameObject.tag != "Player") {
            Destroy(this);
        }
    }
}
