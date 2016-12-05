using UnityEngine;
using System.Collections;

public class WSAD : MonoBehaviour {
    const int speed = 4;
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
        if (rb.velocity.magnitude < topSpeed)
        {
            if (Input.GetKey(KeyCode.W))
            {
                rb.AddRelativeForce(Vector3.forward * speed, ForceMode.VelocityChange);
            }
            if (Input.GetKey(KeyCode.S))
            {
                rb.velocity += -transform.forward * Time.deltaTime * speed;
            }
            if (Input.GetKey(KeyCode.A))
            {
                rb.velocity += -transform.right * Time.deltaTime * speed;
            }
            if (Input.GetKey(KeyCode.D))
            {
                rb.velocity += transform.right * Time.deltaTime * speed;
            }
        }

        if (Input.GetKey(KeyCode.Space) && !isFalling)
        {
            rb.AddForce(Vector3.up * jumpPower, ForceMode.Impulse);
            isFalling = true;
        }
    }

    void OnCollisionStay(Collision collisionInfo)
    {
        isFalling = false;
    }
}

