using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestPrefabInstantiation : MonoBehaviour
{

    // Reference to the Prefab. Drag a Prefab into this field in the Inspector.
    public GameObject buttonPrefab;
    public GameObject panelToAttachButtonsTo;


    // This script will simply instantiate the Prefab when the game starts.
    void Start()
    {


        int rowGap = -20;
        int columnLeft = 45;
        int columnRight = 120;
        int column = 0;

        for (int i = 1; i < 16; i++)
        {
            GameObject button = (GameObject)Instantiate(buttonPrefab);
            button.transform.SetParent(panelToAttachButtonsTo.transform);//Setting button parent



            if(i % 2 == 0) {
                button.transform.localPosition = new Vector3(columnRight, -20*column, 0);

            } else
            {
                button.transform.localPosition = new Vector3(columnLeft, -20* column, 0);
                column += 1;
            }

            button.transform.localScale = new Vector3(0.4f, 0.4f, 0);
        }


    }

}
