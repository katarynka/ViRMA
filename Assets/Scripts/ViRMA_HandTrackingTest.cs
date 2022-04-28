using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ViRMA_HandTrackingTest : MonoBehaviour
{
    private OVRCameraRig m_CameraRig;
    public GameObject handTrackingLeft;
    public GameObject handTrackingRight;
    public GameObject controllerLeft;
    public GameObject controllerRight;

    void Start()
    {

        m_CameraRig = FindObjectOfType<OVRCameraRig>();
    }

    // Update is called once per frame
    void Update()
    {
        if (OVRInput.GetDown(OVRInput.Button.One))
        {
            handTrackingLeft.SetActive(true);
            handTrackingRight.SetActive(true);
            controllerLeft.SetActive(false);
            controllerRight.SetActive(false);
        }

    }
}
