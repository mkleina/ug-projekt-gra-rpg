using UnityEngine;

public class WSAD : MonoBehaviour {

    const int speed = 2;
    const int jumpPower = 1100;
    const int topSpeed = 8;
    const float walkSpeed = 1.15f;
    bool run = false;
    bool isFalling = false;
    bool onGround = true;
    float jumping;
    Animator anim;
    Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        Runing();

        if (rb.velocity.magnitude < topSpeed)
        {
            if (run)
            {
                if (Input.GetKey(KeyCode.W))
                {
                    rb.AddRelativeForce(Vector3.forward * speed, ForceMode.VelocityChange);
                }

                if (Input.GetKey(KeyCode.S))
                {
                    rb.AddRelativeForce(Vector3.back * speed, ForceMode.VelocityChange); ;
                }
                if (Input.GetKey(KeyCode.A))
                {
                    rb.AddRelativeForce(Vector3.left * speed, ForceMode.VelocityChange);
                }
                if (Input.GetKey(KeyCode.D))
                {
                    rb.AddRelativeForce(Vector3.right * speed, ForceMode.VelocityChange);
                }
            }
            else
            {
                if (Input.GetKey(KeyCode.W))
                {
                    rb.AddRelativeForce(Vector3.forward *walkSpeed, ForceMode.VelocityChange);
                }

                if (Input.GetKey(KeyCode.S))
                {
                    rb.AddRelativeForce(Vector3.back *walkSpeed, ForceMode.VelocityChange); ;
                }
                if (Input.GetKey(KeyCode.A))
                {
                    rb.AddRelativeForce(Vector3.left *walkSpeed, ForceMode.VelocityChange);
                }
                if (Input.GetKey(KeyCode.D))
                {
                    rb.AddRelativeForce(Vector3.right *walkSpeed, ForceMode.VelocityChange);
                }
            }
        }
        anim.SetFloat("VelX", Input.GetAxis("Horizontal"));
        anim.SetFloat("VelY", Input.GetAxis("Vertical"));

        jump();


    }

    void jump()
    {

        if (Input.GetKeyDown(KeyCode.Space) && onGround)
        {
            
            rb.AddForce(new Vector3(0, jumpPower, 0));

            anim.SetTrigger("Jump");
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

    void OnCollisionStay(Collision collisionInfo)
    {
        onGround = true;
    }

    void OnCollisionExit(Collision collisionInfo)
    {
        onGround = false;
    }




}

