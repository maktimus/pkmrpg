using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pokemon
{
    public PokemonBase Base { get; set; }
    public int Level { get; set; }

    public int HP { get; set; }

    //reference to its move list
    public List<Move> Moves { get; set; }

    //gets reference to what pokemon and level it is
    public Pokemon(PokemonBase pBase, int pLevel)
    {
        Base = pBase;
        Level = pLevel;
        HP = MaxHP;

        #region Generating Moves
        Moves = new List<Move>();

        //for every move that is set in the base pokemon
        foreach (var move in Base.LearnableMoves)
        {
            //checks that the level requirement has been achieved by the pokemon
            if (move.Level <= Level)
                //adds the move to the pokemon
                Moves.Add(new Move(move.Base));
            //if there is already 4 moves equipped, do nothing
            if (Moves.Count >= 4)
                break;
        }
        #endregion
    }

    //calculates damage taken
    public bool TakeDamage(int movePower, Pokemon attacker)
    {
        //implements a high and low roll for damage taken for slight randomness
        float modifiers = Random.Range(0.8f, 1f);

        //damage calc formula from pokemon
        float a = (2 * attacker.Level + 10) / 250f;
        float d = a * movePower * ((float)attacker.Attack / Defense) + 2;
        int damage = Mathf.FloorToInt(d * modifiers);

        //reduce targets hp
        HP -= damage;
        Debug.Log("Ouch" + HP);

        //checks if they have fainted
        if(HP <= 0)
        {
            HP = 0;
            return true;
        }
        return false;
    }

    #region Calculate Stats
    public int MaxHP
    {
        get { return Mathf.RoundToInt(0.01f * (2 * Base.BaseHP + 31 + Mathf.Floor(0.25f * 256f)) * Level) + 10; }
    }

    public int Attack
    {
        get { return Mathf.FloorToInt(Mathf.Floor(0.01f * (2 * Base.BaseAtk + 31 + Mathf.Floor(0.25f * 256f)) * Level) + 5); }
    }

    public int Defense
    {
        get { return Mathf.FloorToInt(Mathf.Floor(0.01f * (2 * Base.BaseDef + 31 + Mathf.Floor(0.25f * 256f)) * Level) + 5); }
    }

    public int SpAttack
    {
        get { return Mathf.FloorToInt(Mathf.Floor(0.01f * (2 * Base.BaseSpAtk + 31 + Mathf.Floor(0.25f * 256f)) * Level) + 5); }
    }

    public int SpDefense
    {
        get { return Mathf.FloorToInt(Mathf.Floor(0.01f * (2 * Base.BaseSpDef + 31 + Mathf.Floor(0.25f * 256f)) * Level) + 5); }
    }

    public int Speed
    {
        get { return Mathf.FloorToInt(Mathf.Floor(0.01f * (2 * Base.BaseSpd + 31 + Mathf.Floor(0.25f * 256f)) * Level) + 5); }
    }
    #endregion
}
