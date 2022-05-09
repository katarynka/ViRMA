using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ViRMA_NaiveGrabbable : MonoBehaviour
{
    public GameObject rightControllerModel;
    public GameObject rightHand;
    private Vector2 thumbstickAxes;
    private float triggerAxis;
    private bool offsetSet;
    private Quaternion currentRot;
    public GameObject controller;
    public Quaternion addition;
    private Vector3 startPos;
    public bool allowThumbstickManipulation;
    public bool snapToController;
    private GameObject poke;
    private GameObject pokeFinger;
    public bool faceRotation;
    public OVRCameraRig m_CameraRig;
    public ViRMA_GlobalGrabStatus virmaGrabStatus;
    private bool isGrabbed = false;
    public bool allowHandGrab = true;

    public float activationDistance;

    // Start is called before the first frame update
    void Start()
    {
        m_CameraRig = FindObjectOfType<OVRCameraRig>();
        virmaGrabStatus = m_CameraRig.GetComponent<ViRMA_GlobalGrabStatus>();
    }

    // Update is called once per frame
    void Update()
    {
        PokeRenderer();

        StartGrab();

        ControllerModelRendering();


    }

    void StartGrab()
    {
        if (OVRInput.Get(OVRInput.Button.SecondaryIndexTrigger) && IsCloseEnough() && OVRInput.GetActiveController() == OVRInput.Controller.Touch)
        {
            if (virmaGrabStatus.grabbedObject == null || virmaGrabStatus.grabbedObject == gameObject)
            {
                //virmaGrabStatus.grabStatus = true;
                virmaGrabStatus.grabbedObject = gameObject;

                poke = GameObject.Find("PokeLocation");
                // Set thumbstick and trigger axes - these must remain in Update()
                thumbstickAxes = OVRInput.Get(OVRInput.Axis2D.SecondaryThumbstick);
                triggerAxis = OVRInput.Get(OVRInput.Axis1D.SecondaryHandTrigger);
                Vector3 position = poke.transform.position;
                poke.GetComponent<SphereCollider>().enabled = false;
                poke.GetComponent<MeshRenderer>().enabled = false;

                transform.position = position + new Vector3(0.015f, 0.015f, 0.015f);

                if (allowThumbstickManipulation)
                {
                    ControllerRotation();
                }
                else
                {
                    // Set rotation of grabbed object - based on PokeLocation
                    Quaternion rotation = poke.transform.rotation;
                    rotation *= addition;
                    transform.rotation = rotation;

                    // ScrollableMenu additive rotation
                    /*(-42f, -10, 180);*/
                }

            }            
            else
            {
                virmaGrabStatus.grabbedObject = null;
            }
        }
        else if (allowHandGrab && OVRInput.GetActiveController() == OVRInput.Controller.Hands)
        {
            if (OVRInput.Get(OVRInput.Button.Three) && IsCloseEnough())
            {
                if (virmaGrabStatus.grabbedObject == null || virmaGrabStatus.grabbedObject == gameObject)
                {
                    //virmaGrabStatus.grabStatus = true;
                    virmaGrabStatus.grabbedObject = gameObject;

                    GameObject parent = GameObject.Find("HandPokeInteractorLeft");

                    pokeFinger = GetChildWithName(parent, "HandIndexFingertip");

                    Vector3 position = pokeFinger.transform.position;
                    transform.position = position + new Vector3(0.035f, 0.035f, 0.035f);
                    if (faceRotation)
                    {
                        transform.rotation = Camera.main.transform.rotation;
                    }

                }
                else
                {
                    virmaGrabStatus.grabbedObject = null;
                }
            }
        }
    }

    void PokeRenderer()
    {
        poke = GameObject.Find("PokeLocation");
        if (OVRInput.GetDown(OVRInput.Button.SecondaryIndexTrigger))
        {
            poke.GetComponent<SphereCollider>().enabled = false;
            poke.GetComponent<MeshRenderer>().enabled = false;
        }
        if (OVRInput.GetUp(OVRInput.Button.SecondaryIndexTrigger))
        {
            poke.GetComponent<SphereCollider>().enabled = true;
            poke.GetComponent<MeshRenderer>().enabled = true;
        }

    }

    bool IsCloseEnough()
    {
        OVRInput.Controller controllingHand;
        if (OVRInput.GetActiveController() == OVRInput.Controller.Hands)
        {
            controllingHand = OVRInput.Controller.LHand;
        }
        else { controllingHand = OVRInput.Controller.RTouch; }

        if (Mathf.Abs(Vector3.Distance(OVRInput.GetLocalControllerPosition(controllingHand), transform.position)) < activationDistance)
            return true;

        return false;
    }

    void Rotate()
    {
        SetOffsets();

        Vector3 closestPoint = Vector3.Normalize(controller.transform.position - transform.position);
        var rot = Quaternion.FromToRotation(startPos, closestPoint);
        rot = Quaternion.Euler(0, rot.eulerAngles.y, 0);
        transform.rotation = rot * currentRot;
    }

    // Controls rendering of controller (disabled if grab or hands are activated)
    void ControllerModelRendering()
    {
        if (rightHand.activeSelf || OVRInput.Get(OVRInput.Button.SecondaryIndexTrigger))
            rightControllerModel.SetActive(false);
        else
            rightControllerModel.SetActive(true);
    }


    void ControllerRotation()
    {
        Debug.Log(triggerAxis + "Rotation TRIG");
        Debug.Log(thumbstickAxes + "Rotation THUMB");
        //transform.position = new Vector3(transform.position.x, transform.position.y, controller.transform.position.z + 1f); Position adjustment - not functional

        if (thumbstickAxes.y > 0.5)
        {
            float scaleRate = /*0.00005f + */thumbstickAxes.y * 0.0001f;
            transform.localScale = new Vector3(transform.localScale.x - scaleRate, transform.localScale.y - scaleRate, transform.localScale.z - scaleRate);
        }


        if (thumbstickAxes.y < -0.5)
        {
            float scaleRate = /*0.00005f*/ -thumbstickAxes.y * 0.0001f;
            transform.localScale = new Vector3(transform.localScale.x + scaleRate, transform.localScale.y + scaleRate, transform.localScale.z + scaleRate);
        }

        if (thumbstickAxes.x > 0.5)
        {
            Debug.Log("ROTATION");
            float rotationRate = /*0.00005f + */thumbstickAxes.x;
            Vector3 v = new Vector3(transform.eulerAngles.x, transform.localRotation.eulerAngles.y - rotationRate, transform.eulerAngles.z);
            transform.localRotation = Quaternion.Euler(v);
        }

        if (thumbstickAxes.x < -0.5)
        {
            float rotationRate = /*0.00005f*/ thumbstickAxes.x;
            Vector3 v = new Vector3(transform.eulerAngles.x, transform.localRotation.eulerAngles.y - rotationRate, transform.eulerAngles.z);
            transform.localRotation = Quaternion.Euler(v);


        }
    }

    void SetOffsets()
    {
        if (offsetSet)
            return;

        startPos = Vector3.Normalize(controller.transform.position - transform.position);
        currentRot = transform.rotation;

        offsetSet = true;
    }


    GameObject GetChildWithName(GameObject obj, string name)
    {
        Transform trans = obj.transform;
        Transform childTrans = trans.Find(name);
        if (childTrans != null)
        {
            return childTrans.gameObject;
        }
        else
        {
            return null;
        }
    }

}
