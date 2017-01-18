using UnityEngine;

public class BowAttack : Photon.MonoBehaviour {
    Animator anim;
	// Use this for initialization
	void Start () {

        anim = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {

        if (!photonView.isMine)
        {
            return;
        }


        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            anim.SetTrigger("Shoot");
        }
        if (Input.GetKeyDown(KeyCode.Mouse1))
        {
            anim.SetTrigger("SpecialShoot");
        }

    }
}
