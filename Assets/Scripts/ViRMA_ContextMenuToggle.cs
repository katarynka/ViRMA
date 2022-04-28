using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ViRMA_ContextMenuToggle : MonoBehaviour
{
    public bool projectionMenuEnabled = true; // if true, the clearFilters menu is going to take precedence over the context menu.
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ProjectionMenuEnabled(bool status)
    {
        projectionMenuEnabled = status;
    }
}
