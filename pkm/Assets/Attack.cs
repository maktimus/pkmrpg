using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Attack : MonoBehaviour
{
    Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
    }

    private void OnFire(InputValue inputValue)
    {
        anim.ResetTrigger("StopAttack");

        anim.SetTrigger("Attack");

        //stop movement when attacking
        Debug.Log("Attacking");
    }

    public void NotAttacking()
    {
        anim.ResetTrigger("Attack");
        anim.SetTrigger("StopAttack");

        
        if(anim.GetInteger("AttackNumber") == 1)
        {
            int test = anim.GetInteger("AttackNumber");
            anim.SetInteger("AttackNumber", 2);
            Debug.Log(test);
        }
        else if(anim.GetInteger("AttackNumber") == 2)
        {
            int test = anim.GetInteger("AttackNumber");
            anim.SetInteger("AttackNumber", 1);
            Debug.Log(test);
        }
    }
}
