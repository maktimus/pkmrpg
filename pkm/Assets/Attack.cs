using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Attack : MonoBehaviour
{
    Animator anim;
    bool attackAgain = true;
    public Transform[] atkPoint;
    public LayerMask enemyLayer;
    public float atkRange = 0.5f;

    int direction;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
    }

    private void AttackDir(int dir)
    {
        //working but need to fix diagonals, stays on diagonal values on occasions
        direction = dir;
        Debug.Log(dir);
    }

    private void OnFire(InputValue inputValue)
    {
        if(attackAgain == true)
        {

            anim.ResetTrigger("StopAttack");

            anim.SetTrigger("Attack");
            attackAgain = false;

            Collider[] hitEnemies = Physics.OverlapSphere(atkPoint[direction].position, atkRange, enemyLayer);

            foreach(Collider enemy in hitEnemies)
            {
                Debug.Log("We hit " + enemy.name);
            }

            Debug.Log("Attacking Direction = " + atkPoint[direction]);

            //damage
        }
        //stop movement when attacking
    }

    public void NotAttacking()
    {
        anim.ResetTrigger("Attack");
        anim.SetTrigger("StopAttack");

        
        if(anim.GetInteger("AttackNumber") == 1)
        {
            int test = anim.GetInteger("AttackNumber");
            anim.SetInteger("AttackNumber", 2);
        }
        else if(anim.GetInteger("AttackNumber") == 2)
        {
            int test = anim.GetInteger("AttackNumber");
            anim.SetInteger("AttackNumber", 1);
        }

        attackAgain = true;
    }

    private void OnDrawGizmosSelected()
    {
        for (int i = 0; i < atkPoint.Length; i++)
        {
            Gizmos.DrawWireSphere(atkPoint[i].position, atkRange);
        }

    }
}
