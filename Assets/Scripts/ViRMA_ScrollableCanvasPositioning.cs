using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ViRMA_ScrollableCanvasPositioning : MonoBehaviour
{
    public MeshFilter projectionObject;
    MeshRenderer quadOutline;

    void Start()
    {
        GameObject ovrCameraRig = GameObject.Find("OVRCameraRig");
        if (ovrCameraRig == null)
        {
            Debug.LogError("Scene does not contain an OVRCameraRig");
            return;
        }

        
        // The MeshRenderer component renders the quad as a blue outline
        // we only use this when Passthrough isn't visible
        quadOutline = projectionObject.GetComponent<MeshRenderer>();
        quadOutline.enabled = false;
    }

    void Update()
    {
        // Hide object when A button is held, show it again when button is released, move it while held.
        if (OVRInput.GetDown(OVRInput.Button.One))
        {
            quadOutline.enabled = true;
        }
        if (OVRInput.Get(OVRInput.Button.One))
        {
            OVRInput.Controller controllingHand = OVRInput.Controller.RTouch;
            Vector3 controllerPosition = OVRInput.GetLocalControllerPosition(controllingHand);
            Quaternion controllerRotation = OVRInput.GetLocalControllerRotation(controllingHand);
            controllerRotation.x += -41;
            Debug.Log(controllerPosition.x);
            transform.position = new Vector3(controllerPosition.x, controllerPosition.y, controllerPosition.z);
            transform.rotation = controllerRotation;
            //transform.rotation = controllerRotation;
            //Debug.Log("CONTROLLEROT: " + controllerRotation);
            //Debug.Log("MENUROT: " + new Quaternion(controllerRotation.w, controllerRotation.x - 41f, controllerRotation.y, controllerRotation.z));
        }
        if (OVRInput.GetUp(OVRInput.Button.One))
        {
            quadOutline.enabled = false;
        }
    }
}



    //// Start is called before the first frame update
    //void Start()
    //{
        
    //}

    //// Update is called once per frame
    //void Update()
    //{
    //    if (OVRInput.Get(OVRInput.Button.One))
    //    {
    //        OVRInput.Controller controllingHand = OVRInput.Controller.RTouch;
    //        transform.position = OVRInput.GetLocalControllerPosition(controllingHand);
    //        transform.rotation = OVRInput.GetLocalControllerRotation(controllingHand);
    //    }
    //}
    //}
