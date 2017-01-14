using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class detectHit : MonoBehaviour {

    //public Slider healthbar;
    Animator anim;

    public float max_Health = 100f;
    public float cur_Health = 0f;
    private float damageMagnitude = 5.0f;
    private float damageMagnitudeSensivity = 0.8f;
    public GameObject healthBar;


    private void OnTriggerEnter(Collider other)
    {
        //    healthbar.value -= 100;
        //
        float colliderMagnitude = other.GetComponent<Rigidbody>().velocity.magnitude;
        cur_Health = Mathf.Clamp(cur_Health - colliderMagnitude * damageMagnitudeSensivity, 0, 100);
        float calc_Health = cur_Health / max_Health;
        SetHealthBar(calc_Health);
        if (calc_Health == 0)
        {
            anim.SetBool("isDead", true);
            GetComponent<NavMeshAgent>().enabled = false;
        }
    }

    // Use this for initialization
    void Start () {
        cur_Health = max_Health;
        anim = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void SetHealthBar(float myHealth)
    {
        healthBar.transform.localScale = new Vector3(myHealth, healthBar.transform.localScale.y, healthBar.transform.localScale.z);
    }

}
