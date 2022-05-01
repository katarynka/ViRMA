using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ViRMA_SpawnInFrontOfCamera : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(LateStart(1));
    }

    IEnumerator LateStart(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        transform.position = Camera.main.transform.TransformPoint(Vector3.forward * 0.5f) + new Vector3(-0.2f, 0.1f, 0);
        transform.rotation = Camera.main.transform.rotation;
    }

    // Update is called once per frame
    void Update()
    {

    }
}
