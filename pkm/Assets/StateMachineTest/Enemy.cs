using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour, EnemyStats, EnemyMove
{
    #region Variablles
    public Rigidbody rb { get; set; }

    public Slider hpBar { get; set; }

    public TextMeshPro info { get; set; }

    [field: SerializeField] public string _name { get; set; }

    [field: SerializeField] public Bounds bounds;
    [SerializeField] TextMeshProUGUI damageNum;
    [SerializeField] GameObject damage;
    #endregion

    #region stat
    [field: SerializeField] public int baseAtk { get; set; }
    [field: SerializeField] public int baseSpa { get; set; }
    [field: SerializeField] public int baseDef { get; set; }
    [field: SerializeField] public int baseSpdef { get; set; }
    [field: SerializeField] public int baseSpd { get; set; }
    [field: SerializeField] public int baseHp { get; set; }
    [field: SerializeField] public int atk { get; set; }
    [field: SerializeField] public int spa { get; set; }
    [field: SerializeField] public int def { get; set; }
    [field: SerializeField] public int spdef { get; set; }
    [field: SerializeField] public int spd { get; set; }
    [field: SerializeField] public int hp { get; set; }
    [field: SerializeField] public int level { get; set; }
    [field: SerializeField] public int maxHP { get; set; }
    [field: SerializeField] public int moveSpd { get; set; }
    #endregion

    #region Calculate Stats
    void SetStats()
    {
        Debug.Log("Stats calculating");
        //stat calculations
        maxHP = Mathf.RoundToInt(0.01f * (2 * baseHp + 31 + Mathf.Floor(0.25f * 256.0f)) * level + 10);
        atk = Mathf.RoundToInt(Mathf.Floor(0.01f * (2 * baseAtk + 31 + Mathf.Floor(0.25f * 256f)) * level) + 5);
        spa = Mathf.RoundToInt(Mathf.Floor(0.01f * (2 * baseSpa + 31 + Mathf.Floor(0.25f * 256f)) * level) + 5);
        spdef = Mathf.RoundToInt(Mathf.Floor(0.01f * (2 * baseSpdef + 31 + Mathf.Floor(0.25f * 256f)) * level) + 5);
        def = Mathf.RoundToInt(Mathf.Floor(0.01f * (2 * baseDef + 31 + Mathf.Floor(0.25f * 256f)) * level) + 5);
        spd = Mathf.RoundToInt(Mathf.Floor(0.01f * (2 * baseSpd + 31 + Mathf.Floor(0.25f * 256f)) * level) + 5);
        moveSpd = spd / 3;

        hp = maxHP;

        //sets hp UI bar max value to = its max hp
        hpBar.maxValue = maxHP;
    }

    public void SetHponSpawn()
    {
        hp = maxHP;
        hpBar.maxValue = maxHP;
        Debug.Log("Bounds got");
    }
    #endregion

    #region Damage + Death
    public void Damage(int damageAmount)
    {
        hp -= damageAmount;

        damageNum.text = damageAmount.ToString();
        damage.SetActive(true);
        if (hp <= 0)
        {
            Die();
        }
    }

    public void Die()
    {
        Destroy(gameObject);
    }
    #endregion

    #region State Machine Variables

    public EnemyStateMachine stateMachine { get; set; }
    public EnemyIdle idleState { get; set; }
    public EnemyChase chaseState { get; set; }
    public EnemyAttack attackState { get; set; }


    #endregion

    #region Idle State Variables
    public float randMovementRange = 5f;
    public float randMovementSpeed = 1f;
    #endregion

    #region Update Info
    public void UpdateInfo()
    {
        info.text = "Level: " + level.ToString() + " " + _name;
    }
    #endregion

    #region Skills Variable

    #endregion

    private void Awake()
    {
        stateMachine = new EnemyStateMachine();

        idleState = new EnemyIdle(this, stateMachine);
        chaseState = new EnemyChase(this, stateMachine);
        attackState = new EnemyAttack(this, stateMachine);

        //skillList.GetC
    }
    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        hpBar = GetComponentInChildren<Slider>();
        info = GetComponentInChildren<TextMeshPro>();
        //damage = GetComponentsInChildren <TextMeshPro>();

        SetStats();
        UpdateInfo();

        stateMachine.Initialise(idleState);
    }

    private void Update()
    {
        stateMachine.currentEnemyState.FrameUpdate();
        hpBar.value = hp; //move this to when taking damage after
    }
    private void FixedUpdate()
    {
        //stateMachine.currentEnemyState.PhysicsUpdate(); 
    }

    public void AnimationTriggerEvent(AnimationTriggerType triggerType)
    {
        stateMachine.currentEnemyState.AnimationTriggerEvent(triggerType);
    }

    public void MoveEnemy(Vector3 velocity)
    {
        rb.velocity = velocity;
    }

    public enum AnimationTriggerType
    {
        EnemyDamaged,
        PlayFootstepSound
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Spawn")
        {
            bounds = other.bounds;
        }

    }
}
