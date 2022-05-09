using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ViRMA_ColourClearButtons : MonoBehaviour
{
    public GameObject clearX;
    public GameObject clearY;
    public GameObject clearZ;
    public GameObject clearDirect;
    private Toggle toggleX;
    private Toggle toggleY;
    private Toggle toggleZ;
    private Toggle toggleD;
    private Color axisRed = new Color(255, 0, 0, 177);
    private Color axisGreen = new Color(0, 245, 10, 181);
    private Color axisBlue = new Color(13, 29, 248, 197);
    private Color axisBlack = new Color(0, 0, 0, 255);
    // Start is called before the first frame update
    void Start()
    {
        toggleX = clearX.GetComponent<Toggle>();
        toggleY = clearY.GetComponent<Toggle>();
        toggleZ = clearZ.GetComponent<Toggle>();
        toggleD = clearDirect.GetComponent<Toggle>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ColourButton(string button)
    {
        Debug.Log("color button called");
        if(button == "X")
        {
            ColorBlock cbX = toggleX.colors;
            cbX.normalColor = axisRed;
            cbX.highlightedColor = axisRed;
            cbX.pressedColor = axisRed;
            cbX.selectedColor = axisRed;
            toggleX.colors = cbX;
        }
        else if (button == "Y")
        {
            ColorBlock cbY = toggleY.colors;
            cbY.normalColor = ViRMA_Colors.axisGreen;
            //cbY.highlightedColor = axisGreen;
            //cbY.pressedColor = axisGreen;
            //cbY.selectedColor = axisGreen;
            toggleY.colors = cbY;
            Debug.Log("colors " + toggleY.colors.normalColor);
        }
        else if (button == "Z")
        {
            ColorBlock cbZ = toggleZ.colors;
            cbZ.normalColor = ViRMA_Colors.axisBlue;
            //cbZ.highlightedColor = axisBlue;
            //cbZ.pressedColor = axisBlue;
            //cbZ.selectedColor = axisBlue;
            toggleZ.colors = cbZ;
        }
        else
        {
            ColorBlock cbD = toggleD.colors;
            cbD.normalColor = axisBlack;
            cbD.highlightedColor = axisBlack;
            cbD.pressedColor = axisBlack;
            cbD.selectedColor = axisBlack;
            toggleD.colors = cbD;
        }
    }
}
