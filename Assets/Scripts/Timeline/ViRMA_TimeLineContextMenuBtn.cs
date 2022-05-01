﻿using TMPro;
using UnityEngine;
using Valve.VR.InteractionSystem;

public class ViRMA_TimeLineContextMenuBtn : MonoBehaviour
{
    private ViRMA_GlobalsAndActions globals;

    public GameObject targetTimelineChild;
    public string btnType;
    public OVRCameraRig m_CameraRig;


    // assigned inside prefab
    public TextMeshPro textMesh;
    public Renderer outerBgRend;
    public Renderer innerBgRend;
    public MaterialPropertyBlock outerBgPropBlock;
    public MaterialPropertyBlock innerBgPropBlock;

    private void Awake()
    {
        m_CameraRig = FindObjectOfType<OVRCameraRig>();
        globals = m_CameraRig.GetComponent<ViRMA_GlobalsAndActions>();

        outerBgPropBlock = new MaterialPropertyBlock();
        innerBgPropBlock = new MaterialPropertyBlock();
    }

    private void OnTriggerEnter(Collider triggeredCol)
    {
        if (triggeredCol.GetComponent<ViRMA_Drumstick>())
        {
            globals.timeline.hoveredContextMenuBtn = gameObject;

            innerBgRend.GetPropertyBlock(innerBgPropBlock);
            innerBgPropBlock.SetColor("_Color", Color.white);
            innerBgRend.SetPropertyBlock(innerBgPropBlock);
            textMesh.color = ViRMA_Colors.axisDarkBlue;
        }
    }
    private void OnTriggerExit(Collider triggeredCol)
    {
        if (triggeredCol.GetComponent<ViRMA_Drumstick>())
        {
            if (globals.timeline.hoveredContextMenuBtn = gameObject)
            {
                globals.timeline.hoveredContextMenuBtn = null;
            }

            innerBgRend.GetPropertyBlock(innerBgPropBlock);
            innerBgPropBlock.SetColor("_Color", ViRMA_Colors.axisDarkBlue);
            innerBgRend.SetPropertyBlock(innerBgPropBlock);
            textMesh.color = Color.white;
        }
    }
    public void LoadTimelineContextMenuBtn(string btnName, GameObject timelineChild)
    {
        btnType = btnName;
        targetTimelineChild = timelineChild;

        textMesh.text = btnName;
        textMesh.color = Color.white;

        outerBgRend.GetPropertyBlock(outerBgPropBlock);
        outerBgPropBlock.SetColor("_Color", ViRMA_Colors.axisDarkBlue);
        outerBgRend.SetPropertyBlock(outerBgPropBlock);

        innerBgRend.GetPropertyBlock(innerBgPropBlock);
        innerBgPropBlock.SetColor("_Color", ViRMA_Colors.axisDarkBlue);
        innerBgRend.SetPropertyBlock(innerBgPropBlock);
    }

}
