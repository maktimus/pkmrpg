using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public interface EnemyStats
{
    void Damage(int damageAmount);

    void Die();

    int baseAtk { get; set; }
    int baseSpa { get; set; }
    int baseDef { get; set; }
    int baseSpdef { get; set; }
    int baseSpd { get; set; }
    int baseHp { get; set; }

    int atk { get; set; }
    int spa { get; set; }
    int def { get; set; }
    int spdef { get; set; }
    int spd { get; set; }
    int hp { get; set; }
    int maxHP { get; set; }
    int moveSpd { get; set; }

    int level { get; set; }

    Slider hpBar { get; set; }

    TextMeshPro info { get; set; }

    string _name { get; set; }
    
}
