using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ViRMA_ClearQueriesButton : MonoBehaviour
{
    public Toggle toggleComponent;
    public string filterToClear;
    private ViRMA_GlobalsAndActions globals;
    private OVRCameraRig m_CameraRig;

    // Start is called before the first frame update
    void Start()
    {
        // Get toggle component
        toggleComponent = gameObject.GetComponent<Toggle>();
        toggleComponent.isOn = false;

        m_CameraRig = FindObjectOfType<OVRCameraRig>();
        // define ViRMA globals script
        globals = m_CameraRig.GetComponent<ViRMA_GlobalsAndActions>();

        // Get the filter (based on gameobject name)
        filterToClear = gameObject.name;
        Debug.Log("CONTEXTBTN AXIS: " + filterToClear);
    }

    // Update is called once per frame
    void Update()
    {
        // If toggle is on and no query has been made yet (to avoid repeated function calls)
        if (toggleComponent.isOn)
        {
            Debug.Log(filterToClear + " CLEARBTN ACTIVATED");
            if(filterToClear == "filter")
            { 
                globals.queryController.buildingQuery.ClearFilters();
            } else
            {
                globals.queryController.buildingQuery.ClearAxis(filterToClear, true);
            }
                
        }

            StartCoroutine(Wait());
    }

    private IEnumerator Wait()
    {
        yield return new WaitForSeconds(1);
        toggleComponent.isOn = false;
    }
}
