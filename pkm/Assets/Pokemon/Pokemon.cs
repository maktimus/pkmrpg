using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pokemon
{
    public PokemonBase Base { get; set; }
    public int Level { get; set; }

    public int HP { get; set; }

    public List<Move> Moves { get; set; }

    public Pokemon(PokemonBase pBase, int pLevel)
    {
        Base = pBase;
        Level = pLevel;
        HP = MaxHP;

        #region Generating Moves
        Moves = new List<Move>();

        foreach (var move in Base.LearnableMoves)
        {
            if (move.Level <= Level)
                Moves.Add(new Move(move.Base));

            if (Moves.Count >= 4)
                break;
        }
        #endregion
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
