using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ControllerChecker : MonoBehaviour
{
    public GameObject pet;
    public PlayerInput playerController;
    public PlayerInput petController;
    [SerializeField] bool playerActive = true;

    Follow followScript;

    void Start()
    {
        followScript = pet.GetComponent<Follow>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift))
            {
                SwitchControls();
            }
    }

    public void SwitchControls()
    {
        if (playerActive == true)
        {
            playerController.enabled = false;
            petController.enabled = true;
            playerActive = false;
            Debug.Log("does something");
        }
        else
        {
            playerController.enabled = true;
            petController.enabled = false;
            playerActive = true;
        }
    }
}
