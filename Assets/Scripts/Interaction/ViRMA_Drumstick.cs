using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;

public class ViRMA_Drumstick : MonoBehaviour
{
    private ViRMA_GlobalsAndActions globals;
    public OVRCameraRig m_CameraRig;

    private void Awake()
    {
        m_CameraRig = FindObjectOfType<OVRCameraRig>();
        // define ViRMA globals script
        globals = m_CameraRig.GetComponent<ViRMA_GlobalsAndActions>();
        Debug.Log("GLOBALS: " + globals);

    }

    void Start()
    {

                GameObject steamVRHoverPoint = GameObject.Find("PokeLocation").gameObject;
                steamVRHoverPoint.transform.position = transform.position;

        //GetComponent<Renderer>().material.renderQueue = 3001;
    }

    private void OnTriggerEnter(Collider triggeredCol)
    {
        //Debug.Log("TRIGGER ENTER! " + triggeredCol.name);
    }
    private void OnTriggerExit(Collider triggeredCol)
    {
        //Debug.Log("TRIGGER EXIT! " + triggeredCol.name);
    }
    private void OnTriggerStay(Collider triggeredCol)
    {
        //Debug.Log("TRIGGER STAYING! " + triggeredCol.name);
    }

}
