using UnityEngine;
using UnityEngine.AI;

public class DetectHitByObject : Photon.MonoBehaviour {

    //public Slider healthbar;
    Animator anim;
    private float damageMagnitudeSensivity = 0.8f;
    public GameObject healthBar;


    private void OnCollisionEnter(Collision other)
    {
        if (photonView.isMine) photonView.RPC("damage", PhotonTargets.All, other.gameObject.GetComponent<Rigidbody>().velocity.magnitude * damageMagnitudeSensivity);
    }

    // Use this for initialization
    void Start () {
        anim = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
        float hp = GetComponent<CharacterHealth>().getPercent();

        setHealthBarValue(hp);
        if (hp == 0)
        {
            anim.SetBool("isDead", true);
            GetComponent<NavMeshAgent>().enabled = false;
        }
    }

    public void setHealthBarValue(float enemyHealth)
    {
        healthBar.transform.localScale = new Vector3(enemyHealth, healthBar.transform.localScale.y, healthBar.transform.localScale.z);
    }

}
