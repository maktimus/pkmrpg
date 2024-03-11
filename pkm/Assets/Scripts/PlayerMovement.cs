using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    Stats stats;
    public float runSpeed;
    public int size; //1 - 3, 1 being small and 3 being big. For footsteps

    private Rigidbody rb;
    Vector2 moveInput;
    public GameObject Char;
    public Animator animator;
    public List<AudioClip> footstepSounds;
    private FootstepSwapper swapper;
    private AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        swapper = GetComponent<FootstepSwapper>();
        rb = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
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

    public void SwapFootsteps(FootstepCollection collection)
    {
        footstepSounds.Clear();
        for (int i = 0; i < collection.footstepSounds.Count; i++)
        {
            footstepSounds.Add(collection.footstepSounds[i]);
        }

    }

    private void PlayFootStep()
    {
        Debug.Log("Passed 0");
        swapper.CheckLayers();

        int n = Random.Range(1, footstepSounds.Count);
        audioSource.clip = footstepSounds[n];
        //audioSource.PlayOneShot(audioSource.clip);
        if(size == 1)
        {
            audioSource.volume = Random.Range(0.02f, 0.05f);
            audioSource.pitch = Random.Range(2.5f, 3.0f);
        }
        else if (size == 2)
        {
            audioSource.volume = Random.Range(0.08f, 0.11f);
            audioSource.pitch = Random.Range(1.4f, 1.8f);
        }
        else if (size == 3)
        {
            audioSource.volume = Random.Range(0.14f, 0.17f);
            audioSource.pitch = Random.Range(0.5f, 0.8f);
        }
        audioSource.Play();

        footstepSounds[n] = footstepSounds[0];
        footstepSounds[0] = audioSource.clip;
        Debug.Log("Source Got");

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
