using UnityEngine;

//public class CameraMove : MonoBehaviour {

//    float rotationSensitivity = 250.0f;
//    float minAngle = -90.0f;
//    float maxAngle = 90.0f;

//    // Camera X rotation value
//    float xRotate = 0.0f;
//    float yRotate = 0.0f;

//    public static bool stopCamera = false;

//    Rigidbody rb;

//    // Use this for initialization
//    void Start()
//    {
//        rb = GetComponent<Rigidbody>();
//        xRotate = rb.rotation.eulerAngles.x;
//        yRotate = rb.rotation.eulerAngles.y;
//    }

//    // Update is called once per frame
//    void Update()
//    {
//        //Rotate Y view
//        xRotate += -Input.GetAxis("Mouse Y") * rotationSensitivity * Time.deltaTime;
//        yRotate += Input.GetAxis("Mouse X") * rotationSensitivity * Time.deltaTime;
//        xRotate = Mathf.Clamp(xRotate, minAngle, maxAngle);

//        Debug.Log(xRotate);

//        if (!stopCamera)
//        {
//            Camera.main.transform.eulerAngles = new Vector3(xRotate, transform.eulerAngles.y, 0.0f);
//            rb.rotation = Quaternion.Euler(new Vector3(0, yRotate, 0));
//        }
//    }
//}

public class CameraMove : Photon.MonoBehaviour
{

    float rotationSensitivity = 50.0f;
    //float minAngle = 90.0f;
    //float maxAngle = 0;

    public static bool stopCamera = false;

    Rigidbody rb;

    // Use this for initialization
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!photonView.isMine) return;
        if (!stopCamera)
        {
            Camera.main.transform.Rotate(new Vector3(-Input.GetAxis("Mouse Y") * rotationSensitivity * Time.deltaTime, 0, 0));
            rb.transform.Rotate(new Vector3(0, Input.GetAxis("Mouse X") * rotationSensitivity * Time.deltaTime, 0));
        }
    }
}