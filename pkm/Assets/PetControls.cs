using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PetControls : MonoBehaviour
{
    Follow followScript;
    Stats stats;
    float runSpeed;
    public bool canControl = false;
    Transform target;
    // Start is called before the first frame update
    void Start()
    {
        followScript.GetComponent<Follow>();
        stats.GetComponent<Stats>();
    }

    // Update is called once per frame
    void Update()
    {
        if (canControl == true)
        {
            runSpeed = (stats.spd / 10);
            runSpeed = Mathf.Clamp(runSpeed, 2, 35);

            //Vector3 horizontal = new Vector3(Input.GetAxis("Horizontal"), 0.0f, 0.0f);
            //transform.position = transform.position + horizontal * runSpeed;

            //Vector3 vertical = new Vector3(0.0f, 0.0f, Input.GetAxis("Vertical"));
            //transform.position = transform.position + vertical * runSpeed;

            //transform.LookAt(target);
        }
        runSpeed = (stats.spd / 10);
        runSpeed = Mathf.Clamp(runSpeed, 2, 35);

        Vector3 horizontal = new Vector3(Input.GetAxis("Horizontal"), 0.0f, 0.0f);
        transform.position = transform.position + horizontal * runSpeed;

        Vector3 vertical = new Vector3(0.0f, 0.0f, Input.GetAxis("Vertical"));
        transform.position = transform.position + vertical * runSpeed;

        //https://docs.unity3d.com/Packages/com.unity.inputsystem@1.0/manual/Components.html#:~:text=To%20disable%20a%20player's%20input,ActivateInput%20.
    }

    public void GetTarget(Transform targetPos)
    {
        target = targetPos;
    }
}
