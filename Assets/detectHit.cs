using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class detectHit : MonoBehaviour {

    //public Slider healthbar;
    Animator anim;
    private float damageMagnitudeSensivity = 0.8f;
    public GameObject healthBar;


    private void OnTriggerEnter(Collider other)
    {
        GetComponent<CharacterHealth>().damage(other.GetComponent<Rigidbody>().velocity.magnitude * damageMagnitudeSensivity);
    }

    // Use this for initialization
    void Start () {
        anim = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
        float hp = GetComponent<CharacterHealth>().getPercent();
        SetHealthBar(hp);
        if (hp == 0)
        {
            anim.SetBool("isDead", true);
            GetComponent<NavMeshAgent>().enabled = false;

            // NOTE: Przykład zmiany dialogu NPC
            GameObject.Find("NPC1").GetComponent<NPCTalk>().setDialog(1);
        }
    }

    public void SetHealthBar(float myHealth)
    {
        healthBar.transform.localScale = new Vector3(myHealth, healthBar.transform.localScale.y, healthBar.transform.localScale.z);
    }

}
