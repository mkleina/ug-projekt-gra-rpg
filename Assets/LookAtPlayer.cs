using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtPlayer : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        var pozycja = GameObject.FindGameObjectWithTag("Wizzard").transform.position;
        if (pozycja != null)
        {
            transform.LookAt(pozycja);
        }
        
    }
}
