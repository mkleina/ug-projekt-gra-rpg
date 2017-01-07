using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class WizardHolding : MonoBehaviour
{
    const float objectMoveSpeed = 2.2f;
    const float objectRotateSpeed = 180.0f;
    const float objectMoveMin = 3.0f;
    const float objectMoveMax = 12.0f;

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
        ray = Camera.allCameras[0].ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2));
        hits = Physics.RaycastAll(ray);
        foreach (var hit in hits)
        {
            if (hit.transform.gameObject.tag == "Holdable" && Mathf.Clamp(hit.distance, objectMoveMin, objectMoveMax) == hit.distance)
            {
                turnSpecialCrosshair(true);

                if (Input.GetMouseButton(0) && holdedThing == null)
                {
                    holdedThing = hit.transform.gameObject;
                    holdedThing.GetComponent<Rigidbody>().isKinematic = true;
                    holdedThing.transform.SetParent(Camera.main.transform);
                    break;
                }
            } else
            {
                turnSpecialCrosshair(false);
            }
        }
        if (!Input.GetMouseButton(0) && holdedThing != null)
        {
            Debug.Log("Dropped");
            holdedThing.GetComponent<Rigidbody>().isKinematic = false;
            holdedThing.transform.SetParent(null);
            holdedThing = null;
        }

        // Moving object towards holder
        if (holdedThing != null)
        {
            if (Input.GetKey(KeyCode.R))
            {
                if (Vector3.Distance(holdedThing.transform.position, holdedThing.transform.parent.position) < objectMoveMax) {
                    holdedThing.transform.position = Vector3.MoveTowards(holdedThing.transform.position, holdedThing.transform.parent.position, -objectMoveSpeed * Time.deltaTime);
                }
            }
            if (Input.GetKey(KeyCode.F))
            {
                if (Vector3.Distance(holdedThing.transform.position, holdedThing.transform.parent.position) > objectMoveMin)
                {
                    holdedThing.transform.position = Vector3.MoveTowards(holdedThing.transform.position, holdedThing.transform.parent.position, objectMoveSpeed * Time.deltaTime);
                }
            }



            if (Input.GetMouseButton(1))
            {
                CameraMove.stopCamera = true;
                holdedThing.transform.Rotate(new Vector3(Input.GetAxis("Mouse Y") * objectRotateSpeed * Time.deltaTime, -Input.GetAxis("Mouse X") * objectRotateSpeed * Time.deltaTime, 0));
            }
            //if (Input.GetKey(KeyCode.Z) && !Input.GetKey(KeyCode.LeftShift))
            //{
            //    holdedThing.transform.Rotate(new Vector3(objectRotateSpeed * Time.deltaTime, 0, 0));
            //}
            //if (Input.GetKey(KeyCode.Z) && Input.GetKey(KeyCode.LeftShift))
            //{
            //    holdedThing.transform.Rotate(new Vector3(-objectRotateSpeed * Time.deltaTime, 0, 0));
            //}
            //if (Input.GetKey(KeyCode.X) && !Input.GetKey(KeyCode.LeftShift))
            //{
            //    holdedThing.transform.Rotate(new Vector3(0, objectRotateSpeed * Time.deltaTime, 0));
            //}
            //if (Input.GetKey(KeyCode.X) && Input.GetKey(KeyCode.LeftShift))
            //{
            //    holdedThing.transform.Rotate(new Vector3(0, -objectRotateSpeed * Time.deltaTime, 0));
            //}
            //if (Input.GetKey(KeyCode.C) && !Input.GetKey(KeyCode.LeftShift))
            //{
            //    holdedThing.transform.Rotate(new Vector3(0, 0, objectRotateSpeed * Time.deltaTime));
            //}
            //if (Input.GetKey(KeyCode.C) && Input.GetKey(KeyCode.LeftShift))
            //{
            //    holdedThing.transform.Rotate(new Vector3(0, 0, -objectRotateSpeed * Time.deltaTime));
            //}
        }

        if (!Input.GetMouseButton(1) || holdedThing == null)
        {
            if (CameraMove.stopCamera)
            {
                CameraMove.stopCamera = false;
            }
        }
    }
}