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
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void SpawnScrollMenu()
    {
        gameObject.transform.position = Camera.main.transform.position + new Vector3(0.4f, 0, 0.3f);
        gameObject.transform.rotation = Camera.main.transform.rotation;
    }
}
