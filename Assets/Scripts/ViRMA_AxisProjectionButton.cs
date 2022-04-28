using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ViRMA_AxisProjectionButton : MonoBehaviour
{
    public ViRMA_AxisProjectionMenu axisMenu;
    public ViRMA_DimExplorer dimExplorer;
    public Toggle toggleComponent;
    public Tag tagQueryData;
    public string axisQueryType;

    // Start is called before the first frame update
    void Start()
    {
        dimExplorer = GameObject.Find("DimensionExplorer").GetComponent<ViRMA_DimExplorer>();

        // Get toggle component
        toggleComponent = gameObject.GetComponent<Toggle>();
        toggleComponent.isOn = false;

        // Get currently selected tag

        // Get the filter (based on gameobject name)
        axisQueryType = gameObject.name;
        Debug.Log("CONTEXTBTN AXIS: " + axisQueryType);
    }

    // Update is called once per frame
    void Update()
    {
        tagQueryData = axisMenu.GetTag();
        // If toggle is on and no query has been made yet (to avoid repeated function calls)
        if (toggleComponent.isOn)
        {
                Debug.Log(axisQueryType + " CONTEXTBTN ACTIVATED");
                dimExplorer.SubmitContextBtnForQuery(tagQueryData, axisQueryType);
         
            StartCoroutine(Wait());
            
        }
    }

    private IEnumerator Wait()
    {
        yield return new WaitForSeconds(1);
        toggleComponent.isOn = false;
    }
}
