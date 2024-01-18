using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    public float runSpeed;
    Follow followScript;
    //will need to be an array or list when implementing a party of pets. Same for other revelant scripts
    public GameObject pet;

    // Start is called before the first frame update
    void Start()
    {
        followScript = pet.GetComponent<Follow>();
    }

    // Update is called once per frame
    void Update()
    {
        if(followScript.battle == true && this.tag == "Player")
        {
            runSpeed = 0;
        }
        else
        {
            runSpeed = 0.05f;
        }
        /*
        if (followScript.battle == true && this.tag == "Pet")
        {
            runSpeed = 0;
        }
        else
        {
            runSpeed = 0.05f;
        }*/

        Vector3 horizontal = new Vector3(Input.GetAxis("Horizontal"), 0.0f, 0.0f);
        transform.position = transform.position + horizontal * runSpeed;

        Vector3 vertical = new Vector3( 0.0f, 0.0f, Input.GetAxis("Vertical"));
        transform.position = transform.position + vertical * runSpeed;
    }

}
