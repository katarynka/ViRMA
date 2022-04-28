﻿using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using Valve.VR;
using Valve.VR.InteractionSystem;

public class ViRMA_DimExplorerBtn : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    private ViRMA_GlobalsAndActions globals;
    private ViRMA_DimExplorerGroup parentDimExGrp;
    public bool searchedForTag;
    public bool contextMenuActiveOnBtn;

    public Tag tagData;

    // assigned inside prefab
    public GameObject background;
    public GameObject innerBackground;
    public TextMeshPro textMesh;
    public BoxCollider col;
    public Renderer bgRend;
    public MaterialPropertyBlock matPropBlock;

    public OVRCameraRig m_CameraRig;

    private void Awake()
    {
        m_CameraRig = FindObjectOfType<OVRCameraRig>();
        globals = m_CameraRig.GetComponent<ViRMA_GlobalsAndActions>();

        matPropBlock = new MaterialPropertyBlock();
    }

    private void Start()
    {
        parentDimExGrp = transform.parent.GetComponent<ViRMA_DimExplorerGroup>();
    }

    private void Update()
    {
        DimExBtnStateContoller();
    }

    // triggers for UI drumsticks
    //private void OnTriggerEnter(Collider triggeredCol)
    //{
    //    Debug.Log("on trigger enter dim explorer button");
    //    //if (triggeredCol.GetComponent<ViRMA_Drumstick>())
    //    //{
    //        globals.dimExplorer.hoveredTagBtn = gameObject;
    //    //}
    //}
    //private void OnTriggerExit(Collider triggeredCol)
    //{
    //    //if (triggeredCol.GetComponent<ViRMA_Drumstick>())
    //    //{
    //        if (globals.dimExplorer.hoveredTagBtn == gameObject)
    //        {
    //            globals.dimExplorer.hoveredTagBtn = null;
    //        }          
    //    //}
    //}

    public void OnPointerEnter(PointerEventData eventData)
    {
        Debug.Log("on pointer enter dimexplorerbutton");
        globals.dimExplorer.hoveredTagBtn = gameObject;
        Debug.Log("hoveredTagBtn verification: " + globals.dimExplorer.hoveredTagBtn);
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        Debug.Log("on pointer exit dimexplorerbutton");
        if (globals.dimExplorer.hoveredTagBtn == gameObject)
        {
            globals.dimExplorer.hoveredTagBtn = null;
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log("on pointer click dimexplorerbutton");
        LoadDimExContextMenu();
       
    }

    public void LoadDimExButton(Tag tag)
    {
        tagData = tag;

        gameObject.name = tagData.Label;

        textMesh.text = tagData.Label;

        textMesh.ForceMeshUpdate();

        float textWidth = textMesh.textBounds.size.x * 0.011f;
        float textHeight = textMesh.textBounds.size.y * 0.02f;

        Vector3 adjustScale = background.transform.localScale;
        adjustScale.x = textWidth;
        adjustScale.y = textHeight;
        background.transform.localScale = adjustScale;

        col.size = adjustScale;

        bgRend.GetPropertyBlock(matPropBlock);
        if (searchedForTag)
        {
            SetFocusedState();
        }
        else
        {
            SetDefaultState();
        }
        bgRend.SetPropertyBlock(matPropBlock);
    }
    public void LoadDimExContextMenu()
    {
        contextMenuActiveOnBtn = true;      

        GameObject contextMenu = new GameObject("DimExContextMenu");
        contextMenu.AddComponent<ViRMA_DimExplorerContextMenu>().tagData = tagData;

        contextMenu.transform.parent = transform;
        contextMenu.transform.localPosition = Vector3.zero;
        contextMenu.transform.localRotation = Quaternion.identity;

        contextMenu.AddComponent<Rigidbody>().useGravity = false;
        contextMenu.AddComponent<BoxCollider>().isTrigger = true;
        contextMenu.GetComponent<BoxCollider>().size = new Vector3(0.5f, 0.25f, 0.15f);
        contextMenu.GetComponent<BoxCollider>().center = new Vector3(col.center.x, col.center.y, (col.size.z * 10f / 2f) * -1);     
    }

    // button state controls
    private void DimExBtnStateContoller()
    {
        if (globals.dimExplorerActions.IsActive())
        {
            // clear border for focused states
            if (innerBackground)
            {
                Destroy(innerBackground);
                innerBackground = null;
            }
            Debug.Log("hoveredTagBtn");
            Debug.Log(globals.dimExplorer.hoveredTagBtn);
            // controls appearance of button in various states
            bgRend.GetPropertyBlock(matPropBlock);
            if (globals.dimExplorer.hoveredTagBtn == gameObject || searchedForTag || contextMenuActiveOnBtn)
            {
                Debug.Log("focused state");
                SetFocusedState();
            }
            else if (globals.dimExplorer.activeVerticalRigidbody == parentDimExGrp.dimExRigidbody)
            {
                SetHighlightState();
            }
            else
            {
                SetDefaultState();
            }

            // if collider on button is disabled, fade the buttons, unless it's context menu is active
            if (col.enabled == false && contextMenuActiveOnBtn == false)
            {
                SetFadedState();
            }
            bgRend.SetPropertyBlock(matPropBlock);
        }
    }
    public void SetDefaultState()
    {
        matPropBlock.SetColor("_Color", ViRMA_Colors.darkBlue);
        textMesh.color = Color.white;
    }
    public void SetHighlightState()
    {
        matPropBlock.SetColor("_Color", ViRMA_Colors.BrightenColor(ViRMA_Colors.darkBlue));
        textMesh.color = Color.white;
    }
    public void SetFocusedState()
    {
        if (innerBackground == null)
        {
            matPropBlock.SetColor("_Color", ViRMA_Colors.darkBlue);
            textMesh.color = ViRMA_Colors.DarkenColor(ViRMA_Colors.darkBlue);

            innerBackground = Instantiate(background, background.transform.parent);

            float borderThickness = innerBackground.transform.localScale.y * 0.1f;
            innerBackground.transform.localScale = new Vector3(innerBackground.transform.localScale.x - borderThickness, innerBackground.transform.localScale.y - borderThickness, innerBackground.transform.localScale.z - borderThickness);
            innerBackground.transform.localPosition = new Vector3(innerBackground.transform.localPosition.x, innerBackground.transform.localPosition.y, innerBackground.transform.localPosition.z - 0.003f);            
        }
    }
    public void SetFadedState()
    {
        Color colourToFade = matPropBlock.GetColor("_Color");
        colourToFade.a = 0.5f;
        matPropBlock.SetColor("_Color", colourToFade);

        textMesh.color = new Color(textMesh.color.r, textMesh.color.g, textMesh.color.b, 0.5f);
    }

}
