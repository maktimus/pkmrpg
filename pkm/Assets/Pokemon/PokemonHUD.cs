using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PokemonHUD : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI nameText;
    [SerializeField] TextMeshProUGUI levelText;
    [SerializeField] HP hpBar;
    [SerializeField] PokemonBase _base;
    [SerializeField] int level;

    public Pokemon Pokemon { get; set; }

    private void Start()
    {
        Pokemon = new Pokemon(_base, level);

        SetData();

        Debug.Log("Attack: " + Pokemon.Attack + "HP: " + Pokemon.HP);
    }

    public void SetData()
    {
        nameText.text = Pokemon.Base.Name;
        levelText.text = "Lvl: " +  Pokemon.Level;
        hpBar.SetHP((float)Pokemon.HP / Pokemon.MaxHP);
    }

    //testing
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Backspace))
        {
            Pokemon.HP -= 5;
            Debug.Log(Pokemon.Base.Name + Pokemon.HP);
            hpBar.SetHP((float)Pokemon.HP / Pokemon.MaxHP);
        }
    }
}
