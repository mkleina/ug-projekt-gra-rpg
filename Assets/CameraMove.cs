using UnityEngine;
using System.Collections;

public class CameraMove : MonoBehaviour {

    float rotationSensitivity = 250.0f;
    float minAngle = -90.0f;
    float maxAngle = 90.0f;

    // Camera X rotation value
    float xRotate = 0.0f;
    float yRotate = 0.0f;

    Rigidbody rb;

    // Use this for initialization
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        xRotate = rb.rotation.eulerAngles.x;
        yRotate = rb.rotation.eulerAngles.y;
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(rotationSensitivity);
        //Rotate Y view
        xRotate += -Input.GetAxis("Mouse Y") * rotationSensitivity * Time.deltaTime;
        yRotate += Input.GetAxis("Mouse X") * rotationSensitivity * Time.deltaTime;
        xRotate = Mathf.Clamp(xRotate, minAngle, maxAngle);
        Camera.allCameras[0].transform.eulerAngles = new Vector3(xRotate, transform.eulerAngles.y, 0.0f);
        rb.rotation = Quaternion.Euler(new Vector3(0, yRotate, 0));
    }
}
