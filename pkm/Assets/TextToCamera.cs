using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextToCamera : MonoBehaviour
{
    Camera toLook;

    // Start is called before the first frame update
    void Start()
    {
        toLook = Camera.main;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        transform.LookAt(toLook.transform);
        transform.rotation = Quaternion.LookRotation(toLook.transform.forward);
    }
}
