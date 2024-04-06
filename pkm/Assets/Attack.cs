using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
namespace Skills
{
    public class Attack : MonoBehaviour
    {
        #region Variables
        Animator anim;
        bool attackAgain = true;
        [Header("Collision Check")]
        public Transform[] atkPoint;
        public LayerMask enemyLayer;
        public float atkRange = 0.5f;

        [Header("Base Crit")]
        public float critChance;

        Stats stats;
        int direction;

        [Header("Move List")]
        public List<Skills> MonSkills = new List<Skills>();
        #endregion

        void Start()
        {
            anim = GetComponent<Animator>();
            stats = GetComponent<Stats>();
        }

        private void AttackDir(int dir)
        {
            //working but need to fix diagonals, stays on diagonal values on occasions
            direction = dir;
            Debug.Log(dir);
        }

        private void OnFire(InputValue inputValue)
        {
            if (attackAgain == true)
            {

                anim.ResetTrigger("StopAttack");

                anim.SetTrigger("Attack");
                attackAgain = false;

                Collider[] hitEnemies = Physics.OverlapSphere(atkPoint[direction].position, atkRange, enemyLayer);

                foreach (Collider enemy in hitEnemies)
                {
                    Debug.Log("We hit " + enemy.name);
                    Enemy _enemy = enemy.GetComponent<Enemy>();
                    Damage(30, _enemy.def, _enemy);
                }

                Debug.Log("Attacking Direction = " + atkPoint[direction]);

                //damage
            }
            else
            {
                return;
            }
            //stop movement when attacking
        }

        private void OnSpecial(InputValue inputValue)
        {

            anim.ResetTrigger("StopAttack");

            anim.SetTrigger("Attack");
            attackAgain = false;

            Collider[] hitEnemies = Physics.OverlapSphere(atkPoint[direction].position, atkRange, enemyLayer);

            foreach (Collider enemy in hitEnemies)
            {
                Debug.Log("We hit " + enemy.name);
                Enemy _enemy = enemy.GetComponent<Enemy>();
                Damage(80, _enemy.def, _enemy);
            }
        }

        public void NotAttacking()
        {
            anim.ResetTrigger("Attack");
            anim.SetTrigger("StopAttack");


            if (anim.GetInteger("AttackNumber") == 1)
            {
                int test = anim.GetInteger("AttackNumber");
                anim.SetInteger("AttackNumber", 2);
            }
            else if (anim.GetInteger("AttackNumber") == 2)
            {
                int test = anim.GetInteger("AttackNumber");
                anim.SetInteger("AttackNumber", 1);
            }

            attackAgain = true;
        }

        public void Damage(int _attackPower, int enemyDef, Enemy enemy)
        {
            float randValue = Random.Range(1.0f, 100f);
            int damage;

            if (randValue < critChance)
            {
                damage = Mathf.RoundToInt(((((2 * stats.level / 5 + 2) * stats.atk * _attackPower / enemyDef) / 50) + 2 /*stab*weakness/resistance*/ * 100 / 100) * 2);
                Debug.Log("Crit!");
            }
            else
            {

                damage = Mathf.RoundToInt((((2 * stats.level / 5 + 2) * stats.atk * _attackPower / enemyDef) / 50) + 2 /*stab*weakness/resistance*/ * 100 / 100);
            }

            enemy.Damage(damage);
        }

        private void OnDrawGizmosSelected()
        {
            for (int i = 0; i < atkPoint.Length; i++)
            {
                Gizmos.DrawWireSphere(atkPoint[i].position, atkRange);
            }

        }
    }
}
