using UnityEngine;

public class Wizard : MonoBehaviour
{
    bool run = false;
    Animator anim;

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
        if (!run)
        {
            anim.SetFloat("VelX", Mathf.Clamp(Input.GetAxis("MoveX"),-.50f,.50f));
            anim.SetFloat("VelY", Mathf.Clamp(Input.GetAxis("MoveY"),-.50f,.50f));
        }
        else
        {
            anim.SetFloat("VelX", Input.GetAxis("MoveX"));
            anim.SetFloat("VelY", Input.GetAxis("MoveY"));
        }
      
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
        }
    }
}