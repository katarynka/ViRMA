using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;



// This script relates to population of scrollable menu with search results from DimExKeyboard.
public class ViRMA_SearchTagsScrollableMenu : MonoBehaviour
{


    // Reference to the Prefab. Drag a Prefab into this field in the Inspector.
    public GameObject buttonPrefab;
    public GameObject panelToAttachButtonsTo;
    public bool isPopulated = false;


    // This script will simply instantiate the Prefab when the game starts.
    void Start()
    {

    }

    public void PrintTest()
    {
        Debug.Log("PrintTestSuccessful! :)");
    }


    public IEnumerator PopulateMenu(List<Tag> searchResults)
    {
        Debug.Log("populate menu");
        if (isPopulated)
        {
            Debug.Log("destroying children of " + panelToAttachButtonsTo);
            foreach (Transform child in transform)
            {
                Debug.Log("to be destroyed" + child);
                child.GetComponent<ViRMA_ToggleButton>().DestroySelf();
                Debug.Log("destroy complete");
            }
        }

        foreach (Tag tag in searchResults)
        {
                GameObject button = (GameObject)Instantiate(buttonPrefab);
                button.transform.SetParent(panelToAttachButtonsTo.transform);   //Setting button parent

                button.transform.localPosition = new Vector3(button.transform.position.x, button.transform.position.y, 0);
                button.transform.localScale = new Vector3(1, 1, 1);
                button.transform.localRotation = new Quaternion(0,0,0,0);

                // Set togglegroup of button to the content they are laid on (necessary to avoid multiple select)
                button.GetComponent<Toggle>().group = panelToAttachButtonsTo.GetComponent<ToggleGroup>();
                button.GetComponentInChildren<TextMeshProUGUI>().text = tag.Label + " (" + tag.Parent.Label + ")";
                button.GetComponent<ViRMA_ToggleButton>().tag = tag;

        }

        isPopulated = true;


        // wait a second for DimExGroups to finish loading so AABB is calculated correctly
        // Taken from ViRMA_DimExplorerBtn, no idea what it does tbh but it's probably necessary :)
        yield return new WaitForSeconds(1);

    }


}
