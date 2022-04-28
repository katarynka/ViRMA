using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ViRMA_GlobalGrabStatus : MonoBehaviour
{
    public bool grabStatus;
    public GameObject grabbedObject;
    // Start is called before the first frame update
    void Start()
    {
        grabStatus = false;
        grabbedObject = null;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
