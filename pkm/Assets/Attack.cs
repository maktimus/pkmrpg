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
        PokemonHUD pkmHUD;
        Pokemon pokemon;
        #endregion

        bool canAttack = true;
        float cdTimer = 0.0f;

        void Start()
        {
            anim = GetComponent<Animator>();
            stats = GetComponent<Stats>();
            pkmHUD = GetComponent<PokemonHUD>();

            pokemon = pkmHUD.Pokemon;
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
        }

        private void Update()
        {
            if(!canAttack)
            {
                cdTimer -= Time.deltaTime;
                Debug.Log(cdTimer);
                if(cdTimer <= 0.0f)
                {
                    canAttack = true;
                }
            }
        }

        private void OnSpecial(InputValue inputValue)
        {
            float cdDuration = pkmHUD.sp1.CdTime; //the set cooldown of the move i.e 15secoonds
            //float lastAttackTime;

            if (canAttack)
            {
                anim.ResetTrigger("StopAttack");

                anim.SetTrigger("Attack");

                cdTimer = cdDuration;
                canAttack = false;

                Collider[] hitEnemies = Physics.OverlapSphere(atkPoint[direction].position, atkRange, enemyLayer);

                foreach (Collider enemy in hitEnemies)
                {
                    Debug.Log("Sableye used " + pkmHUD.sp1.Name + pkmHUD.sp1.Power);
                    int Power = pkmHUD.sp1.Power;

                    PokemonHUD _enemy = enemy.GetComponent<PokemonHUD>();
                    _enemy.Pokemon.TakeDamage(Power, pokemon);
                    _enemy.UpdateHP();

                    Debug.Log("Enemy take damage");

                }
            }
            else
            {
                Debug.Log("Cooldown");
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
