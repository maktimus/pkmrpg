using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Stats : MonoBehaviour
{
    [SerializeField]
    int baseAtk, baseSpa, baseDef, baseSpdef, baseSpd, baseHp;
    public int atk, spa, spdef, def, spd, hp, maxHP, moveSpd;
    public int level, exp, enemyLvl;
        
    public bool isFast = false, isMFast = false, isMSlow = false, isSlow = false;
    public int expCurve;
    int expGain;

    public Slider expBar, hpBar;

    AnimController levelUpAnim;
    [SerializeField]
    MonHeader monInfo;
    // Start is called before the first frame update
    void Start()
    {
        GetExpCurve();
        GetStats();
        monInfo = this.GetComponentInChildren<MonHeader>();
        UpdateInfo();
        levelUpAnim = this.GetComponentInChildren<AnimController>();
        hp = maxHP;
    }

    //determines how fast it levels up by calculating how much exp is needed to reach next level
    void GetExpCurve()
    {
        if (isFast == true)
        {
            expCurve = Mathf.RoundToInt(0.8f * (Mathf.Pow(level, 3)));
        }
        else if (isMFast == true)
        {
            expCurve = Mathf.RoundToInt(Mathf.Pow(level, 3));
        }
        else if (isMSlow == true)
        {
            expCurve = Mathf.RoundToInt(1.2f * (Mathf.Pow(level, 3)) - 15 * (Mathf.Pow(level, 2)) + 100 * (level) - 140);
            if (expCurve <= 0)
            {
                expCurve = 0;
            }
        }
        else if (isSlow == true)
        {
            expCurve = Mathf.RoundToInt(1.25f * (Mathf.Pow(level, 3)));
        }

    }
    //updates their stats
    void GetStats()
    {
        //stat calculations
        maxHP = Mathf.RoundToInt(0.01f * (2 * baseHp + 31 + Mathf.Floor(0.25f * 256.0f)) * level + 10);
        atk = Mathf.RoundToInt(Mathf.Floor(0.01f * (2 * baseAtk + 31 + Mathf.Floor(0.25f * 256f)) * level) + 5);
        spa = Mathf.RoundToInt(Mathf.Floor(0.01f * (2 * baseSpa + 31 + Mathf.Floor(0.25f * 256f)) * level) + 5);
        spdef = Mathf.RoundToInt(Mathf.Floor(0.01f * (2 * baseSpdef + 31 + Mathf.Floor(0.25f * 256f)) * level) + 5);
        def = Mathf.RoundToInt(Mathf.Floor(0.01f * (2 * baseDef + 31 + Mathf.Floor(0.25f * 256f)) * level) + 5);
        spd = Mathf.RoundToInt(Mathf.Floor(0.01f * (2 * baseSpd + 31 + Mathf.Floor(0.25f * 256f)) * level) + 5);
        moveSpd = spd / 3;

        //sets hp UI bar max value to = its max hp
        hpBar.maxValue = maxHP;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            GainExp(enemyLvl, 0);
            
        }
        if (Input.GetKeyDown(KeyCode.Q))
        {
            hp -= 3;

        }
        //sets the maximum and minimum value for their health
        hp = Mathf.Clamp(hp, 0, maxHP);

        //sets the values of each bar
        expBar.maxValue = expCurve;     
        expBar.value = exp;
        hpBar.value = hp;
        hpBar.maxValue = maxHP;

        if(gameObject.tag == "Enemy")
        {
            if (hp <= 0)
            {
                Debug.Log("should be destroyed");
                CheckSpawn spawn = gameObject.GetComponent<CheckSpawn>();
                spawn.ReduceSpawn();
                Destroy(gameObject);
            }
        }

    }

    //exp gain
    public void GainExp(int enemyLevel, int remainder)
    {
        int a = 10 + (enemyLevel * 2);
        int b = (enemyLevel * 160) / 5;
        int c = (enemyLevel + level + 10);
        //calculates how much exp we get from enemies
        int gain = Mathf.RoundToInt(Mathf.Floor(Mathf.Sqrt(a) * (a * a)) * (b * 1.5f) / Mathf.Floor(Mathf.Sqrt(c) * (c * c))) + 1;

        //stores current stat values
        int _hp = hp;
        int _atk = atk;
        int _spa = spa;
        int _spdef = spdef;
        int _def = def;
        int _spd = spd;

        //adds the exp gained
        exp += gain;

        //stores current max hp value
        int _maxHp = maxHP;

        //checks whether there is enough exp points to level up
        while (exp >= expCurve)
        {
            //resets exp bar to 0 and continues adding up the leftover exp when leveled up
            int remaining = exp - expCurve;
            if (remaining <= 0)
            {
                remaining = 0;
            }
            exp = 0 + remaining;

            //increases their level
            level++;
            //updates the exp needed to level up
            GetExpCurve();
            //updates their stats
            GetStats();

            //calculates the stat increase
            _maxHp = maxHP - _maxHp;
            _atk = atk - _atk;
            _spa = spa - _spa;
            _spdef = spdef - _spdef;
            _def = def - _def;
            _spd = spd - _spd;
            //plays stat menu animation
            levelUpAnim.LevelUpAnim(_maxHp, _atk, _spa, _def, _spdef, _spd);
            //adds the increase of hp to current hp value to maintain current health %
            hp += _maxHp;
        }
        //updates their level text
        UpdateInfo();
        Debug.Log("Exp Gained = " + gain);
    }

    public void UpdateInfo()
    {
        Debug.Log("Info Updated");
        monInfo.UpdateLabel(level);
    }

    public void SetHponSpwan()
    {
        hp = maxHP;
        hpBar.maxValue = maxHP;
        hpBar.value = hp;
    }

    private void OnCollisionEnter(Collision other)
    {
        if(other.gameObject.tag == "Enemy")
        {
            Stats stats;
            stats = other.gameObject.GetComponent<Stats>();
            int hp = stats.hp;
            int def = stats.def;

            Damage(30, def, hp, stats);
        }
    }

    //calculates how much damage to deal
    public void Damage(int _attackPower, int enemyDef, int enemyHp, Stats stats)
    {
        //try calling function when using attack and inputting the attacks power value to calculate
        //int attackPower = 50;
        int damage = Mathf.RoundToInt((((2 * level / 5 + 2) * atk * _attackPower / def) / 50) + 2 /*stab*weakness/resistance*/ * 100 / 100);

        stats.hp -= damage;

        Debug.Log(damage);

        //in collision script on attack hit box, grab opposing stat script and - the hp stat with the calculated damage : The how to compute these values
    }
}
