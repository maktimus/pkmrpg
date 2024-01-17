using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stats : MonoBehaviour
{
     int baseAtk, baseSpa, baseDef, baseSpdef, baseSpd, baseHp;
     int atk, spa, spdef, def, spd, hp;
    public int level, exp, enemyLvl;

    public int fast, mFast, mSlow, slow; 
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        //stat calculations
        hp = Mathf.RoundToInt(0.01f * (2 * baseHp + 31 + Mathf.Floor(0.25f * 256.0f)) * level + 10);
        atk = Mathf.RoundToInt(Mathf.Floor(0.01f * (2 * baseAtk + 31 + Mathf.Floor(0.25f * 256f)) * level) + 5);
        spa = Mathf.RoundToInt(Mathf.Floor(0.01f * (2 * baseSpa + 31 + Mathf.Floor(0.25f * 256f)) * level) + 5);
        spdef = Mathf.RoundToInt(Mathf.Floor(0.01f * (2 * baseSpdef + 31 + Mathf.Floor(0.25f * 256f)) * level) + 5);
        def = Mathf.RoundToInt(Mathf.Floor(0.01f * (2 * baseDef + 31 + Mathf.Floor(0.25f * 256f)) * level) + 5);
        spd = Mathf.RoundToInt(Mathf.Floor(0.01f * (2 * baseSpd + 31 + Mathf.Floor(0.25f * 256f)) * level) + 5);

        //exp curve
        fast = Mathf.RoundToInt(0.8f * (Mathf.Pow(level, 3)));
        mFast = Mathf.RoundToInt(Mathf.Pow(level, 3));
        mSlow = Mathf.RoundToInt(1.2f * (Mathf.Pow(level, 3)) - 15 * (Mathf.Pow(level, 2)) + 100 * (level) - 140);
        if(mSlow <= 0)
        {
            mSlow = 0;
        }
        slow = Mathf.RoundToInt(1.25f * (Mathf.Pow(level, 3)));

        if(Input.GetKeyDown(KeyCode.P))
        {
            GainExp(enemyLvl);
        }

        if (exp >= fast)
        {
            int remaining = exp - mSlow;
            level++;
            exp = 0 + remaining;
        }

    }

    //exp gain
    public void GainExp(int enemyLevel)
    {
        int a = 10 + (enemyLevel * 2);
        int b = (enemyLevel * 130) / 5;
        int c = (enemyLevel + level + 10);
        int gain = Mathf.RoundToInt(Mathf.Floor(Mathf.Sqrt(a) * (a * a)) * b / Mathf.Floor(Mathf.Sqrt(c) * (c * c))) + 1;

        exp += gain;

        Debug.Log("Exp Gained = " + gain);

        //damage calculator
        int attackPower = 50;
        int damage = Mathf.RoundToInt((((2 * level / 5 + 2) * atk * attackPower / def) / 50) + 2 /*stab*weakness/resistance*/ * 100 / 100);
        Debug.Log(damage);
    }
}
