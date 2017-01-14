using UnityEngine;

public class Animations : MonoBehaviour
{
    Animator anim;
    bool run = false;
    bool onGround = true;
    // Use this for initialization
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        run = Input.GetKey(KeyCode.LeftShift);
        anim.SetBool("Sprint", run);

        anim.SetFloat("VelX", Input.GetAxis("Horizontal"));
        anim.SetFloat("VelY", Input.GetAxis("Vertical"));

        if (Input.GetKeyDown(KeyCode.Space) && onGround)
        {
            jump();
        }
    }



    void jump()
    {
        anim.SetTrigger("Jump");
    }

    void OnTriggerEnter(Collider other)
    {
        onGround = true;
    }
}