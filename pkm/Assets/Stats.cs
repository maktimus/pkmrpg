using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Stats : MonoBehaviour
{
    public int baseAtk, baseSpa, baseDef, baseSpdef, baseSpd, baseHp;
    public int atk, spa, spdef, def, spd, hp, maxHP;
    public  int level, exp, enemyLvl;
        
    public bool isFast = false, isMFast = false, isMSlow = false, isSlow = false;
    public int expCurve;
    int expGain;

    public Slider expBar, hpBar;

    AnimController levelUpAnim;
    MonHeader monInfo;
    // Start is called before the first frame update
    void Start()
    {
        GetExpCurve();
        GetStats();
        monInfo = this.GetComponentInChildren<MonHeader>();
        monInfo.UpdateLabel(level);
        levelUpAnim = this.GetComponentInChildren<AnimController>();
        //expBar = GetComponentInChildren<Slider>();
        hp = maxHP;
    }

    //determines how fast it levels up
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

        hpBar.maxValue = maxHP;
        
     
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            GainExp(enemyLvl, 0);
            hp -= 3;
        }

        hp = Mathf.Clamp(hp, 0, maxHP);

        //find how to decrease/increase these values using time.deltatime or other ways to gradually but once per event i.e exp gain or damage taken
        expBar.maxValue = expCurve;
        expBar.value = exp;
        hpBar.value = hp;

        /* expBar.maxValue = expCurve;
         //problem expgain does not reset

         exp += Mathf.RoundToInt(expGain * Time.deltaTime * 2);
         expBar.value = exp;

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
         }*/
    }

    //exp gain
    public void GainExp(int enemyLevel, int remainder)
    {
        int a = 10 + (enemyLevel * 2);
        int b = (enemyLevel * 160) / 5;
        int c = (enemyLevel + level + 10);
        //calculates how much exp we get from enemies
        int gain = Mathf.RoundToInt(Mathf.Floor(Mathf.Sqrt(a) * (a * a)) * (b * 1.5f) / Mathf.Floor(Mathf.Sqrt(c) * (c * c))) + 1;
        //expGain = gain;

        //stores current stat values
        int _hp = hp;
        int _atk = atk;
        int _spa = spa;
        int _spdef = spdef;
        int _def = def;
        int _spd = spd;

        //adds to their exp
        exp += gain;

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
            _hp = hp - _hp;
            _atk = atk - _atk;
            _spa = spa - _spa;
            _spdef = spdef - _spdef;
            _def = def - _def;
            _spd = spd - _spd;
            //plays stat menu animation
            levelUpAnim.LevelUpAnim(_hp, _atk, _spa, _def, _spdef, _spd);
        }
        //updates their level text
        monInfo.UpdateLabel(level);
        Debug.Log("Exp Gained = " + gain);
    }


    public void Damage()
    {
        //damage calculator
        int attackPower = 50;
        int damage = Mathf.RoundToInt((((2 * level / 5 + 2) * atk * attackPower / def) / 50) + 2 /*stab*weakness/resistance*/ * 100 / 100);
        Debug.Log(damage);
    }
}
