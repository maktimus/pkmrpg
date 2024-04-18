using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//used to create new types of pokemon
[CreateAssetMenu(fileName = "Pokemon", menuName = "Pokemon/Create new Pokemon")]
public class PokemonBase : ScriptableObject
{
    [Header("Name")]
    [SerializeField] string name_;

    [Header("Info")]
    [TextArea]
    [SerializeField] string description;

    [Header("Typing")]
    [SerializeField] PokemonType type1;
    [SerializeField] PokemonType type2;

    [Header("Base Stats")]
    [SerializeField] int baseHP;
    [SerializeField] int baseAtk;
    [SerializeField] int baseDef;
    [SerializeField] int baseSpAtk;
    [SerializeField] int baseSpDef;
    [SerializeField] int baseSpd;

    [Header("Animation Controller")]
    [SerializeField] Animator animController;

    [Header("Move List")]
    [SerializeField] List<LearnableMove> learnableMoves;

    //to get reference of these variables from other scripts
    #region public properties
    public string Name
    {
        get { return name_; }
    }

    public string Description
    {
        get { return description; }
    }

    public PokemonType Type1
    {
        get { return type1; }
    }

    public PokemonType Type2
    {
        get { return type2; }
    }

    public int BaseHP
    {
        get { return baseHP; }
    }

    public int BaseAtk
    {
        get { return baseAtk; }
    }

    public int BaseDef
    {
        get { return baseDef; }
    }

    public int BaseSpAtk
    {
        get { return baseSpAtk; }
    }

    public int BaseSpDef
    {
        get { return baseSpDef; }
    }

    public int BaseSpd
    {
        get { return baseSpd; }
    }

    public List<LearnableMove> LearnableMoves
    {
        get { return learnableMoves; }
    }

    public Animator AnimController
    {
        get { return animController; }
    }
}
#endregion

//allows to save data locally
[System.Serializable]
public class LearnableMove
{
    //set up its move list
    [SerializeField] MoveBase mBase;
    //level to learn each move
    [SerializeField] int level;

    public MoveBase Base
    {
        get { return mBase; }
    }

    public int Level
    {
        get { return level; }
    }
}

//set variables for each type
public enum PokemonType
{
    None,
    Normal,
    Fire,
    Water,
    Electric,
    Grass,
    Ice,
    Fighting,
    Poison,
    Ground,
    Flying,
    Psychic,
    Bug,
    Rock,
    Ghost,
    Dragon,
    Fairy,
    Dark
}