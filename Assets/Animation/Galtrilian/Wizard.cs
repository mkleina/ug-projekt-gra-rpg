using UnityEngine;

public class Wizard : MonoBehaviour {

	Animator anim;

	// Use this for initialization
	void Start () {
		anim = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetButtonDown("Jump"))
		{
			anim.SetTrigger("isJumping");
		}

		if(Input.GetKey(KeyCode.W))
		{
			anim.SetBool("isRuning", true);
		}
		else
		{
			anim.SetBool("isRuning", false);
		}
	}
}
