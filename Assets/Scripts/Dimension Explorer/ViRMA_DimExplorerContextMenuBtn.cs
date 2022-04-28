﻿using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using Valve.VR.InteractionSystem;

public class ViRMA_DimExplorerContextMenuBtn : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    private ViRMA_GlobalsAndActions globals;

    // assigned inside prefab
    public TextMeshPro textMesh;
    public BoxCollider col;
    public Renderer outerBgRend;
    public Renderer innerBgRend;
    public Color activeColor;
    public MaterialPropertyBlock outerBgPropBlock;
    public MaterialPropertyBlock innerBgPropBlock;

    // query info
    public Tag tagQueryData;
    public string axisQueryType;

    public OVRCameraRig m_CameraRig;

    public ViRMA_DimExplorer dimExplorer;

    private void Awake()
    {
        m_CameraRig = FindObjectOfType<OVRCameraRig>();
        globals = m_CameraRig.GetComponent<ViRMA_GlobalsAndActions>();

        outerBgPropBlock = new MaterialPropertyBlock();
        innerBgPropBlock = new MaterialPropertyBlock();

        dimExplorer = GameObject.Find("DimensionExplorer").GetComponent<ViRMA_DimExplorer>();
        Debug.Log(dimExplorer + "dimExplorerNullTest");
    }

    public void LoadDimExContextMenuBtn(string axisType)
    {
        axisQueryType = axisType;

        if (axisType == "filter")
        {
            textMesh.text = "Apply as Filter";
            activeColor = Color.black;          
        }
        else
        {
            textMesh.text = "Project to " + axisType + " Axis";
            if (axisType == "X")
            {
                activeColor = ViRMA_Colors.axisRed;
            }
            if (axisType == "Y")
            {
                activeColor = ViRMA_Colors.axisGreen;
            }
            if (axisType == "Z")
            {
                activeColor = ViRMA_Colors.axisBlue;
            }
        }

        textMesh.color = Color.white;

        outerBgRend.GetPropertyBlock(outerBgPropBlock);
        outerBgPropBlock.SetColor("_Color", activeColor);
        outerBgRend.SetPropertyBlock(outerBgPropBlock);

        innerBgRend.GetPropertyBlock(innerBgPropBlock);
        innerBgPropBlock.SetColor("_Color", activeColor);
        innerBgRend.SetPropertyBlock(innerBgPropBlock);
    }

    //private void OnTriggerEnter(Collider triggeredCol)
    //{
    //    //if (triggeredCol.GetComponent<ViRMA_Drumstick>())
    //    //{
    //        globals.dimExplorer.hoveredFilterBtn = gameObject;

    //        innerBgRend.GetPropertyBlock(innerBgPropBlock);
    //        innerBgPropBlock.SetColor("_Color", Color.white);
    //        innerBgRend.SetPropertyBlock(innerBgPropBlock);

    //        textMesh.color = activeColor;      
    //    //}
    //}
    //private void OnTriggerExit(Collider triggeredCol)
    //{
    //    //if (triggeredCol.GetComponent<ViRMA_Drumstick>())
    //    //{
    //        if (globals.dimExplorer.hoveredFilterBtn == gameObject)
    //        {
    //            globals.dimExplorer.hoveredFilterBtn = null;
    //        }

    //        innerBgRend.GetPropertyBlock(innerBgPropBlock);
    //        innerBgPropBlock.SetColor("_Color", activeColor);
    //        innerBgRend.SetPropertyBlock(innerBgPropBlock);

    //        textMesh.color = Color.white;            
    //    //}
    //}

    public void OnPointerEnter(PointerEventData eventData)
    {
        Debug.Log("on pointer enter dimexplorercontextmenubutton");
        globals.dimExplorer.hoveredFilterBtn = gameObject;

        innerBgRend.GetPropertyBlock(innerBgPropBlock);
        innerBgPropBlock.SetColor("_Color", Color.white);
        innerBgRend.SetPropertyBlock(innerBgPropBlock);

        textMesh.color = activeColor;
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        Debug.Log("on pointer exit dimexplorercontextmenubutton");
        if (globals.dimExplorer.hoveredFilterBtn == gameObject)
        {
            globals.dimExplorer.hoveredFilterBtn = null;
        }

        innerBgRend.GetPropertyBlock(innerBgPropBlock);
        innerBgPropBlock.SetColor("_Color", activeColor);
        innerBgRend.SetPropertyBlock(innerBgPropBlock);

        textMesh.color = Color.white;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log("project to axis " + gameObject);
        dimExplorer.SubmitContextBtnForQuery();
    }

}
