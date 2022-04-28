using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ViRMA_HelpMenu : MonoBehaviour
{
    OVRInput.Button toggleButton;
    public bool menuHidden = true;
    private ViRMA_GlobalsAndActions globals;
    private OVRCameraRig m_CameraRig;


    // Start is called before the first frame update
    void Start()
    {
        toggleButton = OVRInput.Button.Three;

        m_CameraRig = FindObjectOfType<OVRCameraRig>();
        // define ViRMA globals script
        globals = m_CameraRig.GetComponent<ViRMA_GlobalsAndActions>();

    }

    // Update is called once per frame
    void Update()
    {
        if (OVRInput.GetDown(toggleButton) && OVRInput.GetActiveController() == OVRInput.Controller.Touch)
        {

            if (menuHidden)
            {
                // Set position and rotation to be in front of camera
                transform.position = Camera.main.transform.TransformPoint(Vector3.forward * 0.5f);
                transform.rotation = transform.rotation = Camera.main.transform.rotation;
                menuHidden = false;
            }
            else
            {
                // Banish the menu to the shadow realm
                transform.position = new Vector3(0, 9999, 0);
                menuHidden = true;
            }
        }

        if (OVRInput.GetDown(OVRInput.Button.Four))
        {
            globals.queryController.buildingQuery.ClearAxis("X", true);
            globals.queryController.buildingQuery.ClearAxis("Y", true);
            globals.queryController.buildingQuery.ClearAxis("Z", true);
            globals.queryController.buildingQuery.ClearFilters();
        }
    }
}
