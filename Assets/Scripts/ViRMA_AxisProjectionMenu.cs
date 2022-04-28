using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ViRMA_AxisProjectionMenu : MonoBehaviour
{

    public Tag tag;
    public GameObject canvasObject;
    public ViRMA_ContextMenuToggle contextMenuSwitcher;
    public Canvas menuCanvas;
    public GameObject scrollableCanvas;

    // Start is called before the first frame update
    void Start()
    {

        //transform.SetParent(scrollableCanvas.transform);
        //gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {


        //Vector3 position = scrollableCanvas.transform.position;
        //transform.localPosition = new Vector3(position.x, position.y, position.z);
        //transform.rotation = scrollableCanvas.transform.rotation;
    }

    public Tag GetTag()
    {
        return tag;
    }

    public void SetTag(Tag newTag)
    {
        Debug.Log("tagOLD: " + tag.Label);
        tag = newTag;
        Debug.Log("tagNEW: " + tag.Label);
    }


}
