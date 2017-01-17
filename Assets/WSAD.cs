using UnityEngine;

public class WSAD : Photon.MonoBehaviour {

    const float runSpeed = 8.0f;
    const float walkSpeed = 3.0f;

    const float jumpPower = 120000.0f;

    bool run = false;
    bool onGround = true;

    Animator anim;
    Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        if (!photonView.isMine)
        {
            return;
        }
            run = Input.GetKey(KeyCode.LeftShift);
            anim.SetBool("Sprint", run);

            float speed = run ? runSpeed : walkSpeed;

            if (rb.velocity.magnitude < speed)
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
            anim.SetFloat("VelX", Input.GetAxis("Horizontal"));
            anim.SetFloat("VelY", Input.GetAxis("Vertical"));

            if (Input.GetKeyDown(KeyCode.Space) && onGround)
            {
                jump();
            }
        
    }

    void jump()
    {
        onGround = false;
        rb.AddRelativeForce(new Vector3(0, jumpPower, 0));
        anim.SetTrigger("Jump");
    }

    void OnTriggerEnter(Collider other)
    {
        onGround = true;
    }
}

