using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ViRMA_ToggleButton : MonoBehaviour
{
    public Toggle toggleComponent;
    public bool contextMenuRendered = false;
    public GameObject axisProjectionMenuPrefab;
    public GameObject contextMenuButtonsCanvas;
    public GameObject clearFilersMenu;
    public Canvas clearFilersMenuCanvas;
    public Tag tag;
    public GameObject contextMenu;
    public ViRMA_AxisProjectionMenu axisProjectionMenu;
    public Canvas contextMenuCanvas;
    public ViRMA_ContextMenuToggle contextMenuSwitcher;
    public ToggleGroup toggleGroup;
    public Vector3 originalPos;
    public GameObject clearFiltersMenuButtonsCanvas;
    public GameObject clearFiltersMenu;
    public Canvas clearFiltersMenuCanvas;

    // Start is called before the first frame update
    void Start()
    {
        contextMenuButtonsCanvas = GameObject.Find("ContextMenuButtonsCanvas");
        contextMenu = GameObject.Find("ViRMA_ContextMenuButtons");
        clearFiltersMenuButtonsCanvas = GameObject.Find("ClearFiltersMenuButtonsCanvas");
        clearFiltersMenu = GameObject.Find("ViRMA_ContextMenuButtons");
        axisProjectionMenu = contextMenu.GetComponent<ViRMA_AxisProjectionMenu>();
        toggleComponent = gameObject.GetComponent<Toggle>();
        contextMenuCanvas = contextMenuButtonsCanvas.GetComponent<Canvas>();
        clearFiltersMenuCanvas = clearFiltersMenuButtonsCanvas.GetComponent<Canvas>();
        toggleGroup = contextMenuButtonsCanvas.GetComponentInChildren<ToggleGroup>();
    }

    // Update is called once per frame
    void Update()
    {


        if (toggleComponent.isOn)
        {
            //contextMenu.GetComponentInChildren<Canvas>().gameObject.SetActive(true);
            contextMenuCanvas.enabled = true;
            clearFiltersMenuCanvas.enabled = true;
            if (!contextMenuRendered)
            {

                // ADD PASSING TAG
                toggleGroup.SetAllTogglesOff();
                axisProjectionMenu.tag = tag;
                Debug.Log("axisMenu tag: " + axisProjectionMenu.tag.Label + " toggle button tag: " + tag.Label);


                contextMenuRendered = true;

            }
        } else
        {
            contextMenuRendered = false;
        }

    }

    // Destroy the button, called from the search menu script to clear the old results
    public void DestroySelf()
    {
        Destroy(this.gameObject);
    }

}
