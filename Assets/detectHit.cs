using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class detectHit : MonoBehaviour {

    //public Slider healthbar;
    Animator anim;

    public float max_Health = 100f;
    public float cur_Health = 0f;
    public GameObject healthBar;


    private void OnTriggerEnter(Collider other)
    {
        //    healthbar.value -= 100;
        //
        cur_Health -= 100f;
        float calc_Health = cur_Health / max_Health;
        SetHealthBar(calc_Health);
        if(calc_Health == 0)
        {
            anim.SetBool("isDead", true);
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
