using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PetControls : MonoBehaviour
{
    //Follow followScript;
    Stats stats;
    public float runSpeed = 5;
    //Transform target;

    Vector2 moveInput;
    private Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        //followScript.GetComponent<Follow>();
        stats.GetComponent<Stats>();
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        /*if (canControl == true)
        {
            runSpeed = (stats.spd / 10);
            runSpeed = Mathf.Clamp(runSpeed, 2, 35);

            //Vector3 horizontal = new Vector3(Input.GetAxis("Horizontal"), 0.0f, 0.0f);
            //transform.position = transform.position + horizontal * runSpeed;

            //Vector3 vertical = new Vector3(0.0f, 0.0f, Input.GetAxis("Vertical"));
            //transform.position = transform.position + vertical * runSpeed;

            //transform.LookAt(target);
        }*/
        //runSpeed = stats.spd;
        Run();
    }
    //try different variable names
    void Run()
    {
        Vector3 playerVelocity = new Vector3(moveInput.x * runSpeed, rb.velocity.y, moveInput.y * runSpeed);
        rb.velocity = transform.TransformDirection(playerVelocity);
    }

    private void OnMove(InputValue value)
    {
        moveInput = value.Get<Vector2>();
    }

    /*public void GetTarget(Transform targetPos)
    {
        target = targetPos;
    }*/
}
