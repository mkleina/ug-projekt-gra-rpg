using UnityEngine;

public class WSAD : MonoBehaviour {
    const int speed = 2;
    const int jumpPower = 15;
    const int topSpeed = 8;

	// Update is called once per frame
    Rigidbody rb;
    bool isFalling;

    // Use this for initialization
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }
    void FixedUpdate()
    {
        //if (rb.velocity.magnitude < topSpeed)
       // {
            if (Input.GetKey(KeyCode.W))
            {
                rb.AddRelativeForce(Vector3.forward * speed, ForceMode.VelocityChange);
            }
            if (Input.GetKey(KeyCode.S))
            {
                rb.AddRelativeForce(Vector3.back * speed, ForceMode.VelocityChange);
            }
            if (Input.GetKey(KeyCode.A))
            {
                rb.AddRelativeForce(Vector3.left * speed, ForceMode.VelocityChange);
            }
            if (Input.GetKey(KeyCode.D))
            {
                rb.AddRelativeForce(Vector3.right* speed, ForceMode.VelocityChange);
            }
      //  }

        jump();
    }

    void OnCollisionStay(Collision collisionInfo)
    {
        isFalling = false;
    }

    void jump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && !isFalling)
        {
            rb.AddForce(Vector3.up * jumpPower*speed, ForceMode.Impulse);
            isFalling = true;
        }
        else if (Input.GetKeyDown(KeyCode.Space) && !isFalling)
        {
            rb.AddForce(Vector3.up * jumpPower * speed, ForceMode.Impulse);

        }
    }
}

