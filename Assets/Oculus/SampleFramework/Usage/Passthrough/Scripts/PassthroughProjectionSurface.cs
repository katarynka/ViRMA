using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PassthroughProjectionSurface : MonoBehaviour
{
    private OVRPassthroughLayer passthroughLayer;
    public MeshFilter projectionObject;
    MeshRenderer quadOutline;
    public float activationDistance;

    void Start()
    {
        activationDistance = 0.25f;

        GameObject ovrCameraRig = GameObject.Find("OVRCameraRig");
        if (ovrCameraRig == null)
        {
            Debug.LogError("Scene does not contain an OVRCameraRig");
            return;
        }


        passthroughLayer = ovrCameraRig.GetComponent<OVRPassthroughLayer>();
        if (passthroughLayer == null)
        {
            Debug.LogError("OVRCameraRig does not contain an OVRPassthroughLayer component");
        }

        passthroughLayer.AddSurfaceGeometry(projectionObject.gameObject, true);

        // The MeshRenderer component renders the quad as a blue outline
        // we only use this when Passthrough isn't visible
        quadOutline = projectionObject.GetComponent<MeshRenderer>();
        quadOutline.enabled = false;
    }

    void Update()
    {
        if (OVRInput.Get(OVRInput.Button.SecondaryIndexTrigger) && IsCloseEnough())
        {
            OVRInput.Controller controllingHand = OVRInput.Controller.RTouch;
            transform.position = OVRInput.GetLocalControllerPosition(controllingHand);
            transform.rotation = OVRInput.GetLocalControllerRotation(controllingHand);
            passthroughLayer.RemoveSurfaceGeometry(projectionObject.gameObject);
            quadOutline.enabled = true;
        }
    }


    bool IsCloseEnough()
    {
        OVRInput.Controller controllingHand = OVRInput.Controller.RTouch;
        if (Mathf.Abs(Vector3.Distance(OVRInput.GetLocalControllerPosition(controllingHand), transform.position)) < activationDistance)
            return true;

        return false;
    }

}
