using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectTimeout : MonoBehaviour {
    public float delay;
    private float timer = 0f;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        timer += Time.deltaTime;
        if (timer >= delay)
        {
            Destroy(this.gameObject);
            Debug.Log("Bow auto destroyed");
        }
    }
}
