using UnityEngine;

public class Wizard : MonoBehaviour
{
    bool run = false;
    Animator anim;
    bool onGround = true;
    // Use this for initialization
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        Runing();
        Moveing();
        Jumping();
    }
    void Moveing()
    {

            anim.SetFloat("VelX", Input.GetAxis("MoveX"));
            anim.SetFloat("VelY", Input.GetAxis("MoveY"));

      
    }

    void Runing()
    {
        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            run = !run;
            anim.SetBool("Sprint", run);
        }
    }
    void Jumping()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {      
            anim.SetTrigger("Jump");
            onGround = false;
        }
    }
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            onGround = true;
        }
    }
}