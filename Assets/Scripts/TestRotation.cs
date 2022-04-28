using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class TestRotation : MonoBehaviour
{
    OVRInput.Button grabButton;
    OVRInput.Button resetRotationButton;
    [Header("Controller")]
    public GameObject controller;
    public float activationDistance;
    private Quaternion currentRot;
    private Vector3 startPos;
    private bool offsetSet;
    private Vector2 thumbstickAxes;
    private float triggerAxis;
     
     
        // Start is called before the first frame update

    private void Start()
    {
        offsetSet = false;
        grabButton = OVRInput.Button.One;
    }

    // Update is called once per frame
    void Update()
    {

        if (OVRInput.Get(grabButton))
        {
            Rotate();
        }
        else
            offsetSet = false;
        if (OVRInput.GetDown(resetRotationButton))
            transform.eulerAngles = Vector3.zero;


        // Set thumbstick and trigger axes - these must remain in Update()
        thumbstickAxes = OVRInput.Get(OVRInput.Axis2D.SecondaryThumbstick);
        triggerAxis = OVRInput.Get(OVRInput.Axis1D.SecondaryHandTrigger);

        // Thumbstick moved away from player


        // Trigger held down
        if (triggerAxis > 0.8f)
        {
            //transform.position = new Vector3(transform.position.x, transform.position.y, controller.transform.position.z + 1f); Position adjustment - not functional

            if (thumbstickAxes.y > 0.5)
            {
                float scaleRate = /*0.00005f + */thumbstickAxes.y*0.0001f;
                transform.localScale = new Vector3(transform.localScale.x - scaleRate, transform.localScale.y - scaleRate, transform.localScale.z - scaleRate);
            }


            if (thumbstickAxes.y < -0.5) {
                float scaleRate = /*0.00005f*/ -thumbstickAxes.y*0.0001f;
                transform.localScale = new Vector3(transform.localScale.x + scaleRate, transform.localScale.y + scaleRate, transform.localScale.z + scaleRate);
            }

            if (thumbstickAxes.x > 0.5)
            {
                Debug.Log("ROTATION");
                float rotationRate = /*0.00005f + */thumbstickAxes.x;
                Vector3 v = new Vector3(transform.eulerAngles.x, transform.localRotation.eulerAngles.y - rotationRate, transform.eulerAngles.z);
                transform.localRotation = Quaternion.Euler(v);
            }

            if (thumbstickAxes.x < -0.5)
            {
                float rotationRate = /*0.00005f*/ thumbstickAxes.x;
                Vector3 v = new Vector3(transform.eulerAngles.x, transform.localRotation.eulerAngles.y - rotationRate, transform.eulerAngles.z);
                transform.localRotation = Quaternion.Euler(v);

                
            }

        }


    }

    void SetOffsets()
    {
        if (offsetSet)
            return;

        startPos = Vector3.Normalize(controller.transform.position - transform.position);
        currentRot = transform.rotation;

        offsetSet = true;
    }

    void Rotate()
    {
        SetOffsets();

        Vector3 closestPoint = Vector3.Normalize(controller.transform.position - transform.position);
        var rot = Quaternion.FromToRotation(startPos, closestPoint);
        rot = Quaternion.Euler(0, rot.eulerAngles.y, 0);
        transform.rotation = rot * currentRot;

    }

    bool IsCloseEnough()
    {
        if (Mathf.Abs(Vector3.Distance(controller.transform.position, transform.position)) < activationDistance)
            return true;

        return false;
    }
}
