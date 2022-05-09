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
    private ViRMA_ColourClearButtons axisProjectionMenu;

    // Start is called before the first frame update
    void Start()
    {
        dimExplorer = GameObject.Find("DimensionExplorer").GetComponent<ViRMA_DimExplorer>();
        axisProjectionMenu = GameObject.Find("ContextMenuButtonsCanvas").GetComponent<ViRMA_ColourClearButtons>();
        Debug.Log("axis proj men " + axisProjectionMenu);
        // Get toggle component
        toggleComponent = gameObject.GetComponent<Toggle>();
        toggleComponent.isOn = false;

        // Get currently selected tag

        // Get the filter (based on gameobject name)
        axisQueryType = gameObject.name;
    }

    // Update is called once per frame
    void Update()
    {
        tagQueryData = axisMenu.GetTag();
        // If toggle is on and no query has been made yet (to avoid repeated function calls)
        if (toggleComponent.isOn)
        {
            dimExplorer.SubmitContextBtnForQuery(tagQueryData, axisQueryType);
         
            StartCoroutine(Wait());
            //axisProjectionMenu.ColourButton(axisQueryType);


        }
    }

    private IEnumerator Wait()
    {
        yield return new WaitForSeconds(1);
        toggleComponent.isOn = false;
    }
}
