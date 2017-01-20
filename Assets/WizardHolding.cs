using UnityEngine;
using UnityEngine.UI;

public class WizardHolding : Photon.MonoBehaviour
{
    const float objectMoveSpeed = 15.0f;
    const float objectRotateSpeed = 200.0f;
    const float objectThrowPower = 10000.0f;

    const float objectDistanceMin = 2.7f;
    const float objectDistanceMax = 12.0f;
    const float objectDistanceSpeed = 0.2f;
    private float objectDistance = 10.0f;

    private Ray ray;
    private RaycastHit []hits;
    private GameObject holdedThing = null;

    void turnSpecialCrosshair(bool special)
    {
        GameObject.Find("Crosshair").GetComponent<Image>().enabled = !special;
        GameObject.Find("Crosshair2").GetComponent<Image>().enabled = special;
    }

    void dropHoldedObject()
    {
        holdedThing.GetComponent<Rigidbody>().useGravity = true;
        holdedThing = null;
    }

    float colliderSize(GameObject obj)
    {
        Collider objCollider = obj.GetComponent<Collider>();
        return Mathf.Max(objCollider.bounds.size.x, objCollider.bounds.size.y, objCollider.bounds.size.z);
    }

    void Update()
    {

        ray = Camera.main.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2));
        hits = Physics.RaycastAll(ray);
        foreach (var hit in hits)
        {
            if (hit.transform.gameObject.tag == "Holdable" && hit.distance < objectDistanceMax)
            {
                Debug.Log(hit.transform.gameObject.tag);
                turnSpecialCrosshair(true);

                if (Input.GetMouseButtonDown(0) && holdedThing == null)
                {
                    holdedThing = hit.transform.gameObject;
                    holdedThing.GetComponent<PhotonView>().RequestOwnership();
                    holdedThing.GetComponent<Rigidbody>().useGravity = false;
                    objectDistance = Mathf.Clamp(hit.distance, colliderSize(holdedThing) / 2 + objectDistanceMin, objectDistanceMax);
                    break;
                }
            } else
            {
                turnSpecialCrosshair(false);
            }
        }

        // Drop holded object
        if (!Input.GetMouseButton(0) && holdedThing != null)
        {
            dropHoldedObject();
        }

        // Moving object in space (forward, backward and rotation)
        if (holdedThing != null)
        {
            // Get point in front of camera
            var newObjectPosition = ray.GetPoint(objectDistance);

            // Move holded thing to new destination in front of camera
            var holdedThingDestination = Vector3.ClampMagnitude((newObjectPosition - holdedThing.transform.position) * objectMoveSpeed, objectMoveSpeed);
            holdedThing.GetComponent<Rigidbody>().velocity = holdedThingDestination;

            if (Input.GetKey(KeyCode.R))
            {
                if (objectDistance < objectDistanceMax) {
                    objectDistance = Mathf.Clamp(objectDistance + objectDistanceSpeed, objectDistanceMin, objectDistanceMax);
                }
            }
            if (Input.GetKey(KeyCode.F))
            {
                if (objectDistance > objectDistanceMin + colliderSize(holdedThing) / 2)
                {
                    objectDistance = Mathf.Clamp(objectDistance - objectDistanceSpeed, objectDistanceMin, objectDistanceMax);
                }
            }

            if (Input.GetMouseButton(1))
            {
                CameraMove.stopCamera = true;
                holdedThing.GetComponent<Rigidbody>().angularVelocity = new Vector3(0, 0, 0);
                holdedThing.transform.Rotate(new Vector3(Input.GetAxis("Mouse Y") * objectRotateSpeed * Time.deltaTime, -Input.GetAxis("Mouse X") * objectRotateSpeed * Time.deltaTime, 0), Space.World);
            } else
            {
                if (Input.GetKeyDown(KeyCode.T))
                {
                    holdedThing.GetComponent<Rigidbody>().AddForce(Camera.main.transform.forward * objectThrowPower, ForceMode.Impulse);
                    dropHoldedObject();
                }
            }
        }

        // Stop camera while rotating
        if (!Input.GetMouseButton(1) || holdedThing == null)
        {
            if (CameraMove.stopCamera)
            {
                CameraMove.stopCamera = false;
            }
        }
    }
}