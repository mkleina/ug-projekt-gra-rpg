using UnityEngine;  
using System.Collections;                                            //  --1--

public class DNCycle : MonoBehaviour {

	public float speed = 1; 
	private float mn;                                            // --2--

	void Update () {                                             // --3--
		mn = speed * Time.deltaTime;                         // --4--
		transform.Rotate (Vector3.right * mn);               // --5--
	}
}