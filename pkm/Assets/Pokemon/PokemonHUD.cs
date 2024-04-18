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

    //Gets reference to what pokemon it is i.e. charmander, bulbasaur
    [SerializeField] PokemonBase _base;
    [SerializeField] int level;

    //assigned moves that we can use to attack
    [SerializeField] public MoveBase sp1;
    [SerializeField] public MoveBase sp2;

    public Pokemon Pokemon { get; set; }

    private void Awake()
    {
        //sets the data into the pokemon class
        Pokemon = new Pokemon(_base, level);

        //update the UI info
        SetData();

        //updates the current movelist
        SetMove(Pokemon.Moves);
    }

    //sets the pokemons name, level and hp UI 
    public void SetData()
    {
        nameText.text = Pokemon.Base.Name;
        levelText.text = "Lvl: " +  Pokemon.Level;
        hpBar.SetHP((float)Pokemon.HP / Pokemon.MaxHP);
    }


    public void SetMove(List<Move> moves)
    {
        //assigns moves from list into variable to be used in attack script
        sp1 = moves[0].Base;
        sp2 = moves[1].Base;
    }

    //updates the UI in game to represent current health
    public void UpdateHP()
    {
        hpBar.SetHP((float)Pokemon.HP / Pokemon.MaxHP);
    }
}
