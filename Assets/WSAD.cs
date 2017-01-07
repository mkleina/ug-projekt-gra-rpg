using UnityEngine;

public class WSAD : MonoBehaviour {

    const int speed = 2;
    const int jumpPower = 15;
    const int topSpeed = 8;
    const float walkmulti = 0.5f;
    const float boost = 10;
    bool run = false;
    bool isFalling = false;
    bool onGround = true;
    bool Stay = true;
    float Horizontal = Input.GetAxis("Horizontal");
    float Vertical = Input.GetAxis("Vertical");
    float groundDistance;
    Animator anim;
    Rigidbody rb;
    CharacterController cc;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
        groundDistance = GetComponent<Collider>().bounds.extents.y;
    }
    void Update()
    {
       cc = GetComponent<CharacterController>();
    }

    void FixedUpdate()
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
                    rb.AddRelativeForce(Vector3.forward * speed* walkmulti, ForceMode.VelocityChange);
                }

                if (Input.GetKey(KeyCode.S))
                {
                    rb.AddRelativeForce(Vector3.back * speed*walkmulti, ForceMode.VelocityChange); ;
                }
                if (Input.GetKey(KeyCode.A))
                {
                    rb.AddRelativeForce(Vector3.left * speed*walkmulti, ForceMode.VelocityChange);
                }
                if (Input.GetKey(KeyCode.D))
                {
                    rb.AddRelativeForce(Vector3.right * speed*walkmulti, ForceMode.VelocityChange);
                }
            }
        }
        anim.SetFloat("VelX", Input.GetAxis("MoveX"));
        anim.SetFloat("VelY", Input.GetAxis("MoveY"));

        jump();


    }

    void OnCollisionStay(Collision collisionInfo)
    {
        isFalling = false;
    }


    void jump()
    {

        if (Input.GetKeyDown(KeyCode.Space) && onTheGround() && !run)
        {
            //gameObject.GetComponent<Rigidbody>().AddForce(Vector3.up * 100f);
            rb.AddForce(Vector3.up * jumpPower * speed, ForceMode.Impulse);
           
            anim.SetTrigger("Jump");
        }
        if (Input.GetKeyDown(KeyCode.Space) && onTheGround() && run)
        {
            Vector3 movement = new Vector3(Horizontal*50,30, 150);       
            rb.AddForce(movement, ForceMode.Impulse);
            //rb.AddForce(Vector3.up * jumpPower, ForceMode.Impulse);
            //rb.AddRelativeForce(Vector3.forward * speed * boost, ForceMode.VelocityChange);
            //rb.velocity = new Vector3(0f, 5f, 0f);
            anim.SetTrigger("Jump");
        }



    }
    bool onTheGround()
    {
        return Physics.Raycast(transform.position, -Vector3.up, groundDistance + 0.1f);
    }


    void Runing()
    {
        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            run = !run;
            anim.SetBool("Sprint", run);
        }
    }



}

