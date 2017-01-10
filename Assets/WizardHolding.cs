using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class WizardHolding : MonoBehaviour
{
    const float objectMoveSpeed = 15.0f;
    const float objectRotateSpeed = 200.0f;

    const float objectDistanceMin = 3.0f;
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

    void Update()
    {
        // Grab movable object in front of crosshair
        ray = Camera.main.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2));
        hits = Physics.RaycastAll(ray);
        foreach (var hit in hits)
        {
            if (hit.transform.gameObject.tag == "Holdable" && Mathf.Clamp(hit.distance, objectDistanceMin, objectDistanceMax) == hit.distance)
            {
                turnSpecialCrosshair(true);

                if (Input.GetMouseButton(0) && holdedThing == null)
                {
                    objectDistance = hit.distance;
                    holdedThing = hit.transform.gameObject;
                    holdedThing.GetComponent<Rigidbody>().useGravity = false;
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
            holdedThing.GetComponent<Rigidbody>().useGravity = true;
            holdedThing = null;
        }

        // Moving object in space (forward, backward and rotation)
        if (holdedThing != null)
        {
            // Get point in front of camera
            var cameraFront = Camera.main.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, objectDistance));

            // Move holded thing to new destination in front of camera
            var holdedThingDestination = Vector3.ClampMagnitude((cameraFront - holdedThing.transform.position) * objectMoveSpeed, objectMoveSpeed);
            holdedThing.GetComponent<Rigidbody>().velocity = holdedThingDestination;

            if (Input.GetKey(KeyCode.R))
            {
                if (Vector3.Distance(holdedThing.transform.position, ray.GetPoint(0)) < objectDistanceMax) {
                    objectDistance = Mathf.Clamp(objectDistance + objectDistanceSpeed, objectDistanceMin, objectDistanceMax);
                }
            }
            if (Input.GetKey(KeyCode.F))
            {
                if (Vector3.Distance(holdedThing.transform.position, ray.GetPoint(0)) > objectDistanceMin)
                {
                    objectDistance = Mathf.Clamp(objectDistance - objectDistanceSpeed, objectDistanceMin, objectDistanceMax);
                }
            }

            if (Input.GetMouseButton(1))
            {
                CameraMove.stopCamera = true;
                holdedThing.GetComponent<Rigidbody>().angularVelocity = new Vector3(0, 0, 0);
                holdedThing.transform.Rotate(new Vector3(Input.GetAxis("Mouse Y") * objectRotateSpeed * Time.deltaTime, -Input.GetAxis("Mouse X") * objectRotateSpeed * Time.deltaTime, 0), Space.World);
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