using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    Stats stats;
    public float runSpeed;

    private Rigidbody rb;
    Vector2 moveInput;
    public GameObject Char;
    public Animator animator;
  
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        if(this.CompareTag("Pet"))
        {
            animator = GetComponent<Animator>();
            stats = GetComponent<Stats>();
            runSpeed = stats.moveSpd;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(this.CompareTag("Pet"))
        {
            runSpeed = stats.moveSpd;
        }
        Run();
    }

    private void LateUpdate()
    {
        
    }

    void Run()
    {
        Vector3 playerVelocity = new Vector3(moveInput.x * runSpeed, rb.velocity.y, moveInput.y * runSpeed);
        rb.velocity = transform.TransformDirection(playerVelocity);
    }

    private void OnMove(InputValue value)
    {
        moveInput = value.Get<Vector2>();
        animator.SetFloat("X", moveInput.x);
        animator.SetFloat("Y", moveInput.y);
        animator.SetFloat("Speed", moveInput.sqrMagnitude);

        if(moveInput.x == 1 || moveInput.x == -1)
        {
            animator.SetFloat("lastX", moveInput.x);
            
        }
        if(moveInput.y == 1 || moveInput.y == -1)
        {
            animator.SetFloat("lastY", moveInput.y);
        }
        else if (moveInput.x == 1 && moveInput.y == 0 || moveInput.x == -1 && moveInput.y == 0 || moveInput.y == 1 && moveInput.x == 0 || moveInput.y == -1 && moveInput.x == 0 || moveInput.x == 1 && moveInput.y == 1 || moveInput.x == 1 && moveInput.y == -1 || moveInput.x == -1 && moveInput.y == 1 || moveInput.x == -1 && moveInput.y == -1)
        {
            animator.SetFloat("lastY", moveInput.y);
            animator.SetFloat("lastX", moveInput.x);
        }
    }


}
